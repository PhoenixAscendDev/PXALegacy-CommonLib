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

        #region Properties

        public string Serverity { get; set; }
        public string ID { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
        public string LogDate { get; set; }

        public string Tick { get; set; }

        public string LogCode { get; set; }

        public string Tags { get; set; }

        #endregion Properties
    }
}
