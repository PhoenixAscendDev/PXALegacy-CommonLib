using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Azure.KeyVault;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

using Microsoft.WindowsAzure.Storage.Auth;
using JB2.Common.Data;

using JB2.Common.Extensions;
using JB2.Common.Data.Enum;

namespace JB2.Common.Data
{
    public class AzureTableRepository  : ContainerRepository
    {
        #region Fields
        private CloudTableClient _tableClient;
        private CloudTable _table;

        private LocalResolver _resolver;

        private Microsoft.Azure.KeyVault.Core.IKey _key;

        private TableRequestOptions _insertOptions;
        private TableRequestOptions _retrieveOptions;
        private TableOperationCollection _operations;

        #endregion Fields

        #region Constructors

        public AzureTableRepository(string tableName) : this(AzureHelper.StorageAccount, tableName)
        {

        }


        public AzureTableRepository(CloudStorageAccount account, string tableName) : base(account)
        {
            _tableClient = account.CreateCloudTableClient();
            _table = _tableClient.GetTableReference(tableName);

            _table.CreateIfNotExistsAsync();


            //_key = new RsaKey("private:key1");
            _resolver = new LocalResolver();

            //_resolver.Add(_key);
        }

        #endregion Constructors

        #region Properties

        #endregion Properties

        #region Inserts

        public void Insert<T>(T entity, TableInsertMode option, bool encypt) where T : ITableEntity
        {
            var result = InsertAsync<T>(entity, option, encypt);
        }


        public async Task<TableResult> InsertAsync<T>(T entity, TableInsertMode option, bool encypt)
            where T : ITableEntity
        {
            TableResult retrievedResult;
            switch (option)
            {
               
                case TableInsertMode.Insert:
                    retrievedResult = await _table.ExecuteAsync(TableOperation.Insert(entity), encypt ? this._insertOptions : null,null);
                    break;
                case TableInsertMode.Merge:
                    retrievedResult = await _table.ExecuteAsync(TableOperation.InsertOrMerge(entity), encypt ? this._insertOptions : null, null);
                   break;
                case TableInsertMode.Replace:
                    retrievedResult = await _table.ExecuteAsync(TableOperation.InsertOrReplace(entity), encypt ? this._insertOptions : null, null);
                    break;
                default:
                    retrievedResult = await _table.ExecuteAsync(TableOperation.Insert(entity), encypt ? this._insertOptions : null, null);
                    break;

            }

            return retrievedResult;


        }


        public void Insert<T>(T entity, bool replace,bool encypt)
            where T : ITableEntity
        {
            if (replace)
            {
                _table.ExecuteAsync(TableOperation.InsertOrReplace(entity), encypt ? this._insertOptions : null,null);
            }
            else
                _table.ExecuteAsync(TableOperation.Insert(entity), encypt ? this._insertOptions : null,null);
        }

        public async Task InsertAsync<T>(T entity, bool replace, bool encypt)
            where T : ITableEntity
        {
            if (replace)
            {
                await _table.ExecuteAsync(TableOperation.InsertOrReplace(entity), encypt ? this._insertOptions : null, null);
            }
            else
                await _table.ExecuteAsync(TableOperation.Insert(entity), encypt ? this._insertOptions : null, null);
        }

        public void Insert<T>(T entity, bool replace) where T : ITableEntity
        {
            Insert<T>(entity, replace, false);
        }

        public async void InsertAsync<T>(T entity, bool replace) where T : ITableEntity
        {
            await InsertAsync<T>(entity, replace, false);
        }


        public void Insert<T>(T entity) where T : ITableEntity
        {
            Insert<T>(entity, false);
        }

        public async Task InsertAsync<T>(T entity) where T : ITableEntity
        {
            await InsertAsync<T>(entity);
        }

        #endregion Inserts

        #region Updates

        public ServiceResult UpdateEntity<T>(String partitionKey, String rowKey) where T : class, ITableEntity, new()
        {
            return UpdateEntityAsync<T>(partitionKey, rowKey).Result;
        }

        public ServiceResult UpdateEntity<T>(T entity) where T : class, ITableEntity, new()
        {
            return UpdateEntityAsync(entity).Result;
        }

        public async Task<ServiceResult> UpdateEntityAsync<T>(T entity) where T : class, ITableEntity, new()
        {
            ServiceResult isUpdate = false;
            try
            {
                // Create the InsertOrReplace TableOperation
                TableOperation updateOperation = TableOperation.Replace(entity);

                // Execute the operation.
                await _table.ExecuteAsync(updateOperation);
                isUpdate = true;
            }
            catch (Exception ex)
            {
                isUpdate = false;
            }

            return isUpdate;
        }

        public async Task<ServiceResult> UpdateEntityAsync<T>(String partitionKey, String rowKey) where T : class, ITableEntity, new()
        {
            TableOperation retrieveOperation = TableOperation.Retrieve<T>(partitionKey, rowKey);

            // Execute the operation.
            TableResult retrievedResult = await _table.ExecuteAsync(retrieveOperation);

            // Assign the result to a object.
            var updateEntity = (T)retrievedResult.Result;

            if (updateEntity != null)
            {
                // Create the InsertOrReplace TableOperation
                TableOperation updateOperation = TableOperation.Replace(updateEntity);

                // Execute the operation.
                await _table.ExecuteAsync(updateOperation);
            }
            return true;
        }


        #endregion Updates

        #region Retrieves
        public IEnumerable<T> GetByPartitionKey<T>(string partitionKey, int noOfRecords = 0) where T : ITableEntity, new()
        {
            var query = new TableQuery<T>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey));
            // result =  this.E   _table.ExecuteQuery(query).Take(noOfRecords).ToList();
            var result = ExecuteQuery<T>(query, noOfRecords);

            return result;
        }

        public T GetEntity<T>(string partitionKey, string rowKey, bool decrypt = false) where T : class, ITableEntity, new()
        {
            return GetEntityAsync<T>(partitionKey, rowKey).Result;
        }

        public async Task<T> GetEntityAsync<T>(string partitionKey, string rowKey, bool decrypt =false) where T : class, ITableEntity, new()
        {
            var retrieveOperation = TableOperation.Retrieve<T>(partitionKey, rowKey);
            // Execute the retrieve operation.
            var retrievedResult = await _table.ExecuteAsync(retrieveOperation);
            return retrievedResult.Result as T;
        }

        public IEnumerable<T> GetByRowKeyStartWith<T>(string partitionKey, string startwith, int noOfRecords, bool decrypt = false) where T : ITableEntity, new()
        {
            var query = new TableQuery<T>().Where(startsWithfilter(partitionKey, startwith));
            var result = ExecuteQuery<T>(query, noOfRecords); //_table.StartsWith<T>(partitionKey, startwith, "RowKey", noOfRecords, decrypt);
            return result;
        }

        #endregion Retrieves

        #region Deletes

        public ServiceResult Delete<T>(String partitionKey, String rowKey) where T : class, ITableEntity, new()
        {
            TableOperation retrieveOperation = TableOperation.Retrieve<T>(partitionKey, rowKey);

            // Execute the operation.
            var retrievedResult = _table.ExecuteAsync(retrieveOperation).Result;

            // Assign the result to a CustomerEntity.
            var deleteEntity = (T)retrievedResult.Result;

            // Create the Delete TableOperation.
            if (deleteEntity != null)
            {
                TableOperation deleteOperation = TableOperation.Delete(deleteEntity);

                // Execute the operation.
                _table.ExecuteAsync(deleteOperation);
                
            }

            return true;
        }

        public ServiceResult DeleteAllByPartitionKey(string partitionKey)
        {
            var elist = GetByPartitionKey<DynamicTableEntity>(partitionKey, 1000);

            var operations = new TableOperationCollection();

            foreach(var e in elist)
            {
                operations.Add(AzureTableOperationType.Delete, e);
            }

            var result = ExecuteBulk(operations);

            return result.Count > 0;

        }

        #endregion Deletes

        #region Counts

        public int GetCountByPartitionKey(string partitionKey)
        {
            TableQuery<DynamicTableEntity> query = new TableQuery<DynamicTableEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey)).Select(new List<string> { "PartitionKey", "RowKey", "Timestamp" });

            return this.ExecuteQuery<DynamicTableEntity>(query, 0).Count();
        }

        #endregion Counts

        public int GetCountByRowKeyStartWith(string partitionKey, string startwith)
        {
            var query = new TableQuery<DynamicTableEntity>().Where(startsWithfilter(partitionKey, startwith)).Select(new List<string> { "PartitionKey", "RowKey", "Timestamp" });
            return this.ExecuteQuery<DynamicTableEntity>(query, 0).Count();
        }

        #region Execute 

        public async Task<IEnumerable<T>> ExecuteQueryAsync<T>(TableQuery<T> query, int noOfRecords = 0)  where T : ITableEntity, new()
        {
            List<T> result = null;

            if (noOfRecords < 0)
                noOfRecords = 0;

            if (noOfRecords != 0 && noOfRecords <= 1000)
            {
                var all = await _table.ExecuteQuerySegmentedAsync<T>(query, null);


                result = all.Take(noOfRecords).ToList();
            }
            else
            {
                TableContinuationToken continuationToken = null;
                result = new List<T>(noOfRecords);
                do
                {
                    //Console.WriteLine(result.Count());
                    ///https://azure.microsoft.com/en-us/documentation/articles/storage-dotnet-how-to-use-tables/
                    // Retrieve a segment (up to 1,000 entities).
                    TableQuerySegment<T> tableQueryResult = await _table.ExecuteQuerySegmentedAsync(query, continuationToken);

                    // Assign the new continuation token to tell the service where to
                    // continue on the next iteration (or null if it has reached the end).
                    continuationToken = tableQueryResult.ContinuationToken;

                    // Print the number of rows retrieved.
                    //Console.WriteLine("Rows retrieved {0}", tableQueryResult.Results.Count);
                    result.AddRange(tableQueryResult.Results);

                    //we have enough records
                    if (noOfRecords != 0 && result.Count() >= noOfRecords)
                        continuationToken = null;

                    // Loop until a null continuation token is received, indicating the end of the table.
                } while (continuationToken != null);
                Console.WriteLine("done with query");
            }
            //Console.WriteLine("return results:" + result.Count());
            if (noOfRecords != 0)
                return result.Take(noOfRecords).ToList();
            return result.ToList();
        }

        public IEnumerable<T> ExecuteQuery<T>(TableQuery<T> query, int noOfRecords = 0) where T : ITableEntity, new()
        {
            return ExecuteQueryAsync<T>(query, noOfRecords).Result;
        }

        #endregion Execute




        public ServiceResult SetEncyptKey(Microsoft.Azure.KeyVault.Core.IKey key)
        {

            try
            {
                this._key = key;
                this._resolver = new LocalResolver();
                this._resolver.Add(key);

                _insertOptions = new TableRequestOptions()
                {
                     
                    
                    //EncryptionPolicy = new TableEncryptionPolicy(this._key, null)
                };

                _retrieveOptions = new TableRequestOptions()
                {
                   // EncryptionPolicy = new TableEncryptionPolicy(null, this._resolver)
                };


                return true;
            }
            catch(Exception ex)
            {
                return new ServiceResult(ex);
            }
        }

        public async Task<IList<TableResult>> ExecuteBulkAsync(TableOperationCollection collection)
        {
            var result = new List<TableResult>();

            foreach (var pkey in collection.PartitionKeys)
            {
                var operations = collection.GetOperationsByPartitionKey(pkey);

                TableBatchOperation batch = new TableBatchOperation();
                foreach (var o in operations)
                {
                    if (batch.Count == 100)
                    {
                        var batchResults = await _table.ExecuteBatchAsync(batch);
                        result.AddRange(batchResults);
                        batch = new TableBatchOperation();
                    }
                    batch.Add(o);
                }
                if (batch.Count > 0)
                {
                    var batchResults = await _table.ExecuteBatchAsync(batch);
                    result.AddRange(batchResults);
                }
            }

            return result;
        }

        public IList<TableResult> ExecuteBulk(TableOperationCollection collection)
        {
            return ExecuteBulkAsync(collection).Result;
        }

        public override string GetName()
        {
            return _table.Name;
        }

        public override ContainerType GetContainerType()
        {
            return ContainerType.Table;
        }


        #region helpers

        private string startsWithfilter(string partitionKey, string searchStr,string columnName = "RowKey")
        {
            if (string.IsNullOrEmpty(searchStr)) return null;

            char lastChar = searchStr[searchStr.Length - 1];
            char nextLastChar = (char)((int)lastChar + 1);
            string nextSearchStr = searchStr.Substring(0, searchStr.Length - 1) + nextLastChar;
            string prefixCondition = TableQuery.CombineFilters(
                TableQuery.GenerateFilterCondition(columnName, QueryComparisons.GreaterThanOrEqual, searchStr),
                TableOperators.And,
                TableQuery.GenerateFilterCondition(columnName, QueryComparisons.LessThan, nextSearchStr)
                );

            string filterString = TableQuery.CombineFilters(
                TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey),
                TableOperators.And,
                prefixCondition
                );

            return filterString;
        }

        #endregion helpers


    }
}
