using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Common
{
    public interface IMessageCollection<Tkey,TTag> : IIDNamePair<Tkey,string>, IObjectCollection<IMessage<Tkey,TTag>>
        where Tkey : IComparable
    {
        IEnumerable<IMessage<Tkey, TTag>> GetRead();
        IEnumerable<IMessage<Tkey, TTag>> GetUnread();
        IEnumerable<IMessage<Tkey, TTag>> GetByTag(TTag tag);
    }
}
