using Korduene.UI.WPF.CustomControls;
using Korduene.UI.WPF.ToolWindows.ViewModels;
using System.Windows;

namespace Korduene.UI.WPF.ToolWindows.Views
{
    /// <summary>
    /// Interaction logic for PropertiesView.xaml
    /// </summary>
    public partial class PropertiesView : UserControlEx
    {
        public PropertiesView()
        {
            InitializeComponent();
            this.DataContextChanged += PropertiesView_DataContextChanged;
        }

        private void PropertiesView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is PropertiesViewModel vm)
            {
                vm.PropertyChanged -= Vm_PropertyChanged;
                vm.PropertyChanged += Vm_PropertyChanged;
            }
        }

        private void Vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(PropertiesViewModel.SelectedObject))
            {
                propertiesGrid.SelectedObject = (DataContext as PropertiesViewModel).SelectedObject;
            }
            else if (e.PropertyName == nameof(PropertiesViewModel.SelectedObjects))
            {
                propertiesGrid.SelectedObjects = (DataContext as PropertiesViewModel).SelectedObjects;
            }
        }
    }
}
