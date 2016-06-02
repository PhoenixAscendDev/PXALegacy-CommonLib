using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Common
{
    public interface ICharacter<TID,TName,TAttributeIndex,TAttribute,TKind,TTag,TRng>:
        IGameObject<TID,TName,TKind,TTag,TRng>, JB2.Common.IIDNamePair<TID,TName>
        where TID : IComparable
        where TName: IComparable
        where TRng: IComparable
        where TKind : IComparable
        where TAttribute:  ICharacterAttribute<int>
    {
        IDictionary<TAttributeIndex, TAttribute> BaseAttributes { get; set; }
    }
}
