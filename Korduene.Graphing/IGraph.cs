using Korduene.Common;
using Korduene.Graphing.Collections;
using Korduene.Graphing.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Korduene.Graphing
{
    public interface IGraph : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when node changed.
        /// </summary>
        event EventHandler<NodeChangedEventArgs> NodeChanged;

        /// <summary>
        /// Occurs when a node is selected/unselected.
        /// </summary>
        event EventHandler<INode> NodeSelectionChanged;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is loading.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is loading; otherwise, <c>false</c>.
        /// </value>
        bool IsLoading { get; set; }

        /// <summary>
        /// Gets or sets the offset.
        /// </summary>
        /// <value>
        /// The offset.
        /// </value>
        GraphPoint Offset { get; set; }

        /// <summary>
        /// Gets or sets the startup offset.
        /// </summary>
        /// <value>
        /// The startup offset.
        /// </value>
        GraphPoint StartupOffset { get; set; }

        /// <summary>
        /// Gets or sets the view center.
        /// </summary>
        /// <value>
        /// The view center.
        /// </value>
        GraphPoint ViewCenter { get; set; }

        /// <summary>
        /// Gets or sets the zoom.
        /// </summary>
        /// <value>
        /// The zoom.
        /// </value>
        double Zoom { get; set; }

        /// <summary>
        /// Gets or sets the mouse down port.
        /// </summary>
        /// <value>
        /// The mouse down port.
        /// </value>
        IPort MouseDownPort { get; set; }

        /// <summary>
        /// Gets or sets the mouse up port.
        /// </summary>
        /// <value>
        /// The mouse up port.
        /// </value>
        IPort MouseUpPort { get; set; }

        /// <summary>
        /// Gets or sets the mouse position.
        /// </summary>
        /// <value>
        /// The mouse position.
        /// </value>
        GraphPoint MousePosition { get; set; }

        /// <summary>
        /// Gets or sets the last mouse down position.
        /// </summary>
        /// <value>
        /// The last mouse down position.
        /// </value>
        GraphPoint LastMouseDownPosition { get; set; }

        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        GraphState State { get; }

        /// <summary>
        /// Gets the undo engine.
        /// </summary>
        /// <value>
        /// The undo engine.
        /// </value>
        IUndoEngine UndoEngine { get; }

        /// <summary>
        /// Gets the members.
        /// </summary>
        /// <value>
        /// The members.
        /// </value>
        GraphMemberCollection Members { get; }

        /// <summary>
        /// Gets the nodes.
        /// </summary>
        /// <value>
        /// The nodes.
        /// </value>
        IEnumerable<INode> Nodes { get; }

        /// <summary>
        /// Gets the comments.
        /// </summary>
        /// <value>
        /// The comments.
        /// </value>
        IEnumerable<IComment> Comments { get; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the file path.
        /// </summary>
        /// <value>
        /// The file path.
        /// </value>
        string FilePath { get; set; }

        /// <summary>
        /// Gets or sets the linking link.
        /// </summary>
        /// <value>
        /// The linking link.
        /// </value>
        ILink LinkingLink { get; set; }

        /// <summary>
        /// Creates a connection between the two ports <see cref="MouseDownPort"/> and <see cref="MouseUpPort"/>.
        /// </summary>
        /// <returns></returns>
        ConnectionResult CreateConnection();

        /// <summary>
        /// Destroys the current link.
        /// </summary>
        void DestroyCurrentLink();

        /// <summary>
        /// Gets the port.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="portType">Type of the port.</param>
        /// <returns></returns>
        IPort GetPort(string id, PortType portType);

        /// <summary>
        /// Arranges the nodes.
        /// </summary>
        void ArrangeNodes();

        /// <summary>
        /// Brings to view.
        /// </summary>
        void BringToView();

        /// <summary>
        /// Serializes to json.
        /// </summary>
        /// <returns></returns>
        string SerializeToJson();

        /// <summary>
        /// Deserializes this instance.
        /// </summary>
        /// <param name="graphType">Type of the graph.</param>
        /// <param name="json">The json.</param>
        //void Deserialize(Type graphType, string json);

        /// <summary>
        /// Brings to view.
        /// </summary>
        /// <param name="nodes">The nodes.</param>
        void BringToView(IEnumerable<INode> nodes);

        /// <summary>
        /// Called when property changed.
        /// </summary>
        /// <param name="name">The name.</param>
        void OnPropertyChanged([CallerMemberName] string name = null);
    }
}