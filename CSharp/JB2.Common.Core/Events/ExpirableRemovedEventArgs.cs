using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common.Events
{
    public class ExpirableRemovedEventArgs<TKey, TValue> : EventArgs
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }
    }
}
