using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JB2.Common
{
    public struct RGB
    {

        #region Fields

        int _red;
        int _green;
        int _blue;
        

        #endregion Fields

        #region Constructors

        public RGB(int hex) : this(hex.ToString("X"))
        {
           

        }

        public RGB(string hexString)
        {
            switch(hexString.Length)
            {
                case 6:
                    _red = Int32.Parse(hexString.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
                    _green = Int32.Parse(hexString.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
                    _blue = Int32.Parse(hexString.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
                    break;
                case 3:
                    _red = Int32.Parse(hexString.Substring(0, 1) + hexString.Substring(0, 1), System.Globalization.NumberStyles.HexNumber);
                    _green = Int32.Parse(hexString.Substring(1, 1) + hexString.Substring(1, 1), System.Globalization.NumberStyles.HexNumber);
                    _blue = Int32.Parse(hexString.Substring(2, 1) + hexString.Substring(2, 1), System.Globalization.NumberStyles.HexNumber);
                    break;
                default:
                    _red = 0;
                    _green = 0;
                    _blue = 0;
                    break;
            }
        }

        public RGB(int red, int green, int blue)
        {
            _red = red;
            _green = green;
            _blue = blue;
        }

        public RGB(System.Drawing.Color color)
        {
            _red = color.R;
            _green = color.G;
            _blue = color.B;
        }

        #endregion Constructors


        #region Properties


        public int Red
        {
            get
            {
                return _red;
            }
        }

        public int Green
        {
            get
            {
                return _green;
            }
        }

        public int Blue
        {
            get
            {
                return _blue;
            }
        }

        public string HexString
        {
            get
            {
                string red = _red.ToString("X2");
                string green = _green.ToString("X2");
                string blue = _blue.ToString("X2");

                string hexString = red + green + blue;

                return hexString;
            }
        }
        public int Value
        {
            get
            {
                int value = Int32.Parse(this.HexString, System.Globalization.NumberStyles.HexNumber);

                return value;
            }
        }
        #endregion Properties

        #region ToString

        public override string ToString()
        {
            return "RGB(" + _red.ToString() + ","  + _green.ToString() + "," + _blue.ToString() + ")";     
        }

        #endregion



    }
}
