using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Common
{

    public interface IMessager:  IMessager<IMessage,string>
    {

    }

    public interface IMessager<TMessage,TMailbox>
        where TMessage : JB2.Common.IMessage 
    {
        ServiceResult<TMessage> Deliever(TMessage message);

        ServiceResult<IEnumerable<TMessage>> RecieveMessages(TMailbox mailbox);

        #region Events

        event Action<IMessager<TMessage,TMailbox>, TMessage, System.DateTime> DelieverySuccess;

        event Action<IMessager<TMessage, TMailbox>, TMessage, ServiceResult<TMessage>> DelieveryFailed;

        event Action<IMessager<TMessage, TMailbox>, TMessage, ServiceResult<TMessage>> Delivered;

        event Action<IMessager<TMessage, TMailbox>, IEnumerable<TMessage>, TMailbox,System.DateTime> Recieved;

        #endregion Events
    }
}
