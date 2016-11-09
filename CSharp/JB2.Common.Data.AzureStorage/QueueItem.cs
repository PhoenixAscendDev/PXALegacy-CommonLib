using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.WindowsAzure.Storage.Queue;

namespace JB2.Common.Data.AzureStorage
{
    public class QueueItem
    {

        #region Fields

        protected CloudQueueMessage _message;

        #endregion Fields


        #region Constructors 
        public QueueItem(string content)
        {
            _message = new CloudQueueMessage(content);
        }

        public QueueItem(byte[] content)
        {
            _message = new CloudQueueMessage(content);
        }

        public QueueItem(string id, string receipt)
        {
            _message = new CloudQueueMessage(id, receipt);
        }

        internal QueueItem(CloudQueueMessage message)
        {
            _message = message;
        }

        #endregion Constructors


        #region Properties

        
        public byte[] AsBytes { get { return _message.AsBytes } }
        //
        // Summary:
        //     Gets the content of the message, as a string.
        public string AsString { get { return _message.AsString} }
        //
        // Summary:
        //     Gets the number of times this message has been dequeued.
        public int DequeueCount { get { return _message.DequeueCount } }
        //
        // Summary:
        //     Gets the time that the message expires.
        public DateTimeOffset? ExpirationTime { get { return _message.ExpirationTime } }
        //
        // Summary:
        //     Gets the message ID.
        public string Id { get { return _message.Id } }
        //
        // Summary:
        //     Gets the time that the message was added to the queue.
        public DateTimeOffset? InsertionTime { get { return _message.InsertionTime } }
        //
        // Summary:
        //     Gets the time that the message will next be visible.
        public DateTimeOffset? NextVisibleTime { get { return _message.NextVisibleTime } }
        //
        // Summary:
        //     Gets the message's pop receipt.
        public string PopReceipt { get { return _message.PopReceipt } }


        #endregion Properties


        #region To Methods

        public CloudQueueMessage ToCloudQueueMessage()
        {
            return _message;
        }

        public override string ToString()
        {
            return _message.ToString();
        }

        #endregion To Methods

        #region Implicit Operators

        public static implicit operator CloudQueueMessage(QueueItem i)
        {
            return i.ToCloudQueueMessage();
        }

        public static implicit operator QueueItem(CloudQueueMessage message)
        {
            QueueItem result = new QueueItem(message);

            return result;
        }

        public static implicit operator string(QueueItem i)
        {
            return i._message.AsString;
        }

        public static implicit operator QueueItem(string content)
        {
            return new QueueItem(content);
        }

        public static implicit operator byte[] (QueueItem i)
        {
            return i._message.AsBytes;
        }

        public static implicit operator QueueItem(byte[] content)
        {
            return new QueueItem(content);
        }


        #endregion Implicit Operators


    }
}
