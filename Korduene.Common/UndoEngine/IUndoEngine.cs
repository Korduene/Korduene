using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Korduene.Common
{
    public interface IUndoEngine
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Occurs before undoing.
        /// </summary>
        event EventHandler<IUndoUnit> Undoing;

        /// <summary>
        /// Occurs after undone.
        /// </summary>
        event EventHandler<IUndoUnit> Undone;

        /// <summary>
        /// Gets the undo stack.
        /// </summary>
        /// <value>
        /// The undo stack.
        /// </value>
        Stack<IUndoUnit> UndoStack { get; }

        /// <summary>
        /// Gets the redo stack.
        /// </summary>
        /// <value>
        /// The redo stack.
        /// </value>
        Stack<IUndoUnit> RedoStack { get; }

        /// <summary>
        /// Gets a value indicating whether undo operation is possible, or simply there are any items in the undo stack.
        /// </summary>
        /// <value>
        ///   <c>true</c> if undo operation is possible; otherwise, <c>false</c>.
        /// </value>
        bool CanUndo { get; }

        /// <summary>
        /// Gets a value indicating whether redo operation is possible, or simply there are any items in the redo stack.
        /// </summary>
        /// <value>
        ///   <c>true</c> if redo operation is possible; otherwise, <c>false</c>.
        /// </value>
        bool CanRedo { get; }

        /// <summary>
        /// Performs undo operation if there are any items in the undo stack.
        /// </summary>
        void Undo();

        /// <summary>
        /// Performs redo operation if there are any items in the redo stack.
        /// </summary>
        void Redo();

        /// <summary>
        /// Adds the undo unit to the undo stack.
        /// </summary>
        /// <param name="undoUnit">The undo unit.</param>
        void AddUndoUnit(IUndoUnit undoUnit);

        /// <summary>
        /// Creates an undo unit.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>Undo unit.</returns>
        IUndoUnit CreateUndoUnit(string name);

        /// <summary>
        /// Called when before undoing.
        /// </summary>
        /// <param name="undoUnit">The undo unit.</param>
        void OnUndoing(IUndoUnit undoUnit);

        /// <summary>
        /// Called when after undone.
        /// </summary>
        /// <param name="undoUnit">The undo unit.</param>
        void OnUndone(IUndoUnit undoUnit);

        /// <summary>
        /// Called when public properties changed.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        void OnPropertyChanged([CallerMemberName] string name = null);
    }
}