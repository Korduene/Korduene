using System.Windows;

namespace Korduene.Graphing.UI.WPF
{
    public static class GraphPointExtensions
    {
        /// <summary>
        /// Converts to point.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns></returns>
        public static Point ToPoint(this GraphPoint point)
        {
            return new Point(point.X, point.Y);
        }
    }
}
