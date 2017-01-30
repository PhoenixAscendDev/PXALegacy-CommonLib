using System;
using System.Collections.Generic;
using System.Text;

namespace JB2.Common.Map
{
    public class EmptyAddress : Address<StateProvince, Country>
    {
        public override string ToUSMailStandard()
        {
            return string.Empty;
        }
    }
}
