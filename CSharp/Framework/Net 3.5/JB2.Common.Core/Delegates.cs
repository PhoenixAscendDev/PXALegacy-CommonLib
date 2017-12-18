using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JB2.Common
{
    public delegate ServiceResult<TMessage> SendMessage<TMessage, TMessageKey, TMessageTag>(TMessage message)
        where TMessage : IMessage<TMessageKey, TMessageTag>
        where TMessageKey : IComparable;
}
