using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Queue;

using Microsoft.WindowsAzure.Storage.Auth;

namespace JB2.Common.Data
{
    public class AzureQueueRepository
    {

        #region Fields
        private CloudQueueClient _queueClient;
        private CloudQueue _queue;

        #endregion


        #region Constructors
        public AzureQueueRepository(string tableName) : this(AzureHelper.StorageAccount, tableName)
        {

        }


        public AzureQueueRepository(CloudStorageAccount account, string queueName)
        {
            _queueClient = account.CreateCloudQueueClient();
            _queue = _queueClient.GetQueueReference(queueName);
            _queue.CreateIfNotExists();

        }
        #endregion Constructors


        #region Methods


        #endregion Methods
    }
}
