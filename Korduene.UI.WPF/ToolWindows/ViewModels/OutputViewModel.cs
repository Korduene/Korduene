using System.ComponentModel;
using System.Diagnostics;

namespace Korduene.UI.WPF.ToolWindows.ViewModels
{
    /// <summary>
    /// Output ViewModel
    /// </summary>
    /// <seealso cref="INotifyPropertyChanged" />
    [Description("Output ViewModel")]
    [DisplayName("OutputViewModel")]
    [DebuggerDisplay("{Name}")]
    public class OutputViewModel : KToolWindow
    {
        #region [Events]

        #endregion

        #region [Private Objects]

        #endregion

        #region [Public Properties]

        #endregion

        #region [Commands]

        #endregion

        #region [Constructors]

        /// <summary>
        /// Initializes a new instance of the <see cref="OutputViewModel"/> class.
        /// </summary>
        public OutputViewModel()
        {
            this.ContentId = Constants.ContentIds.OUTPUT;
            this.Name = "Output";
        }

        #endregion

        #region [Command Handlers]

        #endregion

        #region [Public Methods]

        #endregion

        #region [Private Methods]

        #endregion
    }
}
