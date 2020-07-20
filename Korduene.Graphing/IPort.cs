using Korduene.Graphing.Enums;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Korduene.Graphing
{
    public interface IPort : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        string Id { get; set; }

        /// <summary>
        /// Gets the type of the port.
        /// </summary>
        PortType PortType { get; }

        /// <summary>
        /// Gets or sets a value indicating whether this port is pass through.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this port is pass through; otherwise, <c>false</c>.
        /// </value>
        bool IsPassThrough { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this port is a collection of specified type.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this port is a collection of specified type; otherwise, <c>false</c>.
        /// </value>
        bool IsCollection { get; set; }

        /// <summary>
        /// Gets a value indicating whether this port is connected.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this port is connected; otherwise, <c>false</c>.
        /// </value>
        bool IsConnected { get ; }

        /// <summary>
        /// The amount of accepted connections for this port.
        /// </summary>
        /// <value>
        /// The amount of accepted connections for this port.
        /// </value>
        AcceptedConnections AcceptedConnections { get; }

        /// <summary>
        /// Gets or sets the parent node.
        /// </summary>
        INode ParentNode { get; set; }

        /// <summary>
        /// Gets the index of this port in the parent node port collection.
        /// </summary>
        int Index { get; }

        /// <summary>
        /// Gets the index of the row.
        /// </summary>
        int RowIndex { get; }

        /// <summary>
        /// Gets a value indicating whether this port has an editor.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this port has an editor; otherwise, <c>false</c>.
        /// </value>
        bool HasEditor { get; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        /// <value>
        /// The comment.
        /// </value>
        string Comment { get; set; }

        /// <summary>
        /// Gets or sets the type of the data.
        /// </summary>
        /// <value>
        /// The type of the data.
        /// </value>
        object DataType { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        object Value { get; set; }

        /// <summary>
        /// Gets the properties.
        /// </summary>
        /// <value>
        /// The properties.
        /// </value>
        Dictionary<string, object> Properties { get; }

        /// <summary>
        /// Determines whether port has property with the specified name.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>
        ///   <c>true</c> if port has property with the specified name; otherwise, <c>false</c>.
        /// </returns>
        bool HasProperty(string propertyName);

        /// <summary>
        /// Gets the connected ports.
        /// </summary>
        /// <value>
        /// The connected ports.
        /// </value>
        ObservableCollection<IPort> ConnectedPorts { get; }

        #region [Visuals]

        /// <summary>
        /// Gets or sets the color of the text.
        /// </summary>
        /// <value>
        /// The color of the text.
        /// </value>
        GraphColor TextColor { get; set; }

        /// <summary>
        /// Gets or sets the color of the border.
        /// </summary>
        /// <value>
        /// The color of the border.
        /// </value>
        GraphColor BorderColor { get; set; }

        /// <summary>
        /// Gets or sets the port fill color.
        /// </summary>
        /// <value>
        /// The port fill color.
        /// </value>
        GraphColor FillColor { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this port is visible.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this port is visible; otherwise, <c>false</c>.
        /// </value>
        bool IsVisible { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        GraphPoint Location { get; set; }

        /// <summary>
        /// Determines whether this port can connect the specified port.
        /// </summary>
        /// <param name="port">The port.</param>
        /// <returns>
        ///   <c>true</c> if this port can connect the specified port; otherwise, <c>false</c>.
        /// </returns>
        bool CanConnect(IPort port);

        /// <summary>
        /// Determines whether this port is connected to the specified port.
        /// </summary>
        /// <param name="port">The port.</param>
        /// <returns>
        ///   <c>true</c> if this port is connected to the specified port; otherwise, <c>false</c>.
        /// </returns>
        bool IsConnectedTo(IPort port);

        /// <summary>
        /// Connects the specified port.
        /// </summary>
        /// <param name="port">The port.</param>
        /// <returns></returns>
        ConnectionResult Connect(IPort port);

        /// <summary>
        /// Disconnects the specified port.
        /// </summary>
        /// <param name="port">The port.</param>
        void Disconnect(IPort port);

        /// <summary>
        /// Disconnects all.
        /// </summary>
        void Disconnect();

        #endregion

        /// <summary>
        /// Called when property changed.
        /// </summary>
        /// <param name="name">The property name.</param>
        void OnPropertyChanged([CallerMemberName] string name = null);
    }
}