using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Common
{
    public class JB2Color : JB2.Common.IIDNamePair<string, string>
    {
        private string _id;
        private string _name;
        private System.Drawing.Color _systemColor;
        private int _hex;
        private RGB _rgb;

        #region Constructors

        public JB2Color()
        {
            _id = JB2.Common.ShortGuid.NewGuid();
            _name = "color-" + _id;
        }

        public JB2Color(string id, string name, RGB rgbValue)
        {
            _id = id;
            _name = name;
        }

        private JB2Color(RGB rgb)
        {
            _systemColor = System.Drawing.ColorTranslator.FromHtml("#" + rgb.HexString);
            _rgb = rgb;

        }

        private JB2Color(System.Drawing.Color color)
        {
            _systemColor = color;
            _rgb = ColorHelper.RGBConverter(color);

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
                return string.Empty;
            }
        }

        public int ARGB
        {
            get
            {
                return 0;

            }

        }

        public RGB RGB
        {
            get
            {
                return _rgb;

            }
        }

        #endregion Properties


        #region Operators

        public static bool operator ==(JB2Color x, JB2Color y)
        {
            if ((object)x == null) return (object)y == null;
            return x._systemColor == y._systemColor;
        }

        public static bool operator !=(JB2Color x, JB2Color y)
        {
            return !(x == y);
        }


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
            return  new JB2Color(new RGB(hexString));
        }

        public static JB2Color FromRGB(RGB rgb)
        {
            return new JB2Color(rgb);
        }

        public static JB2Color FromRGB(int red, int green, int blue)
        {
            return new JB2Color(new RGB(red, green, blue));
        }

        public static JB2Color FromRGB( int hex)
        {
            return new JB2Color(new RGB(hex));
        }

        #endregion Static From 



    }
}
