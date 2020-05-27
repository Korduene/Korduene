using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Korduene.Common
{
    /// <summary>
    /// Undo Unit
    /// </summary>
    /// <seealso cref="INotifyPropertyChanged" />
    [Description("Undo Unit")]
    [DisplayName("UndoUnit")]
    public class UndoUnit : INotifyPropertyChanged, IUndoUnit
    {
        #region [Events]

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region [Private Objects]

        private string _name;
        private IUndoEngine _undoEngine;

        #endregion

        #region [Public Properties]

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region [Constructors]

        /// <summary>
        /// Initializes a new instance of the <see cref="UndoUnit" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public UndoUnit(string name)
        {
            this.Name = name;
        }

        public UndoUnit(IUndoEngine undoEngine, string name) : this(name)
        {
            _undoEngine = undoEngine;
        }

        #endregion

        #region [Public Methods]

        /// <summary>
        /// Performs undo operation
        /// </summary>
        public virtual void Undo()
        {
        }

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
