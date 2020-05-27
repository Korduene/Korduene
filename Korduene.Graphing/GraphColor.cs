using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Korduene.Graphing
{
    /// <summary>
    /// Graph color
    /// </summary>
    /// <seealso cref="INotifyPropertyChanged" />
    [Description("Graph color")]
    //[DisplayName("GraphColor")]
    [DebuggerDisplay("ARGB = {A},{R},{G},{B}")]
    public struct GraphColor : INotifyPropertyChanged
    {
        #region [Events]

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region [Private Objects]

        private byte _a;
        private byte _r;
        private byte _g;
        private byte _b;

        #endregion

        #region [Public Properties]

        /// <summary>
        /// Gets or sets a.
        /// </summary>
        /// <value>
        /// a.
        /// </value>
        public byte A
        {
            get { return _a; }
            set
            {
                if (_a != value)
                {
                    _a = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the r.
        /// </summary>
        /// <value>
        /// The r.
        /// </value>
        public byte R
        {
            get { return _r; }
            set
            {
                if (_r != value)
                {
                    _r = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the g.
        /// </summary>
        /// <value>
        /// The g.
        /// </value>
        public byte G
        {
            get { return _g; }
            set
            {
                if (_g != value)
                {
                    _g = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the b.
        /// </summary>
        /// <value>
        /// The b.
        /// </value>
        public byte B
        {
            get { return _b; }
            set
            {
                if (_b != value)
                {
                    _b = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region [Defined Colors]

        ///<summary>
        /// AliceBlue, ARGB=255, 240, 248, 255
        ///</summary>
        public static GraphColor AliceBlue { get { return new GraphColor(255, 240, 248, 255); } }

        ///<summary>
        /// AntiqueWhite, ARGB=255, 250, 235, 215
        ///</summary>
        public static GraphColor AntiqueWhite { get { return new GraphColor(255, 250, 235, 215); } }

        ///<summary>
        /// Aqua, ARGB=255, 0, 255, 255
        ///</summary>
        public static GraphColor Aqua { get { return new GraphColor(255, 0, 255, 255); } }

        ///<summary>
        /// Aquamarine, ARGB=255, 127, 255, 212
        ///</summary>
        public static GraphColor Aquamarine { get { return new GraphColor(255, 127, 255, 212); } }

        ///<summary>
        /// Azure, ARGB=255, 240, 255, 255
        ///</summary>
        public static GraphColor Azure { get { return new GraphColor(255, 240, 255, 255); } }

        ///<summary>
        /// Beige, ARGB=255, 245, 245, 220
        ///</summary>
        public static GraphColor Beige { get { return new GraphColor(255, 245, 245, 220); } }

        ///<summary>
        /// Bisque, ARGB=255, 255, 228, 196
        ///</summary>
        public static GraphColor Bisque { get { return new GraphColor(255, 255, 228, 196); } }

        ///<summary>
        /// Black, ARGB=255, 0, 0, 0
        ///</summary>
        public static GraphColor Black { get { return new GraphColor(255, 0, 0, 0); } }

        ///<summary>
        /// BlanchedAlmond, ARGB=255, 255, 235, 205
        ///</summary>
        public static GraphColor BlanchedAlmond { get { return new GraphColor(255, 255, 235, 205); } }

        ///<summary>
        /// Blue, ARGB=255, 0, 0, 255
        ///</summary>
        public static GraphColor Blue { get { return new GraphColor(255, 0, 0, 255); } }

        ///<summary>
        /// BlueViolet, ARGB=255, 138, 43, 226
        ///</summary>
        public static GraphColor BlueViolet { get { return new GraphColor(255, 138, 43, 226); } }

        ///<summary>
        /// Brown, ARGB=255, 165, 42, 42
        ///</summary>
        public static GraphColor Brown { get { return new GraphColor(255, 165, 42, 42); } }

        ///<summary>
        /// BurlyWood, ARGB=255, 222, 184, 135
        ///</summary>
        public static GraphColor BurlyWood { get { return new GraphColor(255, 222, 184, 135); } }

        ///<summary>
        /// CadetBlue, ARGB=255, 95, 158, 160
        ///</summary>
        public static GraphColor CadetBlue { get { return new GraphColor(255, 95, 158, 160); } }

        ///<summary>
        /// Chartreuse, ARGB=255, 127, 255, 0
        ///</summary>
        public static GraphColor Chartreuse { get { return new GraphColor(255, 127, 255, 0); } }

        ///<summary>
        /// Chocolate, ARGB=255, 210, 105, 30
        ///</summary>
        public static GraphColor Chocolate { get { return new GraphColor(255, 210, 105, 30); } }

        ///<summary>
        /// Coral, ARGB=255, 255, 127, 80
        ///</summary>
        public static GraphColor Coral { get { return new GraphColor(255, 255, 127, 80); } }

        ///<summary>
        /// CornflowerBlue, ARGB=255, 100, 149, 237
        ///</summary>
        public static GraphColor CornflowerBlue { get { return new GraphColor(255, 100, 149, 237); } }

        ///<summary>
        /// Cornsilk, ARGB=255, 255, 248, 220
        ///</summary>
        public static GraphColor Cornsilk { get { return new GraphColor(255, 255, 248, 220); } }

        ///<summary>
        /// Crimson, ARGB=255, 220, 20, 60
        ///</summary>
        public static GraphColor Crimson { get { return new GraphColor(255, 220, 20, 60); } }

        ///<summary>
        /// Cyan, ARGB=255, 0, 255, 255
        ///</summary>
        public static GraphColor Cyan { get { return new GraphColor(255, 0, 255, 255); } }

        ///<summary>
        /// DarkBlue, ARGB=255, 0, 0, 139
        ///</summary>
        public static GraphColor DarkBlue { get { return new GraphColor(255, 0, 0, 139); } }

        ///<summary>
        /// DarkCyan, ARGB=255, 0, 139, 139
        ///</summary>
        public static GraphColor DarkCyan { get { return new GraphColor(255, 0, 139, 139); } }

        ///<summary>
        /// DarkGoldenrod, ARGB=255, 184, 134, 11
        ///</summary>
        public static GraphColor DarkGoldenrod { get { return new GraphColor(255, 184, 134, 11); } }

        ///<summary>
        /// DarkGray, ARGB=255, 169, 169, 169
        ///</summary>
        public static GraphColor DarkGray { get { return new GraphColor(255, 169, 169, 169); } }

        ///<summary>
        /// DarkGreen, ARGB=255, 0, 100, 0
        ///</summary>
        public static GraphColor DarkGreen { get { return new GraphColor(255, 0, 100, 0); } }

        ///<summary>
        /// DarkKhaki, ARGB=255, 189, 183, 107
        ///</summary>
        public static GraphColor DarkKhaki { get { return new GraphColor(255, 189, 183, 107); } }

        ///<summary>
        /// DarkMagenta, ARGB=255, 139, 0, 139
        ///</summary>
        public static GraphColor DarkMagenta { get { return new GraphColor(255, 139, 0, 139); } }

        ///<summary>
        /// DarkOliveGreen, ARGB=255, 85, 107, 47
        ///</summary>
        public static GraphColor DarkOliveGreen { get { return new GraphColor(255, 85, 107, 47); } }

        ///<summary>
        /// DarkOrange, ARGB=255, 255, 140, 0
        ///</summary>
        public static GraphColor DarkOrange { get { return new GraphColor(255, 255, 140, 0); } }

        ///<summary>
        /// DarkOrchid, ARGB=255, 153, 50, 204
        ///</summary>
        public static GraphColor DarkOrchid { get { return new GraphColor(255, 153, 50, 204); } }

        ///<summary>
        /// DarkRed, ARGB=255, 139, 0, 0
        ///</summary>
        public static GraphColor DarkRed { get { return new GraphColor(255, 139, 0, 0); } }

        ///<summary>
        /// DarkSalmon, ARGB=255, 233, 150, 122
        ///</summary>
        public static GraphColor DarkSalmon { get { return new GraphColor(255, 233, 150, 122); } }

        ///<summary>
        /// DarkSeaGreen, ARGB=255, 143, 188, 143
        ///</summary>
        public static GraphColor DarkSeaGreen { get { return new GraphColor(255, 143, 188, 143); } }

        ///<summary>
        /// DarkSlateBlue, ARGB=255, 72, 61, 139
        ///</summary>
        public static GraphColor DarkSlateBlue { get { return new GraphColor(255, 72, 61, 139); } }

        ///<summary>
        /// DarkSlateGray, ARGB=255, 47, 79, 79
        ///</summary>
        public static GraphColor DarkSlateGray { get { return new GraphColor(255, 47, 79, 79); } }

        ///<summary>
        /// DarkTurquoise, ARGB=255, 0, 206, 209
        ///</summary>
        public static GraphColor DarkTurquoise { get { return new GraphColor(255, 0, 206, 209); } }

        ///<summary>
        /// DarkViolet, ARGB=255, 148, 0, 211
        ///</summary>
        public static GraphColor DarkViolet { get { return new GraphColor(255, 148, 0, 211); } }

        ///<summary>
        /// DeepPink, ARGB=255, 255, 20, 147
        ///</summary>
        public static GraphColor DeepPink { get { return new GraphColor(255, 255, 20, 147); } }

        ///<summary>
        /// DeepSkyBlue, ARGB=255, 0, 191, 255
        ///</summary>
        public static GraphColor DeepSkyBlue { get { return new GraphColor(255, 0, 191, 255); } }

        ///<summary>
        /// DimGray, ARGB=255, 105, 105, 105
        ///</summary>
        public static GraphColor DimGray { get { return new GraphColor(255, 105, 105, 105); } }

        ///<summary>
        /// DodgerBlue, ARGB=255, 30, 144, 255
        ///</summary>
        public static GraphColor DodgerBlue { get { return new GraphColor(255, 30, 144, 255); } }

        ///<summary>
        /// Firebrick, ARGB=255, 178, 34, 34
        ///</summary>
        public static GraphColor Firebrick { get { return new GraphColor(255, 178, 34, 34); } }

        ///<summary>
        /// FloralWhite, ARGB=255, 255, 250, 240
        ///</summary>
        public static GraphColor FloralWhite { get { return new GraphColor(255, 255, 250, 240); } }

        ///<summary>
        /// ForestGreen, ARGB=255, 34, 139, 34
        ///</summary>
        public static GraphColor ForestGreen { get { return new GraphColor(255, 34, 139, 34); } }

        ///<summary>
        /// Fuchsia, ARGB=255, 255, 0, 255
        ///</summary>
        public static GraphColor Fuchsia { get { return new GraphColor(255, 255, 0, 255); } }

        ///<summary>
        /// Gainsboro, ARGB=255, 220, 220, 220
        ///</summary>
        public static GraphColor Gainsboro { get { return new GraphColor(255, 220, 220, 220); } }

        ///<summary>
        /// GhostWhite, ARGB=255, 248, 248, 255
        ///</summary>
        public static GraphColor GhostWhite { get { return new GraphColor(255, 248, 248, 255); } }

        ///<summary>
        /// Gold, ARGB=255, 255, 215, 0
        ///</summary>
        public static GraphColor Gold { get { return new GraphColor(255, 255, 215, 0); } }

        ///<summary>
        /// Goldenrod, ARGB=255, 218, 165, 32
        ///</summary>
        public static GraphColor Goldenrod { get { return new GraphColor(255, 218, 165, 32); } }

        ///<summary>
        /// Gray, ARGB=255, 128, 128, 128
        ///</summary>
        public static GraphColor Gray { get { return new GraphColor(255, 128, 128, 128); } }

        ///<summary>
        /// Green, ARGB=255, 0, 128, 0
        ///</summary>
        public static GraphColor Green { get { return new GraphColor(255, 0, 128, 0); } }

        ///<summary>
        /// GreenYellow, ARGB=255, 173, 255, 47
        ///</summary>
        public static GraphColor GreenYellow { get { return new GraphColor(255, 173, 255, 47); } }

        ///<summary>
        /// Honeydew, ARGB=255, 240, 255, 240
        ///</summary>
        public static GraphColor Honeydew { get { return new GraphColor(255, 240, 255, 240); } }

        ///<summary>
        /// HotPink, ARGB=255, 255, 105, 180
        ///</summary>
        public static GraphColor HotPink { get { return new GraphColor(255, 255, 105, 180); } }

        ///<summary>
        /// IndianRed, ARGB=255, 205, 92, 92
        ///</summary>
        public static GraphColor IndianRed { get { return new GraphColor(255, 205, 92, 92); } }

        ///<summary>
        /// Indigo, ARGB=255, 75, 0, 130
        ///</summary>
        public static GraphColor Indigo { get { return new GraphColor(255, 75, 0, 130); } }

        ///<summary>
        /// Ivory, ARGB=255, 255, 255, 240
        ///</summary>
        public static GraphColor Ivory { get { return new GraphColor(255, 255, 255, 240); } }

        ///<summary>
        /// Khaki, ARGB=255, 240, 230, 140
        ///</summary>
        public static GraphColor Khaki { get { return new GraphColor(255, 240, 230, 140); } }

        ///<summary>
        /// Lavender, ARGB=255, 230, 230, 250
        ///</summary>
        public static GraphColor Lavender { get { return new GraphColor(255, 230, 230, 250); } }

        ///<summary>
        /// LavenderBlush, ARGB=255, 255, 240, 245
        ///</summary>
        public static GraphColor LavenderBlush { get { return new GraphColor(255, 255, 240, 245); } }

        ///<summary>
        /// LawnGreen, ARGB=255, 124, 252, 0
        ///</summary>
        public static GraphColor LawnGreen { get { return new GraphColor(255, 124, 252, 0); } }

        ///<summary>
        /// LemonChiffon, ARGB=255, 255, 250, 205
        ///</summary>
        public static GraphColor LemonChiffon { get { return new GraphColor(255, 255, 250, 205); } }

        ///<summary>
        /// LightBlue, ARGB=255, 173, 216, 230
        ///</summary>
        public static GraphColor LightBlue { get { return new GraphColor(255, 173, 216, 230); } }

        ///<summary>
        /// LightCoral, ARGB=255, 240, 128, 128
        ///</summary>
        public static GraphColor LightCoral { get { return new GraphColor(255, 240, 128, 128); } }

        ///<summary>
        /// LightCyan, ARGB=255, 224, 255, 255
        ///</summary>
        public static GraphColor LightCyan { get { return new GraphColor(255, 224, 255, 255); } }

        ///<summary>
        /// LightGoldenrodYellow, ARGB=255, 250, 250, 210
        ///</summary>
        public static GraphColor LightGoldenrodYellow { get { return new GraphColor(255, 250, 250, 210); } }

        ///<summary>
        /// LightGray, ARGB=255, 211, 211, 211
        ///</summary>
        public static GraphColor LightGray { get { return new GraphColor(255, 211, 211, 211); } }

        ///<summary>
        /// LightGreen, ARGB=255, 144, 238, 144
        ///</summary>
        public static GraphColor LightGreen { get { return new GraphColor(255, 144, 238, 144); } }

        ///<summary>
        /// LightPink, ARGB=255, 255, 182, 193
        ///</summary>
        public static GraphColor LightPink { get { return new GraphColor(255, 255, 182, 193); } }

        ///<summary>
        /// LightSalmon, ARGB=255, 255, 160, 122
        ///</summary>
        public static GraphColor LightSalmon { get { return new GraphColor(255, 255, 160, 122); } }

        ///<summary>
        /// LightSeaGreen, ARGB=255, 32, 178, 170
        ///</summary>
        public static GraphColor LightSeaGreen { get { return new GraphColor(255, 32, 178, 170); } }

        ///<summary>
        /// LightSkyBlue, ARGB=255, 135, 206, 250
        ///</summary>
        public static GraphColor LightSkyBlue { get { return new GraphColor(255, 135, 206, 250); } }

        ///<summary>
        /// LightSlateGray, ARGB=255, 119, 136, 153
        ///</summary>
        public static GraphColor LightSlateGray { get { return new GraphColor(255, 119, 136, 153); } }

        ///<summary>
        /// LightSteelBlue, ARGB=255, 176, 196, 222
        ///</summary>
        public static GraphColor LightSteelBlue { get { return new GraphColor(255, 176, 196, 222); } }

        ///<summary>
        /// LightYellow, ARGB=255, 255, 255, 224
        ///</summary>
        public static GraphColor LightYellow { get { return new GraphColor(255, 255, 255, 224); } }

        ///<summary>
        /// Lime, ARGB=255, 0, 255, 0
        ///</summary>
        public static GraphColor Lime { get { return new GraphColor(255, 0, 255, 0); } }

        ///<summary>
        /// LimeGreen, ARGB=255, 50, 205, 50
        ///</summary>
        public static GraphColor LimeGreen { get { return new GraphColor(255, 50, 205, 50); } }

        ///<summary>
        /// Linen, ARGB=255, 250, 240, 230
        ///</summary>
        public static GraphColor Linen { get { return new GraphColor(255, 250, 240, 230); } }

        ///<summary>
        /// Magenta, ARGB=255, 255, 0, 255
        ///</summary>
        public static GraphColor Magenta { get { return new GraphColor(255, 255, 0, 255); } }

        ///<summary>
        /// Maroon, ARGB=255, 128, 0, 0
        ///</summary>
        public static GraphColor Maroon { get { return new GraphColor(255, 128, 0, 0); } }

        ///<summary>
        /// MediumAquamarine, ARGB=255, 102, 205, 170
        ///</summary>
        public static GraphColor MediumAquamarine { get { return new GraphColor(255, 102, 205, 170); } }

        ///<summary>
        /// MediumBlue, ARGB=255, 0, 0, 205
        ///</summary>
        public static GraphColor MediumBlue { get { return new GraphColor(255, 0, 0, 205); } }

        ///<summary>
        /// MediumOrchid, ARGB=255, 186, 85, 211
        ///</summary>
        public static GraphColor MediumOrchid { get { return new GraphColor(255, 186, 85, 211); } }

        ///<summary>
        /// MediumPurple, ARGB=255, 147, 112, 219
        ///</summary>
        public static GraphColor MediumPurple { get { return new GraphColor(255, 147, 112, 219); } }

        ///<summary>
        /// MediumSeaGreen, ARGB=255, 60, 179, 113
        ///</summary>
        public static GraphColor MediumSeaGreen { get { return new GraphColor(255, 60, 179, 113); } }

        ///<summary>
        /// MediumSlateBlue, ARGB=255, 123, 104, 238
        ///</summary>
        public static GraphColor MediumSlateBlue { get { return new GraphColor(255, 123, 104, 238); } }

        ///<summary>
        /// MediumSpringGreen, ARGB=255, 0, 250, 154
        ///</summary>
        public static GraphColor MediumSpringGreen { get { return new GraphColor(255, 0, 250, 154); } }

        ///<summary>
        /// MediumTurquoise, ARGB=255, 72, 209, 204
        ///</summary>
        public static GraphColor MediumTurquoise { get { return new GraphColor(255, 72, 209, 204); } }

        ///<summary>
        /// MediumVioletRed, ARGB=255, 199, 21, 133
        ///</summary>
        public static GraphColor MediumVioletRed { get { return new GraphColor(255, 199, 21, 133); } }

        ///<summary>
        /// MidnightBlue, ARGB=255, 25, 25, 112
        ///</summary>
        public static GraphColor MidnightBlue { get { return new GraphColor(255, 25, 25, 112); } }

        ///<summary>
        /// MintCream, ARGB=255, 245, 255, 250
        ///</summary>
        public static GraphColor MintCream { get { return new GraphColor(255, 245, 255, 250); } }

        ///<summary>
        /// MistyRose, ARGB=255, 255, 228, 225
        ///</summary>
        public static GraphColor MistyRose { get { return new GraphColor(255, 255, 228, 225); } }

        ///<summary>
        /// Moccasin, ARGB=255, 255, 228, 181
        ///</summary>
        public static GraphColor Moccasin { get { return new GraphColor(255, 255, 228, 181); } }

        ///<summary>
        /// NavajoWhite, ARGB=255, 255, 222, 173
        ///</summary>
        public static GraphColor NavajoWhite { get { return new GraphColor(255, 255, 222, 173); } }

        ///<summary>
        /// Navy, ARGB=255, 0, 0, 128
        ///</summary>
        public static GraphColor Navy { get { return new GraphColor(255, 0, 0, 128); } }

        ///<summary>
        /// OldLace, ARGB=255, 253, 245, 230
        ///</summary>
        public static GraphColor OldLace { get { return new GraphColor(255, 253, 245, 230); } }

        ///<summary>
        /// Olive, ARGB=255, 128, 128, 0
        ///</summary>
        public static GraphColor Olive { get { return new GraphColor(255, 128, 128, 0); } }

        ///<summary>
        /// OliveDrab, ARGB=255, 107, 142, 35
        ///</summary>
        public static GraphColor OliveDrab { get { return new GraphColor(255, 107, 142, 35); } }

        ///<summary>
        /// Orange, ARGB=255, 255, 165, 0
        ///</summary>
        public static GraphColor Orange { get { return new GraphColor(255, 255, 165, 0); } }

        ///<summary>
        /// OrangeRed, ARGB=255, 255, 69, 0
        ///</summary>
        public static GraphColor OrangeRed { get { return new GraphColor(255, 255, 69, 0); } }

        ///<summary>
        /// Orchid, ARGB=255, 218, 112, 214
        ///</summary>
        public static GraphColor Orchid { get { return new GraphColor(255, 218, 112, 214); } }

        ///<summary>
        /// PaleGoldenrod, ARGB=255, 238, 232, 170
        ///</summary>
        public static GraphColor PaleGoldenrod { get { return new GraphColor(255, 238, 232, 170); } }

        ///<summary>
        /// PaleGreen, ARGB=255, 152, 251, 152
        ///</summary>
        public static GraphColor PaleGreen { get { return new GraphColor(255, 152, 251, 152); } }

        ///<summary>
        /// PaleTurquoise, ARGB=255, 175, 238, 238
        ///</summary>
        public static GraphColor PaleTurquoise { get { return new GraphColor(255, 175, 238, 238); } }

        ///<summary>
        /// PaleVioletRed, ARGB=255, 219, 112, 147
        ///</summary>
        public static GraphColor PaleVioletRed { get { return new GraphColor(255, 219, 112, 147); } }

        ///<summary>
        /// PapayaWhip, ARGB=255, 255, 239, 213
        ///</summary>
        public static GraphColor PapayaWhip { get { return new GraphColor(255, 255, 239, 213); } }

        ///<summary>
        /// PeachPuff, ARGB=255, 255, 218, 185
        ///</summary>
        public static GraphColor PeachPuff { get { return new GraphColor(255, 255, 218, 185); } }

        ///<summary>
        /// Peru, ARGB=255, 205, 133, 63
        ///</summary>
        public static GraphColor Peru { get { return new GraphColor(255, 205, 133, 63); } }

        ///<summary>
        /// Pink, ARGB=255, 255, 192, 203
        ///</summary>
        public static GraphColor Pink { get { return new GraphColor(255, 255, 192, 203); } }

        ///<summary>
        /// Plum, ARGB=255, 221, 160, 221
        ///</summary>
        public static GraphColor Plum { get { return new GraphColor(255, 221, 160, 221); } }

        ///<summary>
        /// PowderBlue, ARGB=255, 176, 224, 230
        ///</summary>
        public static GraphColor PowderBlue { get { return new GraphColor(255, 176, 224, 230); } }

        ///<summary>
        /// Purple, ARGB=255, 128, 0, 128
        ///</summary>
        public static GraphColor Purple { get { return new GraphColor(255, 128, 0, 128); } }

        ///<summary>
        /// Red, ARGB=255, 255, 0, 0
        ///</summary>
        public static GraphColor Red { get { return new GraphColor(255, 255, 0, 0); } }

        ///<summary>
        /// RosyBrown, ARGB=255, 188, 143, 143
        ///</summary>
        public static GraphColor RosyBrown { get { return new GraphColor(255, 188, 143, 143); } }

        ///<summary>
        /// RoyalBlue, ARGB=255, 65, 105, 225
        ///</summary>
        public static GraphColor RoyalBlue { get { return new GraphColor(255, 65, 105, 225); } }

        ///<summary>
        /// SaddleBrown, ARGB=255, 139, 69, 19
        ///</summary>
        public static GraphColor SaddleBrown { get { return new GraphColor(255, 139, 69, 19); } }

        ///<summary>
        /// Salmon, ARGB=255, 250, 128, 114
        ///</summary>
        public static GraphColor Salmon { get { return new GraphColor(255, 250, 128, 114); } }

        ///<summary>
        /// SandyBrown, ARGB=255, 244, 164, 96
        ///</summary>
        public static GraphColor SandyBrown { get { return new GraphColor(255, 244, 164, 96); } }

        ///<summary>
        /// SeaGreen, ARGB=255, 46, 139, 87
        ///</summary>
        public static GraphColor SeaGreen { get { return new GraphColor(255, 46, 139, 87); } }

        ///<summary>
        /// SeaShell, ARGB=255, 255, 245, 238
        ///</summary>
        public static GraphColor SeaShell { get { return new GraphColor(255, 255, 245, 238); } }

        ///<summary>
        /// Sienna, ARGB=255, 160, 82, 45
        ///</summary>
        public static GraphColor Sienna { get { return new GraphColor(255, 160, 82, 45); } }

        ///<summary>
        /// Silver, ARGB=255, 192, 192, 192
        ///</summary>
        public static GraphColor Silver { get { return new GraphColor(255, 192, 192, 192); } }

        ///<summary>
        /// SkyBlue, ARGB=255, 135, 206, 235
        ///</summary>
        public static GraphColor SkyBlue { get { return new GraphColor(255, 135, 206, 235); } }

        ///<summary>
        /// SlateBlue, ARGB=255, 106, 90, 205
        ///</summary>
        public static GraphColor SlateBlue { get { return new GraphColor(255, 106, 90, 205); } }

        ///<summary>
        /// SlateGray, ARGB=255, 112, 128, 144
        ///</summary>
        public static GraphColor SlateGray { get { return new GraphColor(255, 112, 128, 144); } }

        ///<summary>
        /// Snow, ARGB=255, 255, 250, 250
        ///</summary>
        public static GraphColor Snow { get { return new GraphColor(255, 255, 250, 250); } }

        ///<summary>
        /// SpringGreen, ARGB=255, 0, 255, 127
        ///</summary>
        public static GraphColor SpringGreen { get { return new GraphColor(255, 0, 255, 127); } }

        ///<summary>
        /// SteelBlue, ARGB=255, 70, 130, 180
        ///</summary>
        public static GraphColor SteelBlue { get { return new GraphColor(255, 70, 130, 180); } }

        ///<summary>
        /// Tan, ARGB=255, 210, 180, 140
        ///</summary>
        public static GraphColor Tan { get { return new GraphColor(255, 210, 180, 140); } }

        ///<summary>
        /// Teal, ARGB=255, 0, 128, 128
        ///</summary>
        public static GraphColor Teal { get { return new GraphColor(255, 0, 128, 128); } }

        ///<summary>
        /// Thistle, ARGB=255, 216, 191, 216
        ///</summary>
        public static GraphColor Thistle { get { return new GraphColor(255, 216, 191, 216); } }

        ///<summary>
        /// Tomato, ARGB=255, 255, 99, 71
        ///</summary>
        public static GraphColor Tomato { get { return new GraphColor(255, 255, 99, 71); } }

        ///<summary>
        /// Transparent, ARGB=0, 255, 255, 255
        ///</summary>
        public static GraphColor Transparent { get { return new GraphColor(0, 255, 255, 255); } }

        ///<summary>
        /// Turquoise, ARGB=255, 64, 224, 208
        ///</summary>
        public static GraphColor Turquoise { get { return new GraphColor(255, 64, 224, 208); } }

        ///<summary>
        /// Violet, ARGB=255, 238, 130, 238
        ///</summary>
        public static GraphColor Violet { get { return new GraphColor(255, 238, 130, 238); } }

        ///<summary>
        /// Wheat, ARGB=255, 245, 222, 179
        ///</summary>
        public static GraphColor Wheat { get { return new GraphColor(255, 245, 222, 179); } }

        ///<summary>
        /// White, ARGB=255, 255, 255, 255
        ///</summary>
        public static GraphColor White { get { return new GraphColor(255, 255, 255, 255); } }

        ///<summary>
        /// WhiteSmoke, ARGB=255, 245, 245, 245
        ///</summary>
        public static GraphColor WhiteSmoke { get { return new GraphColor(255, 245, 245, 245); } }

        ///<summary>
        /// Yellow, ARGB=255, 255, 255, 0
        ///</summary>
        public static GraphColor Yellow { get { return new GraphColor(255, 255, 255, 0); } }

        ///<summary>
        /// YellowGreen, ARGB=255, 154, 205, 50
        ///</summary>
        public static GraphColor YellowGreen { get { return new GraphColor(255, 154, 205, 50); } }

        #endregion

        #region [Constructors]

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphColor"/> struct.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="r">The r.</param>
        /// <param name="g">The g.</param>
        /// <param name="b">The b.</param>
        public GraphColor(byte a, byte r, byte g, byte b) : this()
        {
            A = a;
            R = r;
            G = g;
            B = b;
        }

        #endregion

        #region [Public Methods]

        public static bool operator ==(GraphColor color1, GraphColor color2)
        {
            if (object.ReferenceEquals(color1, null))
            {
                return object.ReferenceEquals(color2, null);
            }

            return color1.A == color2.A && color1.R == color2.R && color1.G == color2.G && color1.B == color2.B;
        }

        public static bool operator !=(GraphColor color1, GraphColor color2)
        {
            return !(color1 == color2);
        }

        #endregion

        #region [Private Methods]

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
