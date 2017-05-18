using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Common.Message
{
    public delegate Task<ServiceResult<IEnumerable<TMessage>>> RecieveNewMessages<TMessage, TMailbox>(TMailbox mailbox)
                        where TMessage : JB2.Common.Message.IMessage;
}
