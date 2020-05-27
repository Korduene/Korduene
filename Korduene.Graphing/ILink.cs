using System;

namespace Korduene.Graphing
{
    public interface ILink : IGraphMember
    {
        /// <summary>
        /// Gets or sets the unique identifier.
        /// </summary>
        /// <value>
        /// The unique identifier.
        /// </value>
        Guid Guid { get; set; }

        /// <summary>
        /// Gets or sets the in port.
        /// </summary>
        /// <value>
        /// The in port.
        /// </value>
        IPort StartPort { get; set; }

        /// <summary>
        /// Gets or sets the out port.
        /// </summary>
        /// <value>
        /// The out port.
        /// </value>
        IPort EndPort { get; set; }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>
        /// The color.
        /// </value>
        GraphColor Color { get; set; }

        /// <summary>
        /// Gets or sets the color of the second.
        /// </summary>
        /// <value>
        /// The color of the second.
        /// </value>
        GraphColor SecondColor { get; set; }

        /// <summary>
        /// Gets or sets the start point.
        /// </summary>
        /// <value>
        /// The start point.
        /// </value>
        GraphPoint StartPoint { get; set; }

        /// <summary>
        /// Gets or sets the middle point.
        /// </summary>
        /// <value>
        /// The middle point.
        /// </value>
        GraphPoint Point1 { get; set; }

        /// <summary>
        /// Gets or sets the end point.
        /// </summary>
        /// <value>
        /// The end point.
        /// </value>
        GraphPoint Point2 { get; set; }

        /// <summary>
        /// Gets or sets the point3.
        /// </summary>
        /// <value>
        /// The point3.
        /// </value>
        GraphPoint Point3 { get; set; }

        /// <summary>
        /// Destroys this instance.
        /// </summary>
        void Destroy();
    }
}
