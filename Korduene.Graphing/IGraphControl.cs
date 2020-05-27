namespace Korduene.Graphing
{
    /// <summary>
    /// Graph Control interface
    /// </summary>
    public interface IGraphControl
    {
        /// <summary>
        /// Gets or sets the offset.
        /// </summary>
        /// <value>
        /// The offset.
        /// </value>
        GraphPoint Offset { get; set; }

        /// <summary>
        /// Gets or sets the zoom.
        /// </summary>
        /// <value>
        /// The zoom.
        /// </value>
        double Zoom { get; set; }

        /// <summary>
        /// Gets the view center.
        /// </summary>
        /// <value>
        /// The view center.
        /// </value>
        GraphPoint ViewCenter { get; }

        /// <summary>
        /// Gets the graph center.
        /// </summary>
        /// <value>
        /// The graph center.
        /// </value>
        GraphPoint GraphCenter { get; }

        /// <summary>
        /// Centers the graph.
        /// </summary>
        void CenterGraph();

        /// <summary>
        /// Centers the graph.
        /// </summary>
        /// <param name="zoom">The zoom.</param>
        void CenterGraph(double zoom);
    }
}
