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
    }

}
