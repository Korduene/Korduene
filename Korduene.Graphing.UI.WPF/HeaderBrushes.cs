using System.Windows.Media;

namespace Korduene.Graphing.UI.WPF
{
    public static class HeaderBrushes
    {
        public static LinearGradientBrush NodeBrush
        {
            get
            {
                return GetBrush();
            }
        }

        private static LinearGradientBrush GetBrush()
        {
            return new LinearGradientBrush(Colors.DarkGray, Colors.Transparent, new System.Windows.Point(0, 0.5), new System.Windows.Point(1, 0.5));
        }
    }
}
