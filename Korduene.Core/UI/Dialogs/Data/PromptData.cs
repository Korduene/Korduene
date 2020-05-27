using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Korduene.UI.Dialogs.Data
{
    /// <summary>
    /// PromptData
    /// </summary>
    /// <seealso cref="INotifyPropertyChanged" />
    [Description("PromptData")]
    [DisplayName("PromptData")]
    [DebuggerDisplay("PromptData")]
    public class PromptData : INotifyPropertyChanged
    {
        #region [Events]

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region [Private Objects]

        private string _value;

        #endregion

        #region [Public Properties]

        public string Value
        {
            get { return _value; }
            set
            {
                if (_value != value)
                {
                    _value = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region [Commands]

        #endregion

        #region [Constructors]

        /// <summary>
        /// Initializes a new instance of the <see cref="PromptData"/> class.
        /// </summary>
        public PromptData()
        {
        }

        #endregion

        #region [Public Methods]

        #endregion

        #region [Private Methods]

        #endregion

        /// <summary>
        /// Called when public properties changed.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
