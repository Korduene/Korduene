using Korduene.Graphing.Collections;
using System;
using System.Collections.Generic;

namespace Korduene.Graphing
{
    public interface INode : IGraphMember
    {
        /// <summary>
        /// Occurs when port changed.
        /// </summary>
        event EventHandler<PortChangedEventArgs> PortChanged;

        /// <summary>
        /// Occurs when node changed.
        /// </summary>
        event EventHandler<NodeChangedEventArgs> NodeChanged;

        /// <summary>
        /// Occurs when node is selected/unselected.
        /// </summary>
        event EventHandler SelectionChanged;

        /// <summary>
        /// Gets the ports.
        /// </summary>
        PortCollection Ports { get; }

        /// <summary>
        /// Gets the connected nodes.
        /// </summary>
        /// <value>
        /// The connected nodes.
        /// </value>
        IEnumerable<INode> ConnectedNodes { get; }

        /// <summary>
        /// Gets the parent nodes.
        /// </summary>
        /// <value>
        /// The parent nodes.
        /// </value>
        IEnumerable<INode> ParentNodes { get; }

        /// <summary>
        /// Gets a value indicating whether this node is connected.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this node is connected; otherwise, <c>false</c>.
        /// </value>
        bool IsConnected { get; }

        /// <summary>
        /// Gets the child nodes.
        /// </summary>
        /// <value>
        /// The child nodes.
        /// </value>
        IEnumerable<INode> ChildNodes { get; }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        string Comment { get; set; }

        #region [Visuals]

        /// <summary>
        /// Gets or sets the color of the text.
        /// </summary>
        /// <value>
        /// The color of the text.
        /// </value>
        GraphColor TextColor { get; set; }

        /// <summary>
        /// Gets or sets the color of the header.
        /// </summary>
        /// <value>
        /// The color of the header.
        /// </value>
        GraphColor HeaderColor { get; set; }

        /// <summary>
        /// Gets or sets the color of the border.
        /// </summary>
        /// <value>
        /// The color of the border.
        /// </value>
        GraphColor BorderColor { get; set; }

        /// <summary>
        /// Gets or sets the color of the background.
        /// </summary>
        /// <value>
        /// The color of the background.
        /// </value>
        GraphColor BackgroundColor { get; set; }

        /// <summary>
        /// Gets or sets the opacity.
        /// </summary>
        /// <value>
        /// The opacity.
        /// </value>
        double Opacity { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this node is visible.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this node is visible; otherwise, <c>false</c>.
        /// </value>
        bool IsVisible { get; set; }

        #endregion

        /// <summary>
        /// Gets a value indicating whether this node's comment visible.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this node's comment visible; otherwise, <c>false</c>.
        /// </value>
        bool IsCommentVisible { get; }

        /// <summary>
        /// Overlaps the specified node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns></returns>
        bool Overlaps(INode node);

        /// <summary>
        /// Determines whether this node connected to the specified node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns>
        ///   <c>true</c> if this node connected to the specified node; otherwise, <c>false</c>.
        /// </returns>
        bool IsConnectedTo(INode node);

        /// <summary>
        /// Destroys this node.
        /// </summary>
        void Destroy();

        /// <summary>
        /// Gets the top (Y).
        /// </summary>
        /// <returns></returns>
        double GetTop();

        /// <summary>
        /// Gets the bottom (Y + Height).
        /// </summary>
        /// <returns></returns>
        double GetBottom();

        /// <summary>
        /// Gets the left (X).
        /// </summary>
        /// <returns></returns>
        double GetLeft();

        /// <summary>
        /// Gets the right (X + Width).
        /// </summary>
        /// <returns></returns>
        double GetRight();
    }
}