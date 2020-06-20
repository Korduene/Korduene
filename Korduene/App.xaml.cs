using Korduene.UI.WPF;
using System.Windows;

namespace Korduene
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            System.Windows.Forms.Application.EnableVisualStyles();

            Korduene.Current.Instance.UIServices = UIServices.Instance;

            DotNetInfo.SetEnvironmentVariables();

            base.OnStartup(e);
        }
    }
}
