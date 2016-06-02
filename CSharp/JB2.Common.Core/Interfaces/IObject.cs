using System;
using System.Collections.Generic;
using System.Text;

namespace JB2.Common
{
    public interface IObject<TKind,TKey,TTag> : IObject<TKind,TKey,TTag,DateTime>
        where TKey : IComparable
    {

    }

    public interface IObject<TKind, TKey, TTag, TUpdate> : IObject<TKind,TKey,string,TTag,TUpdate>
        where TKey : IComparable
    {

    }

    public interface IObject<TKind,TKey,TName,TTag,TUpdate> : 
            IIDNamePair<TKey, TName>,
            IUpdateable<TUpdate>,
            ITagable<TTag>
         where TKey : IComparable
        where TName : IComparable
    {
        TKind GetKind();
        //TKind Kind { get; }
    }
}
