using System.ComponentModel;
using System.Diagnostics;

namespace Korduene.UI
{
    /// <summary>
    /// Korduene ToolWindow
    /// </summary>
    /// <seealso cref="INotifyPropertyChanged" />
    [Description("Korduene ToolWindow")]
    [DisplayName("ToolWindow")]
    [DebuggerDisplay("{this}")]
    public class KToolWindow : DockableBase
    {
        #region [Events]

        #endregion

        #region [Private Objects]

        private int _width;

        #endregion

        #region [Public Properties]

        public int Width
        {
            get { return _width; }
            set
            {
                if (_width != value)
                {
                    _width = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region [Commands]

        #endregion

        #region [Constructors]

        /// <summary>
        /// Initializes a new instance of the <see cref="KToolWindow"/> class.
        /// </summary>
        public KToolWindow()
        {
            Width = 200;
        }

        #endregion

        #region [Public Methods]

        #endregion

        #region [Private Methods]

        #endregion
    }
}
