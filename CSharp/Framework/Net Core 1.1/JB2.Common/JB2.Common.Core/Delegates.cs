using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Common
{
    public delegate Task<ServiceResult<TMessage>> SendMessage<TMessage, TMessageKey, TMessageTag>(TMessage message)
        where TMessage : IMessage<TMessageKey, TMessageTag>
        where TMessageKey : IComparable;
}
