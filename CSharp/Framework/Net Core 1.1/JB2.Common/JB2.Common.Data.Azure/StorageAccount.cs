using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.WindowsAzure.Storage;

namespace JB2.Common.Data
{
    public class StorageAccount : IStorageAccount
    {
        #region Fields

        private CloudStorageAccount _azureStorage;
        #endregion Fields

        #region Constructors

        private StorageAccount(CloudStorageAccount azureStorage)
        {
            _azureStorage = azureStorage;
        }


        #endregion Constructors

        #region Properties
        public AzureBlobRepository GetBlog(string containerName)
        {
            return new AzureBlobRepository(_azureStorage, containerName);
        }

        public AzureBlobRepository GetBlob(string containerName)
        {
            return new AzureBlobRepository(_azureStorage, containerName);
        }

        public AzureTableRepository GetTable(string tableName)
        {
            try
            {
                return new AzureTableRepository(_azureStorage, tableName);
            }
            catch (StorageException ex)
            {
                throw new JB2.Common.Exceptions.StorageTableException(string.Format("Error connecting to table {0}", tableName), _azureStorage, tableName, ex);
            }

        }

        public AzureQueueRepository GetQueue(string queue)
        {
            try
            {
                return new AzureQueueRepository(_azureStorage, queue);
            }
            catch (StorageException ex)
            {
                throw new JB2.Common.Exceptions.StorageQueueException(string.Format("Error connecting to queue {0}", queue), _azureStorage, queue, ex);
            }
        }

        #endregion Properties

        #region Operators

        public static implicit operator CloudStorageAccount(StorageAccount account)
        {
            return account._azureStorage;
        }

        public static implicit operator StorageAccount(CloudStorageAccount account)
        {
            return new StorageAccount(account);
        }

        #endregion Operators

        #region From Static

        public static StorageAccount FromAzureStorage(string accountName, string accountkey)
        {
            CloudStorageAccount storage = null;
            try
            {
                storage = AzureHelper.GetStorageAccount(accountName, accountkey);
                var account = new StorageAccount(storage);

            }
            catch (Exception ex)
            {
                storage = Microsoft.WindowsAzure.Storage.CloudStorageAccount.DevelopmentStorageAccount;

            }
            return new StorageAccount(storage);
        }

        public static StorageAccount FromAzureStorage(CloudStorageAccount azure)
        {
            
            try
            {
                return new StorageAccount(azure);

            }
            catch (Exception ex)
            {
                var d = Microsoft.WindowsAzure.Storage.CloudStorageAccount.DevelopmentStorageAccount;

                return new StorageAccount(d);

            }
            
        }

        #endregion From 

    }
}
