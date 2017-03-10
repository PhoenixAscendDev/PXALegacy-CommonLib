using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace JB2.Common.Data
{
    public class AzureTableEntity : TableEntity,ITableEntity, JB2.Common.IIDNamePair<string,string>
    {
        #region Fields
       // protected TableEntity _entity;
        #endregion Fields


        #region Constructors
        public AzureTableEntity() : base()
        {
           // _entity = new TableEntity();
        }

        public AzureTableEntity(string partitionKey, string rowKey) : base(partitionKey,rowKey)
        {
           // _entity = new TableEntity(partitionKey, rowKey);
        }

        #endregion Constructors

        #region ITableEntity

        //public string ETag
        //{
        //    get
        //    {
        //        return _entity.ETag;
        //    }

        //    set
        //    {
        //        _entity.ETag = value;
        //    }
        //}

        

        //public string PartitionKey
        //{
        //    get
        //    {
        //        return _entity.PartitionKey;
        //    }

        //    set
        //    {
        //        _entity.PartitionKey = value;
        //    }
        //}

        //public string RowKey
        //{
        //    get
        //    {
        //        return _entity.RowKey;
        //    }

        //    set
        //    {
        //        _entity.RowKey = value;
        //    }
        //}

        //public DateTimeOffset Timestamp
        //{
        //    get
        //    {
        //        return _entity.Timestamp;
        //    }

        //    set
        //    {
        //        _entity.Timestamp = value;
        //    }
        //}

        

        //public void ReadEntity(IDictionary<string, EntityProperty> properties, OperationContext operationContext)
        //{
        //    _entity.ReadEntity(properties, operationContext);
        //}

        //public IDictionary<string, EntityProperty> WriteEntity(OperationContext operationContext)
        //{
        //    return _entity.WriteEntity(operationContext);
        //}

        #endregion ITableEntity

        #region IIDNamePair
        public string ID
        {
            get;set;
            
        }

        public string Name
        {
            get;set;
           
        }

        public string GetID()
        {
            return ID;
        }

        public string GetName()
        {
            return Name;
        }

        #endregion IIDNamePair







    }
}
