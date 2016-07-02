using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public interface ITradeable<TID> : IValidable, IIDProp<TID>
        where TID : IComparable
    {
        IPerson<TID> GetOwner();

        void TradeTo(IPerson<TID> newOwner);


        event Action<ITradeable<TID>, IPerson<TID>, IPerson<TID>> Traded;
    }
}
