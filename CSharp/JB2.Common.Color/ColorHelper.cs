using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

using JB2.Common;

namespace JB2.Helpers
{
    public static class ColorHelper
    {

        public static String HexConverter(System.Drawing.Color c)
        {
            return c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }

        public static String HexConverter(double hue, double saturation, double value)
        {
            System.Drawing.Color color;


            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value = value * 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));
            int q = Convert.ToInt32(value * (1 - f * saturation));
            int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

            if (hi == 0)
                color = Color.FromArgb(255, v, t, p);
            else if (hi == 1)
                color = Color.FromArgb(255, q, v, p);
            else if (hi == 2)
                color = Color.FromArgb(255, p, v, t);
            else if (hi == 3)
                color = Color.FromArgb(255, p, q, v);
            else if (hi == 4)
                color = Color.FromArgb(255, t, p, v);
            else
                color = Color.FromArgb(255, v, p, q);


            return HexConverter(color);
        }

        public static RGB RGBConverter(System.Drawing.Color c)
        {
            return new RGB(c.R, c.G, c.B);
        }


        public static HSV HSVConverter(System.Drawing.Color c)
        {
            int max = Math.Max(c.R, Math.Max(c.G, c.B));
            int min = Math.Min(c.R, Math.Min(c.G, c.B));

            HSV hsv = new HSV(c.GetHue(), (max == 0) ? 0 : 1d - (1d * min / max), max / 255d);

            return hsv;

        }

        public static HSL HSLConverter(System.Drawing.Color c)
        {
            return new HSL(c.GetHue(), c.GetSaturation(), c.GetBrightness());
        }




        public static System.Drawing.Color ColorConverter(HSV hsv)
        {
            System.Drawing.Color color;

            double hue = hsv.Hue;
            double saturation = hsv.Saturation;
            double value = hsv.Value;


            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value = value * 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));
            int q = Convert.ToInt32(value * (1 - f * saturation));
            int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

            if (hi == 0)
                color = Color.FromArgb(255, v, t, p);
            else if (hi == 1)
                color = Color.FromArgb(255, q, v, p);
            else if (hi == 2)
                color = Color.FromArgb(255, p, v, t);
            else if (hi == 3)
                color = Color.FromArgb(255, p, q, v);
            else if (hi == 4)
                color = Color.FromArgb(255, t, p, v);
            else
                color = Color.FromArgb(255, v, p, q);

            return color;
        }

        public static System.Drawing.Color ConvertHSLToColor(double h, double s, double l)
        {

            //http://james-ramsden.com/convert-from-hsl-to-rgb-colour-codes-in-c/
            h = h / 360.0;

            double r = 0, g = 0, b = 0;
            if (l != 0)
            {
                if (s == 0)
                    r = g = b = l;
                else
                {
                    double temp2;
                    if (l < 0.5)
                        temp2 = l * (1.0 + s);
                    else
                        temp2 = l + s - (l * s);

                    double temp1 = 2.0 * l - temp2;

                    r = GetColorComponent(temp1, temp2, h + 1.0 / 3.0);
                    g = GetColorComponent(temp1, temp2, h);
                    b = GetColorComponent(temp1, temp2, h - 1.0 / 3.0);
                }
            }
            return Color.FromArgb((int)(255 * r), (int)(255 * g), (int)(255 * b));

        }

        public static JB2Color GetComplementaryColor(JB2Color color)
        {
            double newHue = (color.HSL.Hue + 180.00);
            newHue = newHue > 360.00 ? newHue - 360.00 : newHue; 
            return JB2Color.FromHSL(newHue, color.HSL.Saturation, color.HSL.Lightness);
        }

        public static JB2Color[] GetComplementaryColors(JB2Color color)
        {
            List<JB2Color> colors = new List<JB2Color>(2);

            colors.Add(color);
            colors.Add(GetComplementaryColor(color));

            return colors.ToArray();

        }

        public static JB2Color[] GetSplitComplementaryColors(JB2Color color)
        {
            List<JB2Color> colors = new List<JB2Color>(3);

            colors.Add(color);

            JB2Color complement = GetComplementaryColor(color);

            JB2Color[] adjcolors = GetAdjacentColors(complement, 30, 1);

            colors.Add(adjcolors[0]);
            colors.Add(adjcolors[2]);

            return colors.ToArray();
        }

        public static JB2Color[] GetAdjacentColors(JB2Color color)
        {
            return GetAdjacentColors(color,30,2);

        }



        public static JB2Color[]  GetAdjacentColors(JB2Color color, int degrees, int numOfColors)
        {
            List<JB2Color> colors = new List<JB2Color>(numOfColors * 2);

            for (int i = numOfColors; i >= 1; i--)
            {
                double newHue = color.HSL.Hue - Convert.ToDouble(degrees * i);
                newHue = newHue > 360.00 ? newHue - 360.00 : newHue;
                colors.Add(JB2Color.FromHSL(newHue, color.HSL.Saturation, color.HSL.Lightness));
            }

            colors.Add(color);

            for (int i = 1; i <= numOfColors; i++)
            {
                double newHue = color.HSL.Hue + Convert.ToDouble(degrees * i);
                newHue = newHue > 360.00 ? newHue - 360.00 : newHue;
                colors.Add(JB2Color.FromHSL(newHue, color.HSL.Saturation, color.HSL.Lightness));
            }

            return colors.ToArray();
        }

        public static JB2Color[] GetTriadColors(JB2Color color)
        {
            return GetTriadColors(color, 120);
        }

        public static JB2Color[] GetTriadColors(JB2Color color, int degrees)
        {
            JB2Color[] colors = ColorHelper.GetAdjacentColors(color, 120,1);

            //base color goes first
            return new JB2Color[] { colors[1], colors[0], colors[2] };
        }


        public static JB2Color[] GetTetradColors(JB2Color color)
        {
            return GetTetradColors(color, 30);
        }

        public static JB2Color[] GetTetradColors(JB2Color color, int degrees)
        {
            List<JB2Color> colors = new List<JB2Color>(4);

            colors.Add(color);


            colors.Add(HueShift(color, degrees));
            colors.Add(HueShift(color, 180));
            colors.Add(HueShift(color, 180 + degrees));

            return colors.ToArray();

        }

        public static JB2Color HueShift(JB2Color color, int degrees)
        {
            double newHue = (color.HSL.Hue + Convert.ToDouble(degrees));
            newHue = newHue > 360.00 ? newHue - 360.00 : newHue;
            return JB2Color.FromHSL(newHue, color.HSL.Saturation, color.HSL.Lightness);

        }

        private static double GetColorComponent(double temp1, double temp2, double temp3)
        {
            if (temp3 < 0.0)
                temp3 += 1.0;
            else if (temp3 > 1.0)
                temp3 -= 1.0;

            if (temp3 < 1.0 / 6.0)
                return temp1 + (temp2 - temp1) * 6.0 * temp3;
            else if (temp3 < 0.5)
                return temp2;
            else if (temp3 < 2.0 / 3.0)
                return temp1 + ((temp2 - temp1) * ((2.0 / 3.0) - temp3) * 6.0);
            else
                return temp1;
        }


    }

}
