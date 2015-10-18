using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Common
{
    public static class ColorHelper
    {

        public static String HexConverter(System.Drawing.Color c)
        {
            return c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }

        public static RGB RGBConverter(System.Drawing.Color c)
        {
            return new RGB(c.R, c.G, c.B);
        }
    }
}
