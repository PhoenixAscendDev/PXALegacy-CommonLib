using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public interface IKeyNamePair<TKey,TName>: INameProp<TName>
    {
        TKey Key { get; set; }

        TKey GetKey();
    }
}
