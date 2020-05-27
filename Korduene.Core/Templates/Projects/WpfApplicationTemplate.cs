using System.Collections.Generic;

namespace Korduene.Templates.Projects
{
    public class WpfApplicationTemplate : ProjectTemplate
    {
        public WpfApplicationTemplate()
        {
            this.Name = "WPF Application";
            this.DefaultName = "WpfApp";
            this.Language = "C#";
            this.MainFile = "ProjectName.csproj";
            this.Icon = "CSWPFApplication";
            this.ProjectTypeGuid = "{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}";
            this.Files = new List<string>()
            {
                "App.xaml",
                "App.xaml.cs",
                "IgnoredNamespaces.txt",
                "MainWindow.xaml",
                "MainWindow.xaml.cs"
            };
        }
    }
}
