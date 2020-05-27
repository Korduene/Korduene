using Korduene.Graphing.Enums;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Korduene.Graphing
{
    public interface IGraphMember : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets the parent graph.
        /// </summary>
        /// <value>
        /// The parent graph.
        /// </value>
        IGraph ParentGraph { set; get; }

        /// <summary>
        /// Gets the type of the member.
        /// </summary>
        /// <value>
        /// The type of the member.
        /// </value>
        MemberType MemberType { get; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        string Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; set; }

        /// <summary>
        /// Gets the default name.
        /// </summary>
        /// <value>
        /// The default name.
        /// </value>
        string DefaultName { get; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is selected.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is selected; otherwise, <c>false</c>.
        /// </value>
        bool IsSelected { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        GraphPoint Location { get; set; }

        /// <summary>
        /// Gets or sets the last location (internal use only).
        /// </summary>
        /// <value>
        /// The last location.
        /// </value>
        GraphPoint LastLocation { get; set; }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        double Width { get; set; }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        double Height { get; set; }

        /// <summary>
        /// Gets or sets Z index of the member.
        /// </summary>
        /// <value>
        /// The Z index of the member.
        /// </value>
        int ZIndex { get; set; }

        /// <summary>
        /// Gets or sets the element.
        /// </summary>
        object VisualContainer { get; set; }

        /// <summary>
        /// Updates the visuals.
        /// </summary>
        void UpdateVisuals();

        /// <summary>
        /// Called when property changed.
        /// </summary>
        /// <param name="name">The property name.</param>
        void OnPropertyChanged([CallerMemberName] string name = null);
    }
}
