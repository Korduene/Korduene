using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Korduene.Graphing
{
    /// <summary>
    /// Graph Point
    /// </summary>
    /// <seealso cref="INotifyPropertyChanged" />
    [Description("Graph Point")]
    //[DisplayName("GraphPoint")]
    [DebuggerDisplay("X={X}, Y={Y}")]
    public struct GraphPoint : INotifyPropertyChanged
    {
        #region [Events]

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region [Private Objects]

        private double _x;
        private double _y;

        #endregion

        #region [Public Properties]

        /// <summary>
        /// Gets or sets the x.
        /// </summary>
        /// <value>
        /// The x.
        /// </value>
        public double X
        {
            get { return _x; }
            set
            {
                if (_x != value)
                {
                    _x = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the y.
        /// </summary>
        /// <value>
        /// The y.
        /// </value>
        public double Y
        {
            get { return _y; }
            set
            {
                if (_y != value)
                {
                    _y = value;
                    OnPropertyChanged();
                }
            }
        }

        public static GraphPoint Empty = new GraphPoint(0, 0);

        #endregion

        #region [Constructors]

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphPoint"/> struct.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public GraphPoint(double x, double y) : this()
        {
            this.X = x;
            this.Y = y;
        }

        #endregion

        #region [Public Methods]

        #endregion

        #region [Private Methods]

        #endregion

        #region [Public Static Methods]

        /// <summary>
        /// Implements the operator -.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="point2">The point2.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static GraphPoint operator -(GraphPoint point1, GraphPoint point2)
        {
            return new GraphPoint(point1.X - point2.X, point1.Y - point2.Y);
        }

        public static GraphPoint operator +(GraphPoint point1, GraphPoint point2)
        {
            return new GraphPoint(point1.X + point2.X, point1.Y + point2.Y);
        }

        public static bool operator ==(GraphPoint point1, GraphPoint point2)
        {
            if (object.ReferenceEquals(point1, null))
            {
                return object.ReferenceEquals(point2, null);
            }

            return point1.X == point2.X && point1.Y == point2.Y;
        }

        public static bool operator !=(GraphPoint point1, GraphPoint point2)
        {
            return !(point1 == point2);
        }

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
