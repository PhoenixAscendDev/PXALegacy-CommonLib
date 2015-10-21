using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace JB2.Common
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
            double newHue = (color.HSV.Hue + 180.00);
            newHue = newHue > 360.00 ? newHue - 360.00 : newHue; 
            return JB2Color.FromHSV(newHue, color.HSV.Saturation, color.HSV.Value);
        }

        public static JB2Color[] GetAdjacentColors(JB2Color color)
        {
            return GetAdjacentColors(color,30);

        }

        public static JB2Color[]  GetAdjacentColors(JB2Color color, int degrees)
        {

            double newHue1 = color.HSV.Hue + Convert.ToDouble(degrees);
            newHue1 = newHue1 > 360.00 ? newHue1 - 360.00 : newHue1;

            double newHue2 = color.HSV.Hue - Convert.ToDouble(degrees);
            newHue2 = newHue2 > 360.00 ? newHue2 - 360.00 : newHue2;

            List<JB2Color> colors = new List<JB2Color>(2);

            colors.Add(JB2Color.FromHSV(newHue1, color.HSV.Saturation, color.HSV.Value));
            colors.Add(JB2Color.FromHSV(newHue1, color.HSV.Saturation, color.HSV.Value));


            return colors.ToArray();
        }

        public static JB2Color[] GetTriadColors(JB2Color color)
        {
            return GetTriadColors(color, 120);
        }

        public static JB2Color[] GetTriadColors(JB2Color color, int degrees)
        {
            return ColorHelper.GetAdjacentColors(color, 120);

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
