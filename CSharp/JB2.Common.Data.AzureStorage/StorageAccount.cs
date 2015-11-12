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

        public AzureTableRepository GetTable(string tableName)
        {
            return new AzureTableRepository(_azureStorage, tableName);
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
            var storage = AzureHelper.GetStorageAccount(accountName, accountkey);
            return new StorageAccount(storage);
        }

        #endregion From 

    }
}
