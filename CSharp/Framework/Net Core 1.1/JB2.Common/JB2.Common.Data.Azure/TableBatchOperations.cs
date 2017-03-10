using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

using Microsoft.WindowsAzure.Storage.Auth;
using JB2.Common.Data;


namespace JB2.Common.Data
{
    public class TableOperationCollection
    {
        protected IDictionary<string, List<TableOperation>> _operations;

        public TableOperationCollection()
        {
            _operations = new Dictionary<string, List<TableOperation>>();
        }

        public TableOperationCollection(AzureTableOperationType operationType, ITableEntity entity) : this()
        {

            this.Add(operationType, entity);
        }

        public void Add(AzureTableOperationType operationType, ITableEntity entity)
        {
            var pkey = entity.PartitionKey;

            TableOperation o = null;

            switch (operationType)
            {
                case AzureTableOperationType.Delete:
                    o = TableOperation.Delete(entity);
                    break;
                case AzureTableOperationType.Insert:
                    o = TableOperation.Insert(entity);
                    break;
                case AzureTableOperationType.InsertOrMerge:
                    o = TableOperation.InsertOrMerge(entity);
                    break;
                case AzureTableOperationType.InsertOrReplace:
                    o = TableOperation.InsertOrReplace(entity);
                    break;
                case AzureTableOperationType.Merge:
                    o = TableOperation.Merge(entity);
                    break;
                case AzureTableOperationType.Replace:
                    o = TableOperation.Replace(entity);
                    break;
            }

            if (!_operations.ContainsKey(pkey))
            {
                List<TableOperation> list = new List<TableOperation>();
                list.Add(o);
                _operations.Add(pkey, list);
            }
            else
            {
                _operations[pkey].Add(o);
            }
        }
        public IDictionary<string, List<TableOperation>> OperationsByPartition
        {
            get
            {
                return _operations;
            }
        }

        public IEnumerable<string> PartitionKeys
        {
            get
            {
                return _operations.Keys;
            }
        }

        public IEnumerable<TableOperation> GetOperationsByPartitionKey(string partitionkey)
        {
            return _operations[partitionkey];
        }
    }
}
