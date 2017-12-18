using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JB2.Common
{
    #region IMessager
    public interface IMessager<TMessage> : IMessager<TMessage, string, JB2.Common.Tag>
        where TMessage : IMessage<string, JB2.Common.Tag>
    {

    }
    public interface IMessager<TMessage,TMessageKey> : IMessager<TMessage,TMessageKey,JB2.Common.Tag>
        where TMessage : IMessage<TMessageKey, JB2.Common.Tag>
        where TMessageKey : IComparable
    {

    }

    public interface IMessager<TMessage, TMessageKey, TMessageTag>
        where TMessage : IMessage<TMessageKey,TMessageTag>
        where TMessageKey : IComparable
    {
        
        JB2.Common.ServiceResult<TMessage> Deliver(TMessage message);

        #region events
        event Action<IMessager<TMessage, TMessageKey, TMessageTag>, TMessage, DateTime> DeliverySuccess;
        event Action<IMessager<TMessage, TMessageKey, TMessageTag>, TMessage, DateTime, JB2.Common.IServiceResult<TMessage>> DeliveryFailed;
        event Action<IMessager<TMessage, TMessageKey, TMessageTag>, TMessage, DateTime, JB2.Common.IServiceResult<TMessage>> Delivered;
        #endregion events
    }

    #endregion IMessager

    #region IMessagerAsync

    public interface IMessagerAsync<TMessage> : IMessagerAsync<TMessage,string>
        where TMessage : IMessage<string, JB2.Common.Tag>
    {

    }


    public interface IMessagerAsync<TMessage, TMessageKey> : IMessagerAsync<TMessage, TMessageKey, JB2.Common.Tag>
        where TMessage : IMessage<TMessageKey, JB2.Common.Tag>
        where TMessageKey : IComparable
    {

    }

    public interface IMessagerAsync<TMessage, TMessageKey, TMessageTag> : IMessager<TMessage, TMessageKey, TMessageTag>
        where TMessage : IMessage<TMessageKey, TMessageTag>
        where TMessageKey : IComparable
    {
        JB2.Common.ServiceResult<TMessage> DeliverAsync(TMessage message);
    }



    #endregion IMessagerAsync

}
