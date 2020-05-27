using Korduene.UI.WPF.ToolWindows.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

namespace Korduene.UI.WPF.Documents.ViewModels
{
    /// <summary>
    /// Main Windows ViewModel
    /// </summary>
    /// <seealso cref="INotifyPropertyChanged" />
    [Description("Main Windows ViewModel")]
    [DisplayName("MainWindowViewModel")]
    [DebuggerDisplay("{this}")]
    public class MainWindowViewModel : ViewModelBase
    {
        #region [Events]

        #endregion

        #region [Private Objects]

        private ObservableCollection<KToolWindow> _toolWindows;
        private ICommand _newSolutionCommand;
        private ICommand _openSolutionCommand;
        private ICommand _startAppCommand;
        private ICommand _saveCommand;
        private ICommand _saveAllCommand;
        private ICommand _viewSolutionExplorerCommand;
        private ICommand _viewPropertiesCommand;
        private ICommand _viewToolBoxCommand;
        private ICommand _viewOutputCommand;

        #endregion

        #region [Public Properties]

        /// <summary>
        /// Gets or sets the tool windows.
        /// </summary>
        /// <value>
        /// The tool windows.
        /// </value>
        public ObservableCollection<KToolWindow> ToolWindows
        {
            get { return _toolWindows ??= new ObservableCollection<KToolWindow>(); }
            set
            {
                if (_toolWindows != value)
                {
                    _toolWindows = value;
                    OnPropertyChanged();
                }
            }
        }

        public Current Current { get { return Current.Instance; } }

        public ObservableCollection<KDocument> OpenDocuments { get { return Current.Instance.OpenDocuments; } }

        #endregion

        #region [Commands]

        public ICommand NewSolutionCommand
        {
            get { return _newSolutionCommand ??= new KCommand(ExecuteNewSolutionCommand); }
        }

        public ICommand OpenSolutionCommand
        {
            get { return _openSolutionCommand ??= new KCommand(ExecuteOpenSolutionCommand); }
        }

        public ICommand StartAppCommand
        {
            get { return _startAppCommand ??= new KCommand(ExecuteStartAppCommand, CanExecuteStartAppCommand); }
        }

        public ICommand SaveCommand
        {
            get { return _saveCommand ??= new KCommand(ExecuteSaveCommand); }
        }

        public ICommand SaveAllCommand
        {
            get { return _saveAllCommand ??= new KCommand(ExecuteSaveAllCommand); }
        }

        public ICommand ViewSolutionExplorerCommand
        {
            get { return _viewSolutionExplorerCommand ??= new KCommand(ExecuteViewSolutionExplorerCommand); }
        }

        public ICommand ViewPropertiesCommand
        {
            get { return _viewPropertiesCommand ??= new KCommand(ExecuteViewPropertiesCommand); }
        }

        public ICommand ViewToolBoxCommand
        {
            get { return _viewToolBoxCommand ??= new KCommand(ExecuteViewToolBoxCommand); }
        }

        public ICommand ViewOutputCommand
        {
            get { return _viewOutputCommand ??= new KCommand(ExecuteViewOutputCommand); }
        }

        #endregion

        #region [Constructors]

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
        /// </summary>
        public MainWindowViewModel()
        {
        }

        #endregion

        #region [Command Handlers]

        public void ExecuteSaveCommand()
        {
            foreach (var item in Current.Instance.OpenDocuments.Where(x => x.IsActive))
            {
                item.Save();
            }
        }

        public void ExecuteSaveAllCommand()
        {
            foreach (var item in Current.Instance.OpenDocuments)
            {
                item.Save();
            }
        }

        public void ExecuteNewSolutionCommand()
        {
            GlobalCommands.SolutionCommands.ExecuteNewProjectCommand();
        }

        public void ExecuteOpenSolutionCommand()
        {
            var dlg = new OpenFileDialog
            {
                Filter = "Solution File|*.sln"
            };

            if (dlg.ShowDialog().Value)
            {
                Current.Instance.OpenSolution(dlg.FileName);
            }
        }

        public void ExecuteStartAppCommand()
        {
            ExecuteSaveAllCommand();
            Current.Start();
        }

        private bool CanExecuteStartAppCommand(object obj)
        {
            return true;
        }

        public void ExecuteViewOutputCommand()
        {
            if (GetOpenToolWindow(Constants.ContentIds.OUTPUT) is KToolWindow tool)
            {
                tool.IsActive = true;
            }
            else
            {
                ToolWindows.Add(new OutputViewModel());
            }
        }

        public void ExecuteViewToolBoxCommand()
        {
            if (GetOpenToolWindow(Constants.ContentIds.TOOLBOX) is KToolWindow tool)
            {
                tool.IsActive = true;
            }
            else
            {
                ToolWindows.Add(new ToolBoxViewModel());
            }
        }

        public void ExecuteViewPropertiesCommand()
        {
            if (GetOpenToolWindow(Constants.ContentIds.PROPERTIES) is KToolWindow tool)
            {
                tool.IsActive = true;
            }
            else
            {
                ToolWindows.Add(new PropertiesViewModel());
            }
        }

        public void ExecuteViewSolutionExplorerCommand()
        {
            if (GetOpenToolWindow(Constants.ContentIds.SOLUTION_EXPLORER) is KToolWindow tool)
            {
                tool.IsActive = true;
            }
            else
            {
                ToolWindows.Add(new SolutionExplorerViewModel());
            }
        }

        #endregion

        #region [Public Methods]

        public KToolWindow GetOpenToolWindow(string contentId)
        {
            return ToolWindows.FirstOrDefault(x => x.ContentId.Equals(contentId, StringComparison.OrdinalIgnoreCase));
        }

        public bool IsToolWindowOpen(string contentId)
        {
            return ToolWindows.Any(x => x.ContentId.Equals(contentId, StringComparison.OrdinalIgnoreCase));
        }

        public void AddToolsInternal(KToolWindow kToolWindow)
        {
            _toolWindows.Add(kToolWindow);
        }

        #endregion

        #region [Private Methods]

        #endregion
    }
}
