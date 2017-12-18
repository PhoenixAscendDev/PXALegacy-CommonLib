using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JB2.Common
{
    public abstract class EmailMessager : Messager<IEmailMessage<string,JB2.Common.Tag>,string,JB2.Common.Tag>
    {

    }
    public abstract class Messager : Messager<IMessage<string,JB2.Common.Tag>,string>
    {

    }
    public abstract class Messager<TMessage,TMessageKey> :  Messager<TMessage,TMessageKey,JB2.Common.Tag>, IMessagerAsync<TMessage, TMessageKey, JB2.Common.Tag>
        where TMessage : IMessage<TMessageKey, JB2.Common.Tag>
        where TMessageKey : IComparable
    {

    }

    public abstract class Messager<TMessage, TMessageKey, TMessageTag> : IMessagerAsync<TMessage, TMessageKey, TMessageTag>
        where TMessage : IMessage<TMessageKey, TMessageTag>
        where TMessageKey : IComparable
    {

        #region Events
        public event Action<IMessager<TMessage, TMessageKey, TMessageTag>, TMessage, DateTime> DeliverySuccess;
        public event Action<IMessager<TMessage, TMessageKey, TMessageTag>, TMessage, DateTime, IServiceResult<TMessage>> DeliveryFailed;
        public event Action<IMessager<TMessage, TMessageKey, TMessageTag>, TMessage, DateTime, IServiceResult<TMessage>> Delivered;

        protected virtual void OnDeliverySuccess(IMessager<TMessage, TMessageKey, TMessageTag> messager, TMessage message, DateTime timeDelivered)
        {
            if (DeliverySuccess != null)
                DeliverySuccess(messager,message, timeDelivered);
        }

        protected virtual void OnDeliveryFailed(IMessager<TMessage, TMessageKey, TMessageTag> messager, TMessage message, DateTime timeDelivered,IServiceResult<TMessage> result)
        {
            if (DeliveryFailed != null)
                DeliveryFailed(messager,message, timeDelivered,result);
        }

        protected virtual void OnDelivered(IMessager<TMessage, TMessageKey, TMessageTag> messager, TMessage message, DateTime timeDelivered, IServiceResult<TMessage> result)
        {
            if (Delivered != null)
                Delivered(messager,message, timeDelivered, result);

            if(result.ToBool())
            {
                if (DeliverySuccess != null)
                    DeliverySuccess(messager,message, timeDelivered);
            }
            else
            {
                if (DeliveryFailed != null)
                    DeliveryFailed(messager,message, timeDelivered, result);
            }
                
        }

        #endregion Events

        public abstract SendMessage<TMessage,TMessageKey,TMessageTag> SendMessageDelegate();

        public virtual ServiceResult<TMessage> Deliver(TMessage message)
        {
            return DeliverAsync(message);
        }

        public virtual  ServiceResult<TMessage> DeliverAsync(TMessage message)
        {
            JB2.Common.ServiceResult<TMessage> result = false;

            if (SendMessageDelegate() == null)
            {
                result = new ServiceResult<TMessage>(new Exception("SendMessage delegate not defined"));
                
                return result;
            }

            try
            {
                var d = SendMessageDelegate()?.Invoke(message);
                if(d)
                    message.DateSent = DateTime.Now;
                result = d;

            }
            catch(Exception ex)
            {
                result = new ServiceResult<TMessage>(ex);
            }
            finally
            {
                OnDelivered(this,message, message.DateSent, result);
                
            }

            return result;

        }



    }
}
