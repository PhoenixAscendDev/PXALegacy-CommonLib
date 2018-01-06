using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JB2.Common.Message
{
    public delegate ServiceResult<IEnumerable<TMessage>> RecieveNewMessages<TMessage, TMailbox>(TMailbox mailbox)
                        where TMessage : JB2.Common.Message.IMessage;
}
