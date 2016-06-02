using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Common
{
    public interface ICharacterAttribute: ICharacterAttribute<int>
    {

    }
    public interface ICharacterAttribute<TNumber>
    {
        TNumber GetValue();
        TNumber GetMultiplayer();

        Enum.CharacterAttributeType GetAttributeType();

        IEnumerable<ICharacterAttribute<TNumber>> GetChildren();
        




    }
}
