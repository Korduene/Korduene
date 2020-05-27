using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Korduene.Common
{
    /// <summary>
    /// Undo engine
    /// </summary>
    /// <seealso cref="INotifyPropertyChanged" />
    [Description("Undo engine")]
    [DisplayName("UndoEngine")]
    public class UndoEngine : INotifyPropertyChanged, IUndoEngine
    {
        #region [Events]

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Occurs before undoing.
        /// </summary>
        public event EventHandler<IUndoUnit> Undoing;

        /// <summary>
        /// Occurs after undone.
        /// </summary>
        public event EventHandler<IUndoUnit> Undone;
        #endregion

        #region [Private Objects]

        private Stack<IUndoUnit> _undo;
        private Stack<IUndoUnit> _redo;

        #endregion

        #region [Public Properties]

        /// <summary>
        /// Gets the undo stack.
        /// </summary>
        /// <value>
        /// The undo stack.
        /// </value>
        public Stack<IUndoUnit> UndoStack
        {
            get { return _undo; }
        }

        /// <summary>
        /// Gets the redo stack.
        /// </summary>
        /// <value>
        /// The redo stack.
        /// </value>
        public Stack<IUndoUnit> RedoStack
        {
            get { return _redo; }
        }

        /// <summary>
        /// Gets a value indicating whether undo operation is possible, or simply there are any items in the undo stack.
        /// </summary>
        /// <value>
        ///   <c>true</c> if undo operation is possible; otherwise, <c>false</c>.
        /// </value>
        public bool CanUndo
        {
            get { return _undo.Count > 0; }
        }

        /// <summary>
        /// Gets a value indicating whether redo operation is possible, or simply there are any items in the redo stack.
        /// </summary>
        /// <value>
        ///   <c>true</c> if redo operation is possible; otherwise, <c>false</c>.
        /// </value>
        public bool CanRedo
        {
            get { return _redo.Count > 0; }
        }

        #endregion

        #region [Constructors]

        /// <summary>
        /// Initializes a new instance of the <see cref="UndoEngine"/> class.
        /// </summary>
        public UndoEngine()
        {
            _undo = new Stack<IUndoUnit>();
            _redo = new Stack<IUndoUnit>();
        }

        #endregion

        #region [Public Methods]

        /// <summary>
        /// Performs undo operation if there are any items in the undo stack.
        /// </summary>
        public virtual void Undo()
        {
            if (CanUndo)
            {
                var unit = _undo.Pop();
                OnUndoing(unit);

                unit.Undo();
                _redo.Push(unit);

                OnUndone(unit);

                OnPropertyChanged(nameof(CanUndo));
                OnPropertyChanged(nameof(CanRedo));
            }
        }

        /// <summary>
        /// Performs redo operation if there are any items in the redo stack.
        /// </summary>
        public virtual void Redo()
        {
            if (CanRedo)
            {
                var unit = _redo.Pop();
                OnUndoing(unit);

                unit.Undo();
                _undo.Push(unit);

                OnUndone(unit);

                OnPropertyChanged(nameof(CanUndo));
                OnPropertyChanged(nameof(CanRedo));
            }
        }

        /// <summary>
        /// Adds the undo unit to the undo stack.
        /// </summary>
        /// <param name="undoUnit">The undo unit.</param>
        public virtual void AddUndoUnit(IUndoUnit undoUnit)
        {
            _undo.Push(undoUnit);
        }

        /// <summary>
        /// Creates an undo unit.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>Undo unit.</returns>
        public virtual IUndoUnit CreateUndoUnit(string name)
        {
            return new UndoUnit(this, name);
        }

        /// <summary>
        /// Called when before undoing.
        /// </summary>
        /// <param name="undoUnit">The undo unit.</param>
        public virtual void OnUndoing(IUndoUnit undoUnit)
        {
            Undoing?.Invoke(this, undoUnit);
        }

        /// <summary>
        /// Called when after undone.
        /// </summary>
        /// <param name="undoUnit">The undo unit.</param>
        public virtual void OnUndone(IUndoUnit undoUnit)
        {
            Undone?.Invoke(this, undoUnit);
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
