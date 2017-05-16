using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Queue;

using Microsoft.WindowsAzure.Storage.Auth;
using JB2.Common.Data.Enum;

namespace JB2.Common.Data
{
    public class AzureQueueRepository :  ContainerRepository
    {

        #region Fields
        private CloudQueueClient _queueClient;
        private CloudQueue _queue;
        private int _popVisibilityTimeout;
        private int _updateVisbilityTimeout;
        #endregion

        #region Constructors
        public AzureQueueRepository(string tableName) : this(AzureHelper.StorageAccount, tableName)
        {

        }


        public AzureQueueRepository(CloudStorageAccount account, string queueName): base(account)
        {
            _queueClient = account.CreateCloudQueueClient();
            _queue = _queueClient.GetQueueReference(queueName);
            _queue.CreateIfNotExistsAsync();

            _popVisibilityTimeout = 30;
            _updateVisbilityTimeout = 60;

        }
        #endregion Constructors

        #region Properties

        public int DefaultPopTimeoutSeconds
        {
            get
            {
                return _popVisibilityTimeout;
            }
            set
            {
                _popVisibilityTimeout = value;
            }
        }

        public int DefaultUpdateTimeoutSeconds
        {
            get
            {
                return _updateVisbilityTimeout;
            }
            set
            {
                _updateVisbilityTimeout = value;
            }
        }


        #endregion Properties

        public override string GetName()
        {
            return _queue.Name;
        }

        public override ContainerType GetContainerType()
        {
            return ContainerType.Queue;
        }

        #region Add Methods

        public void Push(string content)
        {
            Push(new QueueItem(content));
        }

        public void Push(byte[] content)
        {
            Push(new QueueItem(content));
        }

        public void Push(QueueItem item)
        {
            addItem(item, null, null, null, null);
        }

        public Task PushAsync(QueueItem item)
        {
            return addItemAsync(item, null, null, null, null, null);
        }

        public Task PushAsync(string content)
        {
            return PushAsync(new QueueItem(content));
        }

        public Task PushAsync(byte[] content)
        {
            return PushAsync(new QueueItem(content));
        }

        // Summary:
        //     Adds a message to the queue.
        //
        // Parameters:
        //   message:
        //     A Microsoft.WindowsAzure.Storage.Queue.CloudQueueMessage object.
        //
        //   timeToLive:
        //     A System.TimeSpan specifying the maximum time to allow the message to be in the
        //     queue, or null.
        //
        //   initialVisibilityDelay:
        //     A System.TimeSpan specifying the interval of time from now during which the message
        //     will be invisible. If null then the message will be visible immediately.
        //
        //   options:
        //     A Microsoft.WindowsAzure.Storage.Queue.QueueRequestOptions object that specifies
        //     additional options for the request. If null, default options are applied to the
        //     request.
        //
        //   operationContext:
        //     An Microsoft.WindowsAzure.Storage.OperationContext object that represents the
        //     context for the current operation.
        [DoesServiceRequest]
        protected void addItem(QueueItem item, TimeSpan? timeToLive = default(TimeSpan?), TimeSpan? initialVisibilityDelay = default(TimeSpan?), QueueRequestOptions options = null, OperationContext operationContext = null)
        {
            _queue.AddMessageAsync(item, timeToLive, initialVisibilityDelay, options, operationContext);
        }

        [DoesServiceRequest]
        protected Task addItemAsync(QueueItem item, TimeSpan? timeToLive, TimeSpan? initialVisibilityDelay, QueueRequestOptions options, OperationContext operationContext, CancellationToken? cancellationToken = null)
        {

            if (cancellationToken == null)
                return _queue.AddMessageAsync(item, timeToLive, initialVisibilityDelay, options, operationContext);

            return _queue.AddMessageAsync(item, timeToLive, initialVisibilityDelay, options, operationContext, cancellationToken.GetValueOrDefault());
        }


        #endregion Add Methods

        #region Peak Methods

        public QueueItem Peek()
        {
            return peek(null, null);
        }

        public Task<QueueItem> PeekAsync()
        {
            return peekAsync(null, null, null);
        }

        protected QueueItem peek(QueueRequestOptions options = null, OperationContext operationContext = null)
        {
            var message = _queue.PeekMessageAsync(options, operationContext).Result;

            return (QueueItem)message;
        }

        protected Task<QueueItem> peekAsync(QueueRequestOptions options, OperationContext operationContext, CancellationToken? cancellationToken = null)
        {
            var message = _queue.PeekMessageAsync(options, operationContext, cancellationToken.GetValueOrDefault());
            return convert(message);
        }

        #endregion Peak Methods

        #region Update Methods

        public ServiceResult Update(QueueItem item, int visibleForSeconds = -1)
        {
            if (visibleForSeconds < 0)
                visibleForSeconds = _updateVisbilityTimeout;
            return Update(item, TimeSpan.FromSeconds(visibleForSeconds));
        }


        public ServiceResult Update(QueueItem item, TimeSpan visibilityTimeout)
        {
            updateItem(item, visibilityTimeout, MessageUpdateFields.Content | MessageUpdateFields.Visibility);

            return true;
        }

        protected void updateItem(QueueItem item, TimeSpan visibilityTimeout, MessageUpdateFields updateFields, QueueRequestOptions options = null, OperationContext operationContext = null)
        {
            _queue.UpdateMessageAsync(item, visibilityTimeout, updateFields, options, operationContext);
        }


        #endregion Update Methods


        #region Pop/Get Methods

        public QueueItem Pop(TimeSpan? visibilityTimeout = null)

        {
            return pop((visibilityTimeout == null) ? TimeSpan.FromSeconds(_popVisibilityTimeout) : visibilityTimeout.GetValueOrDefault(), null, null);
        }

        public Task<QueueItem> PopAsync(TimeSpan? visibilityTimeout = null)
        {
            return popAsync((visibilityTimeout == null) ? TimeSpan.FromSeconds(_popVisibilityTimeout) : visibilityTimeout.GetValueOrDefault(), null, null);
        }


        protected QueueItem pop(TimeSpan? visibilityTimeout = default(TimeSpan?), QueueRequestOptions options = null, OperationContext operationContext = null)
        {
            return _queue.GetMessageAsync(visibilityTimeout, options, null).Result;
        }


        protected Task<QueueItem> popAsync(TimeSpan? visibilityTimeout, QueueRequestOptions options, OperationContext operationContext, CancellationToken? cancellationToken = null)
        {
            Task<CloudQueueMessage> item;
            if (cancellationToken == null)
                item = _queue.GetMessageAsync(visibilityTimeout, options, operationContext);
            else
                item = _queue.GetMessageAsync(visibilityTimeout, options, operationContext, cancellationToken.GetValueOrDefault());

            return convert(item);

        }


        #endregion Pop/Get Methods

        #region Pop Bulk Methods

        public IEnumerable<QueueItem> PopBulk(int count, int visibilitiySeconds = -1)
        {
            if (visibilitiySeconds < 0)
                visibilitiySeconds = _popVisibilityTimeout;

            return PopBulk(count, TimeSpan.FromSeconds(visibilitiySeconds));
        }

        public IEnumerable<QueueItem> PopBulk(int count, TimeSpan visibilityTimeout)
        {
            var list = _queue.GetMessagesAsync(count, visibilityTimeout, null, null).Result;

            return list.Cast<QueueItem>().ToList();
        }


        public Task<IEnumerable<QueueItem>> PopBulkAsync(int count)
        {
            var list = _queue.GetMessagesAsync(count, null, null, null);

            return convert(list);
        }

        public Task<IEnumerable<QueueItem>> PopBulkAsync(int count, int visibilitiySeconds = -1)
        {
            if (visibilitiySeconds < 0)
                visibilitiySeconds = _popVisibilityTimeout;

            return PopBulkAsync(count, TimeSpan.FromSeconds(visibilitiySeconds));
        }

        public Task<IEnumerable<QueueItem>> PopBulkAsync(int count, TimeSpan visibilityTimeout)
        {
            var list = _queue.GetMessagesAsync(count, visibilityTimeout, null, null);

            return convert(list);
        }


        #endregion Pop Bulk Methods


        #region Remove Methods

        public void Remove(QueueItem item)
        {
            removeItem(item, null, null);
        }

        public void Remove(string id, string receipt)
        {
            _queue.DeleteMessageAsync(id, receipt, null, null);
        }

        public Task RemoveAsync(QueueItem item)
        {
            return removeItemAsync(item, null, null, null);
        }

        public Task RemoveAsync(string id, string receipt)
        {
            return _queue.DeleteMessageAsync(id, receipt, null, null);
        }

        protected void removeItem(QueueItem item, QueueRequestOptions options = null, OperationContext operationContext = null)
        {
            _queue.DeleteMessageAsync(item, options, operationContext);
        }

        protected Task removeItemAsync(QueueItem item, QueueRequestOptions options, OperationContext operationContext, CancellationToken? cancellationToken = null)
        {
            if (cancellationToken == null)
                return _queue.DeleteMessageAsync(item, options, operationContext);
            else
            {
                string popreceipt = string.Empty;
                return _queue.DeleteMessageAsync(item, popreceipt, options, operationContext, cancellationToken.GetValueOrDefault());
            }
                
        }

        #endregion Remove Methods

        #region Helpers

        private async static Task<T> convert<T>(Task<object> task)
        {
            var result = await task;

            return (T)result;
        }

        private async static Task<QueueItem> convert(Task<CloudQueueMessage> task)
        {
            var result = await task;

            return (QueueItem)result;
        }

        private async static Task<IEnumerable<QueueItem>> convert(Task<IEnumerable<CloudQueueMessage>> task)
        {
            var result = await task;

            return result.Cast<QueueItem>().ToList();
        }




        #endregion Helpers


    }
}
