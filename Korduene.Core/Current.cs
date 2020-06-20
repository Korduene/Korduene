using Korduene.Templates;
using Korduene.UI;
using Korduene.UI.Dialogs.Data;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Korduene
{
    /// <summary>
    /// Current
    /// </summary>
    /// <seealso cref="INotifyPropertyChanged" />
    [Description("Current")]
    [DisplayName("Current")]
    [DebuggerDisplay("{this}")]
    public sealed class Current : INotifyPropertyChanged
    {
        #region [Events]

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler<string> Output;
        public event EventHandler OutputClear;
        public event EventHandler OutputResetColor;
        public event EventHandler<ConsoleColor> OutputSetColor;

        public event EventHandler SolutionLoaded;
        public event EventHandler BuildStarted;
        public event EventHandler BuildFinished;

        #endregion

        #region [Private Objects]

        private AppState _state;
        private object[] _selectedItems;
        private ObservableCollection<KDocument> _openDocuments;
        private KDocument _activeDocument;
        private Microsoft.CodeAnalysis.Project _selectedProject;
        private string _selectedConfiguration;
        private string _selectedPlatform;
        private WorkspaceItemCollection _workspaceItems;

        #endregion

        #region [Static Properties]

        public static Current Instance { get; private set; }

        #endregion

        #region [Public Properties]

        /// <summary>
        /// Gets the current open documents.
        /// </summary>
        /// <value>
        /// The open documents.
        /// </value>
        /// <summary>
        /// Gets or sets the open documents.
        /// </summary>
        /// <value>
        /// The open documents.
        /// </value>
        public ObservableCollection<KDocument> OpenDocuments
        {
            get { return _openDocuments ??= new ObservableCollection<KDocument>(); }
            set
            {
                if (_openDocuments != value)
                {
                    _openDocuments = value;
                    OnPropertyChanged();
                }
            }
        }

        public KDocument ActiveDocument
        {
            get { return _activeDocument; }
            set
            {
                if (_activeDocument != value)
                {
                    _activeDocument = value;
                    OnPropertyChanged();
                }
            }
        }

        public IEnumerable<ProjectTemplate> ProjectTemplates { get { return ProjectTemplate.GetTemplates(Path.Combine(AppContext.BaseDirectory, "Templates", "Projects")); } }

        public IEnumerable<FileTemplate> FileTemplates { get { return FileTemplate.GetTemplates(Path.Combine(AppContext.BaseDirectory, "Templates", "Files")); } }

        public KordueneWorkspace Workspace { get; private set; }

        public IEnumerable<Microsoft.CodeAnalysis.Project> Projects
        {
            get { return Workspace?.CurrentSolution.Projects; }
        }

        public IEnumerable<string> Configurations
        {
            get { return Workspace?.Configurations; }
        }

        public IEnumerable<string> Platforms
        {
            get { return Workspace?.Platforms; }
        }

        public IUIServices UIServices { get; set; }

        public AppState State
        {
            get { return _state; }
            set
            {
                if (_state != value)
                {
                    _state = value;
                    OnPropertyChanged();
                    GlobalCommands.GlobalCommandUtilities.RaiseAllCanExecuteChanged();
                }
            }
        }

        internal void RemoveProject(string path)
        {
            throw new NotImplementedException();
        }

        public Microsoft.CodeAnalysis.Project SelectedProject
        {
            get { return _selectedProject; }
            set
            {
                if (_selectedProject != value)
                {
                    _selectedProject = value;
                    OnPropertyChanged();
                }
            }
        }

        public string SelectedConfiguration
        {
            get { return _selectedConfiguration; }
            set
            {
                if (_selectedConfiguration != value)
                {
                    _selectedConfiguration = value;
                    OnPropertyChanged();
                }
            }
        }

        public string SelectedPlatform
        {
            get { return _selectedPlatform; }
            set
            {
                if (_selectedPlatform != value)
                {
                    _selectedPlatform = value;
                    OnPropertyChanged();
                }
            }
        }

        public object[] SelectedItems
        {
            get { return _selectedItems; }
            set
            {
                if (_selectedItems != value)
                {
                    _selectedItems = value;
                    OnPropertyChanged();
                }
            }
        }

        public WorkspaceItemCollection WorkspaceItems
        {
            get { return _workspaceItems ??= new WorkspaceItemCollection(); }
            private set
            {
                if (_workspaceItems != value)
                {
                    _workspaceItems = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region [Commands]

        #endregion

        #region [Constructors]

        static Current()
        {
            Instance = new Current();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Current"/> class.
        /// </summary>
        public Current()
        {
        }

        #endregion

        #region [Public Methods]

        public void OpenSolution(string slnFile)
        {
            Workspace = new KordueneWorkspace(slnFile);
            OnPropertyChanged(nameof(Projects));
            OnPropertyChanged(nameof(Configurations));
            OnPropertyChanged(nameof(Platforms));

            SelectedProject = Workspace.CurrentSolution.Projects.FirstOrDefault();
            SelectedConfiguration = Configurations.FirstOrDefault();
            SelectedPlatform = Platforms.FirstOrDefault();

            UpdateWorkspaceItems();
            SolutionLoaded?.Invoke(this, EventArgs.Empty);
            State = AppState.SolutionReady;
        }

        public void CreateSolution(string name, string directory, ProjectTemplate template)
        {
            var slnDir = Path.Combine(directory, name);

            Directory.CreateDirectory(slnDir);

            var sln = new Microsoft.DotNet.Cli.Sln.Internal.SlnFile
            {
                FullPath = Path.Combine(slnDir, $"{name}.sln"),
                ProductDescription = "Visual Studio Version 16",
                FormatVersion = "12.00",
                VisualStudioVersion = "16",
                MinimumVisualStudioVersion = "10"
            };

            //create project dir
            var projDir = Path.Combine(slnDir, name);
            template.CreateFiles(projDir);

            var fullPath = Path.Combine(directory, name, template.ChosenName + Path.GetExtension(template.MainFile));

            var proj = new Microsoft.DotNet.Cli.Sln.Internal.SlnProject()
            {
                Id = template.Id,
                FilePath = Path.GetRelativePath(directory, fullPath),
                Name = name,
                TypeGuid = template.ProjectTypeGuid
            };

            sln.Projects.Add(proj);

            var projConfig = new Microsoft.DotNet.Cli.Sln.Internal.SlnPropertySet(proj.Id);
            projConfig.SetValue("Debug|Any CPU.ActiveCfg", "Debug|Any CPU");
            projConfig.SetValue("Debug|Any CPU.Build.0", "Debug|Any CPU");
            projConfig.SetValue("Release|Any CPU.ActiveCfg", "Release|Any CPU");
            projConfig.SetValue("Release|Any CPU.Build.0", "Release|Any CPU");

            sln.SolutionConfigurationsSection.SetValue("Debug|Any CPU", "Debug|Any CPU");
            sln.SolutionConfigurationsSection.SetValue("Release|Any CPU", "Release|Any CPU");

            sln.ProjectConfigurationsSection.Add(projConfig);
            var slnProperties = new Microsoft.DotNet.Cli.Sln.Internal.SlnSection()
            {
                Id = "SolutionProperties"
            };
            slnProperties.Properties.SetValue("HideSolutionNode", "FALSE");
            sln.Sections.Add(slnProperties);

            var extGlobals = new Microsoft.DotNet.Cli.Sln.Internal.SlnSection()
            {
                Id = "ExtensibilityGlobals",
                SectionType = Microsoft.DotNet.Cli.Sln.Internal.SlnSectionType.PostProcess
            };
            extGlobals.Properties.SetValue("SolutionGuid", $"{{{Guid.NewGuid().ToString()}}}");
            sln.Sections.Add(extGlobals);

            sln.Write();

            OpenSolution(sln.FullPath);
        }

        public void OpenDocument(Document document)
        {
            var ext = Path.GetExtension(document.FilePath);

            if (OpenDocuments.FirstOrDefault(x => x.FilePath.Equals(document.FilePath, StringComparison.OrdinalIgnoreCase)) is KDocument doc)
            {
                doc.IsSelected = false;
                doc.IsSelected = true;
                return;
            }

            if (ext.Equals(".cs", StringComparison.OrdinalIgnoreCase))
            {
                OpenDocuments.Add(new CSDocument(document)
                {
                    IsSelected = true,
                    IsSaved = true,
                    Name = document.Name
                });
            }
            else if (ext.Equals(".xaml", StringComparison.OrdinalIgnoreCase))
            {
                OpenDocuments.Add(new XamlDocument(document)
                {
                    IsSelected = true,
                    IsSaved = true,
                    Name = document.Name
                });
            }
        }

        public void RenameItem(WorkspaceItem item)
        {
            var path = item.Path;
            var name = Path.GetFileNameWithoutExtension(path);
            var nameWithExt = Path.GetFileName(path);
            var ext = Path.GetExtension(path);

            var result = UIServices.CreateDialog<PromptData>(UI.Enums.DialogType.GenericPrompt, "Rename", name);

            if (!result.Success)
            {
                return;
            }

            if (name.Equals(result.Data.Value, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            switch (item.Type)
            {
                case WorkspaceItemType.Solution:
                    var newSlnPath = Path.Combine(Path.GetDirectoryName(path), result.Data.Value + ext);
                    File.Move(path, newSlnPath);
                    Instance.OpenSolution(newSlnPath);
                    break;
                case WorkspaceItemType.Project:
                    var newPrjPath = Path.Combine(Path.GetDirectoryName(path), result.Data.Value + ext);
                    File.Move(path, newPrjPath);

                    foreach (var proj in Workspace.SlnFile.Projects)
                    {
                        if (Path.GetFileName(proj.FilePath).Equals(nameWithExt, StringComparison.OrdinalIgnoreCase))
                        {
                            proj.Name = result.Data.Value;
                            proj.FilePath = Path.Combine(Path.GetDirectoryName(proj.FilePath), result.Data.Value + ext);
                        }
                    }

                    Workspace.SlnFile.Write();
                    Instance.OpenSolution(Workspace.SlnFile.FullPath);
                    break;
                case WorkspaceItemType.Folder:
                    var newDirPath = Path.Combine(Path.GetDirectoryName(path), result.Data.Value);
                    Directory.Move(path, newDirPath);
                    Instance.UpdateWorkspaceItems();
                    break;
                case WorkspaceItemType.File:
                    var newFilePath = Path.Combine(Path.GetDirectoryName(path), result.Data.Value + ext);
                    File.Move(path, newFilePath);
                    Instance.UpdateWorkspaceItems();
                    break;
                default:
                    break;
            }
        }

        public async Task BuildSolution(string target)
        {
            State = AppState.Building;
            BuildStarted?.Invoke(this, EventArgs.Empty);

            await Task.Factory.StartNew(() =>
            {
                ClearOutput();

                var msbuild = Microsoft.Build.Execution.BuildManager.DefaultBuildManager;
                var projs = Microsoft.Build.Evaluation.ProjectCollection.GlobalProjectCollection;

                projs.AddToolset(new Microsoft.Build.Evaluation.Toolset("Current", Path.Combine(DotNetInfo.SdkPath, DotNetInfo.SdkVersion), projs, Path.Combine(DotNetInfo.SdkPath, DotNetInfo.SdkVersion)));

                var globalProperties = new Dictionary<string, string>()
                {
                   { "Configuration" , SelectedConfiguration},
                   { "PlatformTarget" , SelectedPlatform}
                };

                foreach (var proj in this.Workspace.CurrentSolution.Projects)
                {
                    projs.LoadProject(proj.FilePath);
                }

                var parameters = new Microsoft.Build.Execution.BuildParameters(projs);

                var loggers = new List<Microsoft.Build.Framework.ILogger>();
                var cl = new Microsoft.Build.Logging.ConsoleLogger(Microsoft.Build.Framework.LoggerVerbosity.Normal, new Microsoft.Build.Logging.WriteHandler((s) => MSBuildLog(s)), new Microsoft.Build.Logging.ColorSetter((c) => MSBuildLogSetColor(c)), new Microsoft.Build.Logging.ColorResetter(() => MSBuildLogResetColor()));

                loggers.Add(cl);

                parameters.Loggers = loggers;

                msbuild.BeginBuild(parameters);

                var targets = new[] { target };

                if (!target.Equals("restore", StringComparison.OrdinalIgnoreCase) && !target.Equals("clean", StringComparison.OrdinalIgnoreCase))
                {
                    targets = new[] { "restore", target };
                }

                foreach (var p in Workspace.ProjectInstances)
                {
                    var pinstance = new Microsoft.Build.Execution.ProjectInstance(p.FullPath, globalProperties, projs.DefaultToolsVersion, projs);
                    var request = new Microsoft.Build.Execution.BuildRequestData(pinstance, targets);

                    msbuild.BuildRequest(request);
                }

                msbuild.EndBuild();
            });

            State = AppState.SolutionReady;
            BuildFinished?.Invoke(this, EventArgs.Empty);
        }

        public async Task Start()
        {
            await BuildSolution("build");

            State = AppState.Running;

            await Task.Factory.StartNew(() =>
            {
                var pinstance = Workspace.GetProjectInstance(SelectedProject.FilePath);

                var path = pinstance.Properties.FirstOrDefault(x => x.Name.Equals("RunCommand", StringComparison.OrdinalIgnoreCase))?.EvaluatedValue;

                if (string.IsNullOrWhiteSpace(path))
                {
                    return;
                }

                Process.Start(new ProcessStartInfo(path)
                {
                    WorkingDirectory = Path.GetDirectoryName(path)
                }).WaitForExit();
            });

            State = AppState.SolutionReady;
        }

        public void UpdateWorkspaceItems()
        {
            if (Workspace == null || Workspace.CurrentSolution == null)
            {
                return;
            }

            if (!(WorkspaceItems.FirstOrDefault(x => x.Type == WorkspaceItemType.Solution) is WorkspaceItem slnItem))
            {
                slnItem = new WorkspaceItem(WorkspaceItemType.Solution);
                WorkspaceItems.AddItem(slnItem);
            }

            slnItem.Name = Path.GetFileNameWithoutExtension(Workspace.CurrentSolution.FilePath);
            slnItem.Path = Workspace.CurrentSolution.FilePath;
            slnItem.Tag = Workspace.CurrentSolution;

            foreach (var project in Workspace.CurrentSolution.Projects)
            {
                if (!(slnItem.Items.FirstOrDefault(x => x.Path.Equals(project.FilePath, StringComparison.OrdinalIgnoreCase)) is WorkspaceItem prjItem))
                {
                    prjItem = new WorkspaceItem(WorkspaceItemType.Project, null, project.Name, project.FilePath, project);
                    slnItem.Items.AddItem(prjItem);
                }

                GetProjectWorkspaceItems(project, prjItem, prjItem);
            }

            foreach (var projItem in slnItem.Items.Where(x => x.Type == WorkspaceItemType.Project).ToArray())
            {
                if (!Workspace.CurrentSolution.Projects.Any(x => x.Name.Equals(projItem.Name, StringComparison.OrdinalIgnoreCase)))
                {
                    slnItem.Items.Remove(projItem);
                }
            }

            OnPropertyChanged(nameof(WorkspaceItems));
        }

        private void GetProjectWorkspaceItems(Microsoft.CodeAnalysis.Project project, WorkspaceItem projectItem, WorkspaceItem item)
        {
            var dirPath = Path.GetExtension(item.Path).Equals(".csproj", StringComparison.OrdinalIgnoreCase) ? Path.GetDirectoryName(item.Path) : item.Path;

            var directories = new DirectoryInfo(dirPath).GetDirectories();

            foreach (var dir in directories)
            {
                if (!(item.Items.FirstOrDefault(x => x.Path.Equals(dir.FullName, StringComparison.OrdinalIgnoreCase)) is WorkspaceItem dirItem))
                {
                    dirItem = new WorkspaceItem(WorkspaceItemType.Folder, projectItem, dir.Name, dir.FullName);
                    item.Items.AddItem(dirItem);
                }

                GetProjectWorkspaceItems(project, projectItem, dirItem);
            }

            foreach (var wsi in item.Items.Where(x => x.Type == WorkspaceItemType.Folder).ToArray())
            {
                if (!directories.Any(x => x.FullName.Equals(wsi.Path, StringComparison.OrdinalIgnoreCase)))
                {
                    item.Items.Remove(wsi);
                }
            }

            var files = new DirectoryInfo(dirPath).GetFiles().OrderBy(x => x.Name);

            foreach (var file in files)
            {
                if (file.Extension.Equals(".csproj", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                if (projectItem.HasFile(file.FullName))
                {
                    continue;
                }

                if (item.Items.FirstOrDefault(x =>
                !x.Name.Equals(file.Name, StringComparison.OrdinalIgnoreCase)
                && file.Name.StartsWith(x.Name, StringComparison.OrdinalIgnoreCase)
                && !item.HasFile(file.FullName)) is WorkspaceItem parent)
                {
                    var doc = project.Documents.FirstOrDefault(x => x.FilePath.Equals(file.FullName, StringComparison.OrdinalIgnoreCase));

                    //TODO: specify source kind
                    if (doc == null)
                    {
                        doc = project.AddDocument(file.Name, string.Empty, null, file.FullName);
                        project = doc.Project;
                    }

                    parent.Items.AddItem(new WorkspaceItem(WorkspaceItemType.File, projectItem, file.Name, file.FullName, doc));
                }
                else
                {
                    var doc = project.Documents.FirstOrDefault(x => x.FilePath.Equals(file.FullName, StringComparison.OrdinalIgnoreCase));

                    //TODO: specify source kind
                    if (doc == null)
                    {
                        doc = project.AddDocument(file.Name, string.Empty, null, file.FullName);
                        project = doc.Project;
                    }

                    item.Items.AddItem(new WorkspaceItem(WorkspaceItemType.File, projectItem, file.Name, file.FullName, doc));
                }
            }

            foreach (var wsi in item.Items.Where(x => x.Type == WorkspaceItemType.File).ToArray())
            {
                if (!files.Any(x => x.FullName.Equals(wsi.Path, StringComparison.OrdinalIgnoreCase)))
                {
                    item.Items.Remove(wsi);
                }
            }
        }

        #endregion

        #region [Private Methods]

        private void MSBuildLog(string message)
        {
            Output?.Invoke(this, message);
        }

        private void MSBuildLogSetColor(ConsoleColor c)
        {
            OutputSetColor?.Invoke(this, c);
        }

        private void MSBuildLogResetColor()
        {
            OutputResetColor?.Invoke(this, EventArgs.Empty);
        }

        private void ClearOutput()
        {
            OutputClear?.Invoke(this, EventArgs.Empty);
        }

        #endregion

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
