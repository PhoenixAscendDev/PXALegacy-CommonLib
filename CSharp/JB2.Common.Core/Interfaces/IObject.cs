using System;
using System.Collections.Generic;
using System.Text;

namespace JB2.Common
{
    public interface IObject<TKind,TKey,TTag> : IObject<TKind,TKey,TTag,DateTime>
        where TKey : IComparable
    {

    }

    public interface IObject<TKind,TKey, TTag,TUpdate> : 
            IIDNamePair<TKey, string>,
            IUpdateable<TUpdate>,
            ITagable<TTag>
         where TKey : IComparable
    {
        TKind GetKind();
        //TKind Kind { get; }
    }
}
