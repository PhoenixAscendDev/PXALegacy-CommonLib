using System;
using System.Collections.Generic;
using System.Text;

namespace JB2.Common
{
    public interface ILookupItem<TKey,TItemType> : IIDNamePair<TKey,string>
    {
        string Code { get; set; }
        string Description { get; set; }
        TItemType Type { get; set; }
    }
}
