using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Common.Message
{
    public abstract class Messager<TMessage,TMailbox> : JB2.Common.Messager<TMessage,string>, JB2.Common.Message.IMessagerAsync<TMessage,TMailbox>
        where TMessage : JB2.Common.Message.IMessage
    {
        public abstract RecieveNewMessages<TMessage, TMailbox> RecieveNewMessagesDelegate();
        #region Events


        public event Action<IMessager<TMessage, TMailbox>, IEnumerable<TMessage>, TMailbox, DateTime> Recieved;


        public void OnRecieved(IMessager<TMessage, TMailbox> messager, IEnumerable<TMessage> messages, TMailbox mailbox,DateTime timeRecieved)
        {
            if (Recieved != null)
                Recieved(messager, messages, mailbox,timeRecieved);
        }




        #endregion Events

        public ServiceResult<IEnumerable<TMessage>> RecieveMessages(TMailbox mailbox)
        {
            return RecieveMessagesAsync(mailbox).Result;
        }

        public async Task<ServiceResult<IEnumerable<TMessage>>> RecieveMessagesAsync(TMailbox mailbox)
        {

            JB2.Common.ServiceResult<IEnumerable<TMessage>> result = false;

            if (RecieveNewMessagesDelegate() == null)
            {
                result = new ServiceResult<IEnumerable<TMessage>>(new Exception("RecieveMessages delegate not defined"));

                return result;
            }

            try
            {
                var d = await Task.Run(() => RecieveNewMessagesDelegate()?.Invoke(mailbox));
                if (d)                   
                result = d;

            }
            catch (Exception ex)
            {
                result = new ServiceResult<IEnumerable<TMessage>>(ex);
            }
            finally
            {
                OnRecieved(this, result.ToObject(), mailbox, DateTime.Now);

            }

            return result;
        }

    }
}
