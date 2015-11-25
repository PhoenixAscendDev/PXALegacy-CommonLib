using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public class ShortGuidID : StringID
    {

        public ShortGuidID() : base(Enum.NewIDType.ShortGuid)
        {

        }

        public ShortGuidID(string id) : base(id)
        {

        }
    }
}
