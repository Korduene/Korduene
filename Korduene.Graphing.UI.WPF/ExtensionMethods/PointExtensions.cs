using System.Windows;

namespace Korduene.Graphing.UI.WPF
{
    public static class PointExtensions
    {
        /// <summary>
        /// Converts to graphpoint.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns></returns>
        public static GraphPoint ToGraphPoint(this Point point)
        {
            return new GraphPoint(point.X, point.Y);
        }
    }
}
