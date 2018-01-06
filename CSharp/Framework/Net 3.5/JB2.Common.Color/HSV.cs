using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using JB2.Helpers;
namespace JB2.Common
{
    public struct HSV
    {

        #region Fields

        double _hue;
        double _saturation;
        double _value;
        

        #endregion Fields

        #region Constructors

        public HSV(int hex) : this(hex.ToString("X"))
        {
           

        }

        public HSV(string hexString)
        {
            HSV hsv = ColorHelper.HSVConverter(System.Drawing.ColorTranslator.FromHtml("#" + hexString));

            _hue = hsv.Hue;
            _saturation = hsv.Saturation;
            _value = hsv.Value;
            
        }

        public HSV(double hue, double saturation, double value )
        {
            _hue = hue;
            _saturation = saturation;
            _value = value;
        }

        public HSV(System.Drawing.Color color)
        {
            HSV hsv = ColorHelper.HSVConverter(color);

            _hue = hsv.Hue;
            _saturation = hsv.Saturation;
            _value = hsv.Value;

        }

        #endregion Constructors


        #region Properties


        public double Hue
        {
            get
            {
                return _hue;
            }
        }

        public string HueString
        {
            get
            {
                return Convert.ToInt32(_hue).ToString() + "°";
            }
        }

        public double Saturation
        {
            get
            {
                return _saturation;
            }
        }

        public string SaturationString
        {
            get
            {
                return Convert.ToInt32(_saturation * 100.00).ToString() + "%";
            }
        }

        public double Value
        {
            get
            {
                return _value;
            }
        }

        public string ValueString
        {
            get
            {
                return Convert.ToInt32(_value * 100.00).ToString() + "%";
            }
        }

        public string HexString
        {
            get
            {
                return ColorHelper.HexConverter(this._hue, this._saturation, this._value);              
            }
        }
        #endregion Properties

        #region ToString

        public override string ToString()
        {
            return "HSV(" + Convert.ToInt32(_hue).ToString() + ","  + Convert.ToInt32(_saturation * 100.00).ToString() + "%," + Convert.ToInt32(_value * 100.00).ToString() + "%)";     
        }

        #endregion



    }
}
