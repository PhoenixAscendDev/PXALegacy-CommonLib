using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Helpers;
namespace JB2.Common
{
    public class JB2Color
    {
        private string _id;
        private string _name;
        private System.Drawing.Color _systemColor;
        private int _hex;
        private RGB _rgb;
        private HSV _hsv;
        private HSL _hsl;

        #region Constructors

        public JB2Color()
        {

        }


        private JB2Color(RGB rgb) : this(System.Drawing.ColorTranslator.FromHtml("#" + rgb.HexString))
        {


        }

        private JB2Color(HSV hsv) : this(ColorHelper.ColorConverter(hsv) )
        {
            
        }

        private JB2Color(System.Drawing.Color color)
        {
            _systemColor = color;
            _rgb = ColorHelper.RGBConverter(color);
            _hsv = ColorHelper.HSVConverter(color);
            _hsl = ColorHelper.HSLConverter(color);

        }

        #endregion Constructors

        #region Properties
        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public string HexString
        {
            get
            {
                return RGB.HexString;
            }
        }

        public int HexValue
        {
            get
            {
                return RGB.Value;
            }

        }

        public int ARGB
        {
            get
            {
                return _systemColor.ToArgb();

            }

        }

        public RGB RGB
        {
            get
            {
                return _rgb;

            }
        }

        public HSV HSV
        {
            get
            {
                return _hsv;
            }
        }

        public HSL HSL
        {
            get
            {
                return _hsl;
            }
        }

        public string HtmlValue
        {
            get
            {
                return "#" + this.HexString.ToUpper();
            }
        }

        #endregion Properties


        #region Operators

        //public static bool operator ==(JB2Color x, JB2Color y)
        //{
        //    if 
        //    if ((object)x == null) return (object)y == null;
        //    return x._systemColor == y._systemColor;
        //}

        //public static bool operator !=(JB2Color x, JB2Color y)
        //{
        //    return !(x == y);
        //}


        public static implicit operator string (JB2Color color)
        {
            return color.RGB.HexString;
        }


        public static implicit operator System.Drawing.Color(JB2Color rhs)
        {
            return rhs._systemColor;
        }

        public static implicit operator JB2Color(System.Drawing.Color sdc)
        {
            var result = new JB2Color(sdc);

            return result;
        }



        #endregion Operators

        #region Static From

        public static JB2Color FromHex(string hexString)
        {
            if (string.IsNullOrEmpty(hexString))
                return JB2Color.Empty;
            else
                return  new JB2Color(new RGB(hexString));
        }

        public static JB2Color FromHex(int hex)
        {

            
            return new JB2Color(new RGB(hex));
        }

        public static JB2Color FromRGB(RGB rgb)
        {
            return new JB2Color(rgb);
        }

        public static JB2Color FromRGB(int red, int green, int blue)
        {
            return new JB2Color(new RGB(red, green, blue));
        }

        public static JB2Color FromHSV( HSV hsv)
        {
            return new JB2Color(hsv);
        }



        public static JB2Color FromHSV(double hue, double saturation, double value)
        {
            return new JB2Color(new HSV(hue, saturation, value));
        }

        public static JB2Color FromHSL(double hue, double saturation, double lightness)
        {
            return new JB2Color(ColorHelper.ConvertHSLToColor(hue, saturation, lightness));
        }

        public static JB2Color GetRandom()
        {
            int r = JB2.Common.Utility.RandomNumber(0, 255);
            int g = JB2.Common.Utility.RandomNumber(0, 255);
            int b = JB2.Common.Utility.RandomNumber(0, 255);

            return JB2Color.FromRGB(r, g, b);
        }



        #endregion Static From 

        #region Static Colors

        public static JB2Color White
        {
            get
            {
                return JB2Color.FromHex("ffffff");
            }
        }

        public static JB2Color Black
        {
            get
            {
                return JB2Color.FromHex("000000");
            }
        }

        public static JB2Color Empty
        {
            get
            {
                return JB2Color.White;
            }
        }



        #endregion Static Colors



    }
}
