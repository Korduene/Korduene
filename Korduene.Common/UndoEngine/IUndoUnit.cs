using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Korduene.Common
{
    public interface IUndoUnit
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; set; }

        /// <summary>
        /// Performs undo operation
        /// </summary>
        void Undo();

        /// <summary>
        /// Called when public properties changed.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        void OnPropertyChanged([CallerMemberName] string name = null);
    }
}