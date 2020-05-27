using Korduene.Graphing.CS;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;

namespace Korduene.Graphing.UI.WPF
{
    internal static class PortColors
    {
        public static GraphColor OP = GraphColor.White;
        public static GraphColor RUN = GraphColor.Black;
        public static GraphColor GROUP = GraphColor.DarkGray;
        //public static Color VAR = Colors.Brown;

        public static GraphColor GetPortColor(CSPort port)
        {
            var type = port.SymbolName?.ToLower();

            switch (type)
            {
                case "byte":
                case "byte[]":
                    return GraphColor.Olive;
                case "sbyte":
                case "sbyte[]":
                    return GraphColor.YellowGreen;
                case "short":
                case "short[]":
                    return GraphColor.Brown;
                case "ushort":
                case "ushort[]":
                    return GraphColor.Chocolate;
                case "int":
                case "int[]":
                    return GraphColor.Blue;
                case "uint":
                case "uint[]":
                    return GraphColor.Maroon;
                case "long":
                case "long[]":
                    return GraphColor.Violet;
                case "ulong":
                case "ulong[]":
                    return GraphColor.Chartreuse;
                case "float":
                case "float[]":
                    return GraphColor.Purple;
                case "double":
                case "double[]":
                    return GraphColor.BlueViolet;
                case "decimal":
                case "decimal[]":
                    return GraphColor.Aqua;
                case "char":
                case "char[]":
                    return GraphColor.OrangeRed;
                case "string":
                case "string[]":
                    return GraphColor.Orange;
                case "bool":
                case "bool[]":
                    return GraphColor.Lime;
                case "color":
                case "color[]":
                    return GraphColor.Magenta;
                //case "image":
                //case "image[]":
                //    return GraphColor.Maroon;
                //case "icon":
                //    return GraphColor.Maroon;
                case "enum":
                case "enum[]":
                    return GraphColor.Aquamarine;
                case "size":
                case "size[]":
                    return GraphColor.BurlyWood;
                case "point":
                case "point[]":
                    return GraphColor.IndianRed;
                case "font":
                case "font[]":
                    return GraphColor.Cyan;
                case "datetime":
                case "datetime[]":
                    return GraphColor.MediumVioletRed;
                case "object":
                case "object[]":
                    return GraphColor.Red;
                //case "form":
                //    return GraphColor.Yellow;
                default:
                    return RandomColor(port.Symbol);
            }
        }

        //public static void SetPortColor(Port port, Type datatype)
        //{
        //    port.BorderColor = GetPortColor(datatype);
        //}

        //public static Color GetPortColor(Type datatype)
        //{
        //    if (datatype == null) { return Colors.White; }
        //    else if (datatype.GetInterface("IList") != null)
        //    {
        //        return GetColor(Core.GetListSubType(datatype));
        //    }
        //    else
        //    {
        //        return GetColor(datatype);
        //    }
        //}

        //private static Color GetColor(Type datatype)
        //{

        //    //if (datatype == typeof(RUN)) { return PortColors.RUN; }
        //    //else if (datatype == typeof(OP)) { return PortColors.OP; }
        //    else if (datatype == typeof(byte)) { return PortColors.BYTE; }
        //    else if (datatype == typeof(int)) { return PortColors.INT; }
        //    else if (datatype == typeof(long)) { return PortColors.LONG; }
        //    else if (datatype == typeof(float)) { return PortColors.FLOAT; }
        //    else if (datatype == typeof(double)) { return PortColors.DOUBLE; }
        //    else if (datatype == typeof(decimal)) { return PortColors.DECIMAL; }
        //    else if (datatype == typeof(char)) { return PortColors.CHAR; }
        //    else if (datatype == typeof(string)) { return PortColors.STRING; }
        //    else if (datatype == typeof(bool)) { return PortColors.BOOL; }
        //    else if (datatype == typeof(object)) { return PortColors.OBJECT; }
        //    else if (datatype == typeof(Color)) { return PortColors.COLOR; }
        //    //else if (datatype == typeof(System.Drawing.Image)) { return PortColors.IMAGE; }
        //    //else if (datatype == typeof(System.Drawing.Icon)) { return PortColors.ICON; }
        //    //else if (datatype == typeof(System.Windows.Forms.Form)) { return PortColors.FORM; }
        //    //else if (datatype.IsEnum) { return PortColors.ENUM; }
        //    //else if (datatype == typeof(System.Drawing.Size)) { return PortColors.SIZE; }
        //    //else if (datatype == typeof(System.Drawing.Point)) { return PortColors.POINT; }
        //    //else if (datatype == typeof(System.Drawing.Font)) { return PortColors.FONT; }
        //    else if (datatype == typeof(DateTime)) { return PortColors.DATETIME; }
        //    //else if (ControlsCache.VisualTypes().Contains(datatype)) { return PortColors.OBJECT; }
        //    else
        //    {
        //        return RandomColor(datatype);
        //    }
        //}

        static GraphColor RandomColor(ISymbol symbol)
        {
            if (symbol == null)
            {
                return GraphColor.Black;
            }

            List<byte> bytes = new List<byte>();
            Dictionary<char, byte> dic = LetterValues();
            foreach (char c in symbol.Name)
            {
                bytes.Add(dic[c]);
            }

            int r;
            int g;
            int b;

            if (bytes.Count >= 9)
            {
                r = bytes[0] + bytes[1] + bytes[2];
                g = bytes[3] + bytes[4] + bytes[5];
                b = bytes[6] + bytes[7] + bytes[8];
            }
            else if (bytes.Count >= 6)
            {
                r = bytes[5] + bytes[1] + bytes[2];
                g = bytes[3] + bytes[4] + bytes[5];
                b = bytes[4] + bytes[3] + bytes[1];
            }
            else if (bytes.Count >= 3)
            {
                r = bytes[0] + bytes[1] + bytes[2];
                g = bytes[2] + bytes[1] + bytes[0];
                b = bytes[1] + bytes[0] + bytes[2];
            }

            return GraphColor.Black;
        }

        static GraphColor RandomColor(Type type)
        {
            if (type == null)
            {
                return GraphColor.Black;
            }

            List<byte> bytes = new List<byte>();
            Dictionary<char, byte> dic = LetterValues();
            foreach (char c in type.Name)
            {
                bytes.Add(dic[c]);
            }

            int r;
            int g;
            int b;
            if (bytes.Count >= 9)
            {
                r = bytes[0] + bytes[1] + bytes[2];
                g = bytes[3] + bytes[4] + bytes[5];
                b = bytes[6] + bytes[7] + bytes[8];
            }
            else if (bytes.Count >= 6)
            {
                r = bytes[5] + bytes[1] + bytes[2];
                g = bytes[3] + bytes[4] + bytes[5];
                b = bytes[4] + bytes[3] + bytes[1];
            }
            else if (bytes.Count >= 3)
            {
                r = bytes[0] + bytes[1] + bytes[2];
                g = bytes[2] + bytes[1] + bytes[0];
                b = bytes[1] + bytes[0] + bytes[2];
            }

            return GraphColor.Black;

            //return System.Windows.Forms.ControlPaint.Light(Colors.FromArgb(r, g, b));
        }

        private static Dictionary<char, byte> LetterValues()
        {
            Dictionary<char, byte> dic = new Dictionary<char, byte>();

            dic.Add('A', 18);
            dic.Add('B', 18);
            dic.Add('C', 28);
            dic.Add('D', 30);
            dic.Add('E', 32);
            dic.Add('F', 33);
            dic.Add('G', 18);
            dic.Add('H', 18);
            dic.Add('I', 35);
            dic.Add('J', 15);
            dic.Add('K', 33);
            dic.Add('L', 24);
            dic.Add('M', 22);
            dic.Add('N', 20);
            dic.Add('O', 28);
            dic.Add('P', 36);
            dic.Add('Q', 22);
            dic.Add('R', 39);
            dic.Add('S', 36);
            dic.Add('T', 30);
            dic.Add('U', 36);
            dic.Add('V', 46);
            dic.Add('W', 30);
            dic.Add('X', 49);
            dic.Add('Y', 29);
            dic.Add('Z', 43);
            dic.Add('a', 6);
            dic.Add('b', 11);
            dic.Add('c', 27);
            dic.Add('d', 31);
            dic.Add('e', 14);
            dic.Add('f', 29);
            dic.Add('g', 34);
            dic.Add('h', 17);
            dic.Add('i', 13);
            dic.Add('j', 28);
            dic.Add('k', 16);
            dic.Add('l', 36);
            dic.Add('m', 21);
            dic.Add('n', 26);
            dic.Add('o', 40);
            dic.Add('p', 35);
            dic.Add('q', 37);
            dic.Add('r', 31);
            dic.Add('s', 29);
            dic.Add('t', 47);
            dic.Add('u', 32);
            dic.Add('v', 41);
            dic.Add('w', 47);
            dic.Add('x', 33);
            dic.Add('y', 33);
            dic.Add('z', 50);

            dic.Add('0', 30);
            dic.Add('1', 49);
            dic.Add('2', 29);
            dic.Add('3', 43);
            dic.Add('4', 6);
            dic.Add('5', 11);
            dic.Add('6', 27);
            dic.Add('7', 31);
            dic.Add('8', 14);
            dic.Add('9', 29);
            dic.Add('_', 34);
            dic.Add('`', 0);

            return dic;
        }
    }
}
