namespace Korduene.Graphing
{
    public static class Constants
    {
        public const double NODE_CORNER_RADIUS = 5;
        public const int NODE_BORDER_THICKNESS = 1;
        public const int PORT_WIDTH_HEIGHT = 14;
        public const int PORT_INNER_WIDTH_HEIGHT = 7;
        public const int PORT_BORDER_THICKNESS = 1;
        public const int NODE_MIN_WIDTH = 140;
        public const int NODE_MIN_HEIGHT = 40;
        public const int NODE_HEADER_PADDING = 3;
        public const int NODE_ZINDEX = 2;
        public const int LINK_ZINDEX = 1;
        public const int LINK_OVERLAY_ZINDEX = 2;

        public static GraphColor COLOR_BACKGROUND_FILL = new GraphColor(255, 40, 40, 40); //"#282828";
        public static GraphColor COLOR_TEXT = new GraphColor(255, 255, 255, 255); // "#fff";
        public static GraphColor COMMENT_HEADER = new GraphColor(40, 40, 40, 40); // "#fff";
        public static GraphColor COMMENT_BORDER = new GraphColor(255, 255, 255, 255); // "#fff";
        public static GraphColor COLOR_BORDER = new GraphColor(255, 0, 160, 0); // "#00a000";
        public static GraphColor COLOR_NODE_FILL = new GraphColor(255, 40, 40, 40); //"#282828";
        public static GraphColor COLOR_NODE_HEADER_FILL = new GraphColor(255, 80, 80, 80); //"#505050";
        public static GraphColor COLOR_SELECTED = new GraphColor(255, 255, 255, 255); //#fff;
        public static GraphColor COLOR_LINK = new GraphColor(255, 0, 0, 0); //#000;

        public static double NODE_OPACITY = 0.9;
        //public static double NODE_OPACITY = 0.6; //default
    }
}
