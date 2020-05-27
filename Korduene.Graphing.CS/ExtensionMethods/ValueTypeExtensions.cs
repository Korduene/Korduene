using System;

namespace Korduene.Graphing.CS
{
    public static class ValueTypeExtensions
    {
        public static Boolean ToBoolean(this object value)
        {
            if (value is null)
            {
                return false;
            }

            if (value is Boolean i)
            {
                return i;
            }

            if (Boolean.TryParse(value.ToString(), out Boolean val))
            {
                return val;
            }

            return false;
        }

        public static Byte ToByte(this object value)
        {
            if (value is null)
            {
                return 0;
            }

            if (value is Byte i)
            {
                return i;
            }

            if (Byte.TryParse(value.ToString(), out Byte val))
            {
                return val;
            }

            return 0;
        }

        public static SByte ToSByte(this object value)
        {
            if (value is null)
            {
                return 0;
            }

            if (value is SByte i)
            {
                return i;
            }

            if (SByte.TryParse(value.ToString(), out SByte val))
            {
                return val;
            }

            return 0;
        }

        public static Char ToChar(this object value)
        {
            if (value is null)
            {
                return Char.MinValue;
            }

            if (value is Char i)
            {
                return i;
            }

            if (Char.TryParse(value.ToString(), out Char val))
            {
                return val;
            }

            return Char.MinValue;
        }

        public static Decimal ToDecimal(this object value)
        {
            if (value is null)
            {
                return Decimal.MinValue;
            }

            if (value is Decimal i)
            {
                return i;
            }

            if (Decimal.TryParse(value.ToString(), out Decimal val))
            {
                return val;
            }

            return 0;
        }

        public static Double ToDouble(this object value)
        {
            if (value is null)
            {
                return Double.MinValue;
            }

            if (value is Double i)
            {
                return i;
            }

            if (Double.TryParse(value.ToString(), out Double val))
            {
                return val;
            }

            return 0;
        }

        public static Int16 ToInt16(this object value)
        {
            if (value is null)
            {
                return 0;
            }

            if (value is Int16 i)
            {
                return i;
            }

            if (Int16.TryParse(value.ToString(), out Int16 val))
            {
                return val;
            }

            return 0;
        }

        public static UInt16 ToUInt16(this object value)
        {
            if (value is null)
            {
                return 0;
            }

            if (value is UInt16 i)
            {
                return i;
            }

            if (UInt16.TryParse(value.ToString(), out UInt16 val))
            {
                return val;
            }

            return 0;
        }

        public static Int32 ToInt32(this object value)
        {
            if (value is null)
            {
                return 0;
            }

            if (value is Int32 i)
            {
                return i;
            }

            if (Int32.TryParse(value.ToString(), out Int32 val))
            {
                return val;
            }

            return 0;
        }

        public static UInt32 ToUInt32(this object value)
        {
            if (value is null)
            {
                return 0;
            }

            if (value is UInt32 i)
            {
                return i;
            }

            if (UInt32.TryParse(value.ToString(), out UInt32 val))
            {
                return val;
            }

            return 0;
        }

        public static Int64 ToInt64(this object value)
        {
            if (value is null)
            {
                return 0;
            }

            if (value is Int64 i)
            {
                return i;
            }

            if (Int64.TryParse(value.ToString(), out Int64 val))
            {
                return val;
            }

            return 0;
        }

        public static UInt64 ToUInt64(this object value)
        {
            if (value is null)
            {
                return 0;
            }

            if (value is UInt64 i)
            {
                return i;
            }

            if (UInt64.TryParse(value.ToString(), out UInt64 val))
            {
                return val;
            }

            return 0;
        }

        public static Single ToSingle(this object value)
        {
            if (value is null)
            {
                return 0;
            }

            if (value is Single i)
            {
                return i;
            }

            if (Single.TryParse(value.ToString(), out Single val))
            {
                return val;
            }

            return 0;
        }

        public static Single ToFloat(this object value)
        {
            return ToSingle(value);
        }
    }
}
