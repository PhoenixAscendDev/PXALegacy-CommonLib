using System;
using System.Collections.Generic;
using System.Text;

namespace JB2.Common
{
    public interface IObject<TKey, TTag> : IIDNamePair<TKey, string>
    {
        TTag[] Tags { get; set; }
        bool AddTag(TTag tag);
        bool RemoveTag(TTag tag);
    }
}
