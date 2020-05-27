using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

namespace Korduene.UI
{
    /// <summary>
    /// KDocument
    /// </summary>
    /// <seealso cref="INotifyPropertyChanged" />
    [Description("KDocument")]
    [DisplayName("KDocument")]
    [DebuggerDisplay("KDocument")]
    public class KDocument : KDocumentBase
    {
        #region [Events]

        #endregion

        #region [Private Objects]

        private ICommand _closeCommand;
        private ICommand _closeAllCommand;
        private ICommand _closeAllButThisCommand;

        #endregion

        #region [Public Properties]

        #endregion

        #region [Commands]

        public ICommand CloseCommand => _closeCommand ??= new KCommand(ExecuteCloseCommand);

        public ICommand CloseAllCommand => _closeAllCommand ??= new KCommand(ExecuteCloseAllCommand);

        public ICommand CloseAllButThisCommand => _closeAllButThisCommand ??= new KCommand(ExecuteCloseAllButThisCommand);

        #endregion

        #region [Constructors]

        /// <summary>
        /// Initializes a new instance of the <see cref="KDocument"/> class.
        /// </summary>
        public KDocument()
        {
        }

        #endregion

        #region [Command Handlers]

        public void ExecuteCloseCommand()
        {
            Current.Instance.OpenDocuments.Remove(this);
        }

        public void ExecuteCloseAllCommand()
        {
            Current.Instance.OpenDocuments.Clear();
        }

        public void ExecuteCloseAllButThisCommand()
        {
            foreach (var item in Current.Instance.OpenDocuments.Where(x => x != this).ToList())
            {
                Current.Instance.OpenDocuments.Remove(item);
            }
        }

        #endregion

        #region [Public Methods]

        #endregion

        #region [Private Methods]

        #endregion
    }
}
