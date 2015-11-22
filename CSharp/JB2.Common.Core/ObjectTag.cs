using System;
using System.Collections.Generic;
using System.Text;

namespace JB2.Common
{
    public class ObjectTag : IIDNamePair<int,string>
    {
        public int ID { get; set; }
        public string Name { get; set; }

    }
}
