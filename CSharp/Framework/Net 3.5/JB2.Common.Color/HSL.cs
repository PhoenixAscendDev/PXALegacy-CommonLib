using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using JB2.Helpers;
namespace JB2.Common
{
    public struct HSL
    {

        #region Fields

        double _hue;
        double _saturation;
        double _lightness;
        

        #endregion Fields

        #region Constructors

        public HSL(int hex) : this(hex.ToString("X"))
        {
           

        }

        public HSL(string hexString)
        {
            HSL hsl = ColorHelper.HSLConverter(System.Drawing.ColorTranslator.FromHtml("#" + hexString));

            _hue = hsl.Hue;
            _saturation = hsl.Saturation;
            _lightness = hsl.Lightness;
            
        }

        public HSL(double hue, double saturation, double lightness )
        {
            _hue = hue;
            _saturation = saturation;
            _lightness = lightness;
        }

        public HSL(System.Drawing.Color color)
        {
            HSL hsl = ColorHelper.HSLConverter(color);

            _hue = hsl.Hue;
            _saturation = hsl.Saturation;
            _lightness = hsl.Lightness;

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

        public double Lightness
        {
            get
            {
                return _lightness;
            }
        }

        public string LightnessString
        {
            get
            {
                return Convert.ToInt32(_lightness * 100.00).ToString() + "%";
            }
        }

        public string HexString
        {
            get
            {
                return ColorHelper.HexConverter(ColorHelper.ConvertHSLToColor(this._hue, this._saturation, this._lightness));         
            }
        }
        #endregion Properties

        #region ToString

        public override string ToString()
        {
            return "HSL(" + Convert.ToInt32(_hue).ToString() + ","  + Convert.ToInt32(_saturation * 100.00).ToString() + "%," + Convert.ToInt32(_lightness * 100.00).ToString() + "%)";     
        }

        #endregion



    }
}
