using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common.Attributes
{
    public class ColorHex : System.Attribute
    {
        public string Hex;

        public ColorHex(string hex)
        {
            this.Hex = hex;
        }
    }
}
