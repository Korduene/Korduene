using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;

namespace Korduene.UI.WPF.ToolWindows.ViewModels
{
    /// <summary>
    /// Solution Explorer ViewModel
    /// </summary>
    /// <seealso cref="INotifyPropertyChanged" />
    [Description("Solution Explorer ViewModel")]
    [DisplayName("SolutionExplorerViewModel")]
    [DebuggerDisplay("{Name}")]
    public class SolutionExplorerViewModel : KToolWindow
    {
        #region [Events]

        #endregion

        #region [Private Objects]
        private ICommand _itemDoubleClickCommand;
        private ICommand _refreshSolutionCommand;

        #endregion

        #region [Public Properties]

        public Current Current { get { return Current.Instance; } }

        #endregion

        #region [Commands]

        public ICommand ItemDoubleClickCommand
        {
            get { return _itemDoubleClickCommand ??= new KCommand<object>(ExecuteItemDoubleClickCommand); }
        }

        public ICommand RefreshSolutionCommand
        {
            get { return _refreshSolutionCommand ??= new KCommand(ExecuteRefreshSolutionCommand); }
        }

        #endregion

        #region [Constructors]

        /// <summary>
        /// Initializes a new instance of the <see cref="SolutionExplorerViewModel"/> class.
        /// </summary>
        public SolutionExplorerViewModel()
        {
            this.ContentId = Constants.ContentIds.SOLUTION_EXPLORER;
            this.Name = "Solution Explorer";
        }

        #endregion

        #region [Command Handlers]

        public void ExecuteItemDoubleClickCommand(object parameter)
        {
            if (!(parameter is WorkspaceItem item))
            {
                return;
            }

            GlobalCommands.SolutionExplorerCommands.ExecuteOpenFileCommand();
        }

        public void ExecuteRefreshSolutionCommand()
        {
            Current.UpdateWorkspaceItems();
        }

        #endregion

        #region [Public Methods]

        #endregion

        #region [Private Methods]

        #endregion
    }
}
