using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Common.Log.Enum;


using JB2.Common.Data;
using Microsoft.WindowsAzure.Storage;



namespace JB2.Common.Log
{
    public class AzureRepo : ILogRepo
    {
        #region Fields
        private readonly AzureTableRepository _table;
        private readonly string _partition;


        #endregion Fields


        public AzureRepo(CloudStorageAccount account,string tablename,string partitionKey = "logentry")
        {
            _table = new AzureTableRepository(account, tablename);
            _partition = partitionKey;
        }

        public AzureRepo(AzureTableRepository table, string partitionKey = "logentry")
        {
            _partition = partitionKey;
            _table = table;
        }


        public IEnumerable<ILogEntry> GetLogEntries(ILogSearch search)
        {
            throw new NotImplementedException();
        }

        public ILogEntry GetLogEntry(ILogSearch search)
        {
            throw new NotImplementedException();
        }


        private static LogTableEntry entryfromLog(ILogEntry e, string partition)
        {
            return new LogTableEntry()
            {
                Exception = e.Exception.ToString(),
                ID = e.ID,
                LogDate = e.LogDate.ToString(),
                Message = e.Message,
                Serverity = ((int)e.Serverity).ToString()
            };
        }

        private static ILogEntry logfromentry(LogTableEntry e)
        {
            LogServerityType type = (LogServerityType)Convert.ToInt32(e.Serverity);
            DateTime dt = Convert.ToDateTime(e.LogDate);
            return new LogEntry(e.ID, type, e.Message, null, dt);
        }


    }
}
