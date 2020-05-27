using System.Windows.Controls;

namespace Korduene.UI.WPF.CustomControls
{
    public class UserControlEx : UserControl
    {
        private bool _loaded;

        public UserControlEx()
        {
            this.Loaded += UserControlEx_Loaded;
        }

        private void UserControlEx_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is IDockable dockable)
            {
                if (!_loaded)
                {
                    _loaded = true;
                    dockable.OnLoaded();
                }

                dockable.OnShown();
            }
        }
    }
}
