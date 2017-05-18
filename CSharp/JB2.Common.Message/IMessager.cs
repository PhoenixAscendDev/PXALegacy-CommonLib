using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Common.Message
{

    #region IMessager
    public interface IMessager:  IMessager<string>
    {

    }

    public interface IMessager<TMailbox> : IMessager<IMessage,TMailbox>
    {

    }

    public interface IMessager<TMessage,TMailbox> : JB2.Common.IMessagerAsync<TMessage>
        where TMessage : JB2.Common.Message.IMessage 
    {
        ServiceResult<IEnumerable<TMessage>> RecieveMessages(TMailbox mailbox);

        #region Events

        event Action<IMessager<TMessage, TMailbox>, IEnumerable<TMessage>, TMailbox,System.DateTime> Recieved;

        #endregion Events
    }

    #endregion IMessager

    #region IMessagerAsync

    public interface IMessagerAsync<TMessage, TMailbox> : JB2.Common.Message.IMessager<TMessage,TMailbox>
       where TMessage : JB2.Common.Message.IMessage
    {
        Task<ServiceResult<IEnumerable<TMessage>>> RecieveMessagesAsync(TMailbox mailbox);
    }



    #endregion IMessagerAsync
}
