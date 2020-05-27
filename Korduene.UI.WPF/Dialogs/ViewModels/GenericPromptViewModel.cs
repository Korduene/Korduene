using Korduene.UI.Dialogs.Data;
using System.ComponentModel;
using System.Diagnostics;

namespace Korduene.UI.WPF.Dialogs.ViewModels
{
    /// <summary>
    /// GenericPrompt ViewModel
    /// </summary>
    /// <seealso cref="INotifyPropertyChanged" />
    [Description("GenericPrompt ViewModel")]
    [DisplayName("GenericPrompt ViewModel")]
    [DebuggerDisplay("GenericPromptViewModel")]
    public class GenericPromptViewModel : ViewModelBase
    {
        #region [Events]

        #endregion

        #region [Private Objects]

        private PromptData _promptData;

        #endregion

        #region [Public Properties]

        public override object Data => _promptData;

        #endregion

        #region [Commands]

        #endregion

        #region [Constructors]

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericPromptViewModel"/> class.
        /// </summary>
        public GenericPromptViewModel(string title, string value)
        {
            Title = title;
            _promptData = new PromptData() { Value = value };
        }

        #endregion

        #region [Public Methods]

        #endregion

        #region [Private Methods]

        #endregion
    }
}
