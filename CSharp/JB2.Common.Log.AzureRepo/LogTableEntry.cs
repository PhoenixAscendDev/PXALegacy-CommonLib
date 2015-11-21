using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Common.Log
{
    public class LogTableEntry :  Microsoft.WindowsAzure.Storage.Table.TableEntity
    {
        

        #region Constructors
        public LogTableEntry(string partitionKey, string rowKey)
        {
            this.PartitionKey = partitionKey;
            this.RowKey = rowKey;
        }


        public LogTableEntry()
        {

        }
        #endregion Constructors
    }
}
