using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Common
{
    public interface IGameObject<TKind,TTag> : IGameObject<string,string,TKind,TTag,int>
        where TKind : IComparable
    {

    }
    public interface IGameObject<TID, TName, TKind, TTag, TRng> : IObject<TKind, TID,TName,TTag,DateTime>
        where TID : IComparable
        where TName : IComparable
        where TKind : IComparable
    {
        TRng RandomNumber { get; set; }

        IGameObject<TID, TName, TKind, TTag, TRng> GetParent();

    }
}
