using Korduene.UI.Enums;
using System;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace Korduene.UI.WPF
{
    public sealed class UIServices : IUIServices
    {
        private static readonly UIServices _instance = new UIServices();

        public static UIServices Instance { get { return _instance; } }

        public static Window MainWindow { get; set; }

        public OperationResult CreateDialog(string name, params object[] parameters)
        {
            var result = CreateDialog<object>(name, parameters);

            return new OperationResult(result.Success, result.Message) { Data = result.Data };
        }

        public OperationResult<TData> CreateDialog<TData>(string name, params object[] parameters)
        {
            var viewType = ResolveViewType(name);
            var viewModelType = ResolveViewModelType(name);

            var window = Activator.CreateInstance(viewType) as Window;
            window.Owner = MainWindow;

            var viewModel = Activator.CreateInstance(viewModelType, parameters) as ViewModelBase;

            window.DataContext = viewModel;

            if (window.ShowDialog().Value)
            {
                return new OperationResult<TData>(true) { Data = (TData)viewModel.Data };
            }

            return new OperationResult<TData>(false);
        }

        public OperationResult<TData> CreateDialog<TData>(DialogType dialogType, params object[] parameters)
        {
            return CreateDialog<TData>(dialogType.ToString(), parameters);
        }

        public OperationResult CreateDialog(DialogType dialogType, params object[] parameters)
        {
            return CreateDialog(dialogType.ToString(), parameters);
        }

        private static Type ResolveViewType(string name)
        {
            return Assembly.GetAssembly(typeof(UIServices)).GetTypes().FirstOrDefault(x => x.Name == $"{name}View");
        }

        private static Type ResolveViewModelType(string name)
        {
            return Assembly.GetAssembly(typeof(UIServices)).GetTypes().FirstOrDefault(x => x.Name == $"{name}ViewModel");
        }
    }
}
