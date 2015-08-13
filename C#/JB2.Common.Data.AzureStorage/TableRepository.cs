using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;

using JB2.Common.Extensions;


namespace JB2.Common.Data
{
    public class AzureTableRepository
    {

        private CloudTableClient _tableClient;
        private CloudTable _table;

        public AzureTableRepository(string tableName) : this(AzureHelper.StorageAccount,tableName)
        {
            
        }


        public AzureTableRepository(CloudStorageAccount account, string tableName)
        {
            _tableClient = account.CreateCloudTableClient();
            _table = _tableClient.GetTableReference(tableName);

            _table.CreateIfNotExists();
        }


        public void Insert<T>(T entity) where T : ITableEntity
        {
            Insert<T>(entity, false);
        }

        public void Insert<T>(T entity, bool replace) where T : ITableEntity
        {
            if (replace)
            {
                _table.Execute(TableOperation.InsertOrReplace(entity));
            }
            else
                _table.Execute(TableOperation.Insert(entity));
        }

        public IEnumerable<T> ExecuteQuery<T>(TableQuery<T> query) where T : class, ITableEntity, new()
        {
            return _table.ExecuteQuery(query);
        }

        public IEnumerable<T> GetByPartitionKey<T>(string partitionKey, int noOfRecords) where T : ITableEntity, new()
        {
            var query = new TableQuery<T>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey));
            var result = _table.ExecuteQuery(query).Take(noOfRecords).ToList();
            return result;
        }

        public T GetEntity<T>(string partitionKey, string rowKey) where T : class, ITableEntity, new()
        {
            var retrieveOperation = TableOperation.Retrieve<T>(partitionKey, rowKey);
            // Execute the retrieve operation.
            var retrievedResult = _table.Execute(retrieveOperation);
            return retrievedResult.Result as T;
        }

        public IEnumerable<T> GetByRowKeyStartWith<T>(string partitionKey, string startwith, int noOfRecords) where T : ITableEntity, new()
        {
            var result = _table.StartsWith<T>(partitionKey, startwith, "RowKey", noOfRecords);

            return result;
            //var query = new TableQuery<T>().Where(
            //    TableQuery.CombineFilters(
            //            TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey),
            //            TableOperators.And,
            //            TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.GreaterThan, startwith)));
            //var result = _table.ExecuteQuery(query).Take(noOfRecords).ToList();
            //return result;


        }


        public ServiceResult Delete<T>(String partitionKey, String rowKey) where T : class, ITableEntity, new()
        {
            TableOperation retrieveOperation = TableOperation.Retrieve<T>(partitionKey, rowKey);

            // Execute the operation.
            var retrievedResult = _table.Execute(retrieveOperation);

            // Assign the result to a CustomerEntity.
            var deleteEntity = (T)retrievedResult.Result;

            // Create the Delete TableOperation.
            if (deleteEntity != null)
            {
                TableOperation deleteOperation = TableOperation.Delete(deleteEntity);

                // Execute the operation.
                _table.Execute(deleteOperation);
            }

            return true;
        }

        public ServiceResult UpdateEntity<T>(String partitionKey, String rowKey) where T : class, ITableEntity, new()
        {
            TableOperation retrieveOperation = TableOperation.Retrieve<T>(partitionKey, rowKey);

            // Execute the operation.
            TableResult retrievedResult = _table.Execute(retrieveOperation);

            // Assign the result to a object.
            var updateEntity = (T)retrievedResult.Result;

            if (updateEntity != null)
            {
                // Create the InsertOrReplace TableOperation
                TableOperation updateOperation = TableOperation.Replace(updateEntity);

                // Execute the operation.
                _table.Execute(updateOperation);
            }
            return true;
        }

        public ServiceResult UpdateEntity<T>(T entity) where T : class, ITableEntity, new()
        {
            ServiceResult isUpdate = false;
            try
            {
                // Create the InsertOrReplace TableOperation
                TableOperation updateOperation = TableOperation.Replace(entity);

                // Execute the operation.
                _table.Execute(updateOperation);
                isUpdate = true;
            }
            catch (Exception ex)
            {
                isUpdate = false;
            }

            return isUpdate;
        }















    }
}
