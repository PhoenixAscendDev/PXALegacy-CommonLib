using System;
using System.Collections.Generic;
using System.Text;

namespace JB2.Common
{
    public interface IObject<TKind,TKey, TTag> : IIDNamePair<TKey, string>
         where TKey : IComparable
    {
        TKind Kind { get; }
        TTag[] Tags { get; set; }
        bool AddTag(TTag tag);
        bool RemoveTag(TTag tag);
    }
}
