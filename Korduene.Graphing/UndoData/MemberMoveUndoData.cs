namespace Korduene.Graphing.UndoData
{
    /// <summary>
    /// Node move undo data
    /// </summary>
    public class MemberMoveUndoData
    {
        /// <summary>
        /// Gets or sets the member.
        /// </summary>
        /// <value>
        /// The member.
        /// </value>
        public IGraphMember Member { get; set; }

        /// <summary>
        /// Gets or sets the x.
        /// </summary>
        /// <value>
        /// The x.
        /// </value>
        public double X { get; set; }

        /// <summary>
        /// Gets or sets the y.
        /// </summary>
        /// <value>
        /// The y.
        /// </value>
        public double Y { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MemberMoveUndoData"/> class.
        /// </summary>
        public MemberMoveUndoData()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MemberMoveUndoData"/> class.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public MemberMoveUndoData(IGraphMember member, double x, double y)
        {
            this.Member = member;
            this.X = x;
            this.Y = y;
        }
    }
}
