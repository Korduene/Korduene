using Korduene.Graphing.CS;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Host;
using Microsoft.DotNet.Cli.Sln.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace Korduene
{
    public class KordueneWorkspace : Workspace, IKordueneWorkspace
    {
        private List<Microsoft.Build.Execution.ProjectInstance> _projectInstances = new List<Microsoft.Build.Execution.ProjectInstance>();

        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<string> _configurations;
        private ObservableCollection<string> _platforms;

        public ObservableCollection<string> Configurations
        {
            get { return _configurations ??= new ObservableCollection<string>(); }
            set
            {
                if (_configurations != value)
                {
                    _configurations = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<string> Platforms
        {
            get { return _platforms ??= new ObservableCollection<string>(); }
            set
            {
                if (_platforms != value)
                {
                    _platforms = value;
                    OnPropertyChanged();
                }
            }
        }

        public IEnumerable<Microsoft.Build.Execution.ProjectInstance> ProjectInstances
        {
            get
            {
                foreach (var project in CurrentSolution.Projects)
                {
                    if (!_projectInstances.Any(x => x.FullPath.Equals(project.FilePath, StringComparison.OrdinalIgnoreCase)))
                    {
                        _projectInstances.Add(new Microsoft.Build.Execution.ProjectInstance(project.FilePath));
                    }
                }

                _projectInstances.RemoveAll(x => !CurrentSolution.Projects.Select(y => y.FilePath).Contains(x.FullPath));

                return _projectInstances;
            }
        }

        public IEnumerable<string> TargetPaths
        {
            get
            {
                return ProjectInstances.Select(x => x.Properties.FirstOrDefault(y => y.Name.Equals("TargetPath", StringComparison.OrdinalIgnoreCase))?.EvaluatedValue).Where(x => !string.IsNullOrWhiteSpace(x));
            }
        }

        public SlnFile SlnFile { get; }

        static KordueneWorkspace()
        {
        }

        public KordueneWorkspace(HostServices host, string workspaceKind) : base(host, workspaceKind)
        {
        }

        public KordueneWorkspace() : this(Microsoft.CodeAnalysis.Host.Mef.MefHostServices.DefaultHost, "KordueneWorkspace")
        {
        }

        public KordueneWorkspace(SlnFile sln) : this()
        {
            SlnFile = sln;

            var guidSection = sln.Sections.GetSection("ExtensibilityGlobals", SlnSectionType.PostProcess);
            var id = SolutionId.CreateFromSerialized(new Guid(guidSection.Properties.GetValue("SolutionGuid")));

            var solution = this.CreateSolution(SolutionInfo.Create(id, VersionStamp.Default, sln.FullPath));

            foreach (var item in sln.Projects)
            {
                var proj = ProjectInfo.Create(ProjectId.CreateFromSerialized(new Guid(item.Id)), VersionStamp.Default, item.Name, item.Name, LanguageNames.CSharp, Path.Combine(sln.BaseDirectory, item.FilePath));
                proj = AddProjectDocuments(proj);
                proj = AddDefaultReferences(proj);
                solution = solution.AddProject(proj);
            }

            var configs = sln.Sections.GetSection("SolutionConfigurationPlatforms");
            Configurations.Clear();
            foreach (var item in configs.Properties)
            {
                var configPlat = item.Value.Split("|");

                if (!Configurations.Contains(configPlat[0].Replace(" ", string.Empty)))
                {
                    Configurations.Add(configPlat[0]);
                }

                if (!Platforms.Contains(configPlat[1].Replace(" ", string.Empty)))
                {
                    Platforms.Add(configPlat[1].Replace(" ", string.Empty));
                }
            }

            this.SetCurrentSolution(solution);

            LoadProjectSymbols();
        }

        public KordueneWorkspace(string slnFile) : this(SlnFile.Read(slnFile))
        {
        }

        public Microsoft.Build.Execution.ProjectInstance GetProjectInstance(string fullPath)
        {
            return ProjectInstances.FirstOrDefault(x => x.FullPath.Equals(fullPath, StringComparison.OrdinalIgnoreCase));
        }

        public void LoadProjectSymbols()
        {
            SymbolProvider.ProjectSymbols.Clear();

            var compilation = Microsoft.CodeAnalysis.CSharp.CSharpCompilation.Create(this.CurrentSolution.ToString());

            foreach (var project in this.CurrentSolution.Projects)
            {
                compilation = compilation.AddReferences(project.MetadataReferences);

                var instance = ProjectInstances.FirstOrDefault(x => x.FullPath.Equals(project.FilePath));

                if (instance.Properties.FirstOrDefault(x => x.Name.Equals("TargetPath", StringComparison.OrdinalIgnoreCase)) is Microsoft.Build.Execution.ProjectPropertyInstance prop && File.Exists(prop.EvaluatedValue))
                {
                    compilation = compilation.AddReferences(MetadataReference.CreateFromFile(prop.EvaluatedValue));
                }

                var projectSymbols = new ProjectSymbolCache(project.Name);

                foreach (var refMeta in compilation.References)
                {
                    var visitor = new GetAllSymbolsVisitor();

                    var assemblySymbol = (IAssemblySymbol)compilation.GetAssemblyOrModuleSymbol(refMeta);

                    visitor.Visit(assemblySymbol.GlobalNamespace);

                    projectSymbols.AssemblySymbols.Add(new AssemblySymbolCache(assemblySymbol, visitor.NamedTypes));
                }

                SymbolProvider.ProjectSymbols.Add(projectSymbols);
            }
        }

        private ProjectInfo AddDefaultReferences(ProjectInfo project)
        {
            var sdkName = GetSDKName(project);

            var csprojContent = File.ReadAllText(project.FilePath);

            var wpf = csprojContent.Contains("<UseWPF>true</UseWPF>"); //TEMP - TODO: DO PROPER CHECK
            var winforms = csprojContent.Contains("<UseWindowsForms>true</UseWindowsForms>"); //TEMP - TODO: DO PROPER CHECK

            var defualtRefs = GetDefaultReferences("Microsoft.NET.Sdk", wpf, winforms);
            var references = GetDefaultReferences(sdkName, wpf, winforms, defualtRefs);
            var all = references.Concat(defualtRefs);

            return project.WithMetadataReferences(all);
        }

        private IEnumerable<MetadataReference> GetDefaultReferences(string sdk, bool wpf, bool winforms, IEnumerable<MetadataReference> existingReferences = null)
        {
            var references = new List<MetadataReference>();
            var existingRefNames = new List<string>();

            if (existingReferences != null)
            {
                existingRefNames = existingReferences?.Select(x => Path.GetFileName(x.Display)).ToList();
            }

            var packDir = DotNetInfo.GetPackPath(sdk);
            var xmlPath = Path.Combine(packDir, "data", "FrameworkList.xml");
            var netCoreAppRefs = new XmlSerializer(typeof(FileList)).Deserialize(new StringReader(File.ReadAllText(xmlPath))) as FileList;

            foreach (var item in netCoreAppRefs.Files)
            {
                var path = Path.Combine(DotNetInfo.PacksPath, packDir, string.Empty, item.Path.Replace("/", "\\"));

                if ((!wpf && string.Equals(item.Profile, "WPF")) || (!winforms && string.Equals(item.Profile, "WindowsForms")))
                {
                    continue;
                }

                if (!existingRefNames.Contains(Path.GetFileName(path)))
                {
                    references.Add(MetadataReference.CreateFromFile(path, default, GetXmlDocumentationProvider(path)));
                }
            }

            return references;
        }

        private string GetSDKName(ProjectInfo projectInfo)
        {
            var doc = new System.Xml.XmlDocument();
            doc.Load(projectInfo.FilePath);

            return doc.DocumentElement.GetAttribute("Sdk");
        }

        private DocumentationProvider GetXmlDocumentationProvider(string file)
        {
            var fi = new FileInfo(file);
            var xmlPath = Path.Combine(fi.Directory.FullName, $"{Path.GetFileNameWithoutExtension(file)}.xml");

            if (File.Exists(xmlPath))
            {
                return XmlDocumentationProvider.CreateFromFile(xmlPath);
            }

            return XmlDocumentationProvider.Default;
        }

        private ProjectInfo AddProjectDocuments(ProjectInfo project)
        {
            var documents = new List<DocumentInfo>();

            var baseDir = Path.GetDirectoryName(project.FilePath);

            var files = new DirectoryInfo(baseDir).GetFiles(string.Empty, SearchOption.TopDirectoryOnly).ToList();

            foreach (var dir in new DirectoryInfo(baseDir).GetDirectories().Where(x => !IsDirectoryIgnored(x)))
            {
                foreach (var file in dir.GetFiles(string.Empty, SearchOption.TopDirectoryOnly))
                {
                    files.Add(file);
                }
            }

            foreach (var file in files)
            {
                var sourceKind = file.Extension.Equals(".cs", StringComparison.OrdinalIgnoreCase) ? SourceCodeKind.Script : SourceCodeKind.Regular;

                //var loader = TextLoader.From(TextAndVersion.Create(Microsoft.CodeAnalysis.Text.SourceText.From(File.ReadAllText(file.FullName)), VersionStamp.Create(file.LastWriteTimeUtc), file.FullName));

                var loader = TextLoader.From(new KordueneSourceTextContainer(file.FullName), VersionStamp.Default, file.FullName);

                documents.Add(DocumentInfo.Create(DocumentId.CreateNewId(project.Id), file.Name, null, sourceKind, loader, file.FullName));
            }

            return project.WithDocuments(documents);
        }

        private bool IsDirectoryIgnored(DirectoryInfo directory)
        {
            return GetIgnoredDirectories(directory.Parent.FullName).Contains(directory.FullName);
        }

        private string[] GetIgnoredDirectories(string dir)
        {
            return new[]
            {
                Path.Combine(dir, "obj"),
                Path.Combine(dir, "bin")
            };
        }

        /// <summary>
        /// Called when public properties changed.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
