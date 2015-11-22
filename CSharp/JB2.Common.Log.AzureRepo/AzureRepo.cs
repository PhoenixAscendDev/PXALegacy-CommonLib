using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Common.Log.Enum;


using JB2.Common.Data;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;


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
            var eList = _table.ExecuteQuery<LogTableEntry>(azurequeryfromSearch(search,_partition).Take(search.MaxRecordReturned));
            List<ILogEntry> result = new List<ILogEntry>(eList.Count());
            foreach(LogTableEntry e in eList)
            {
                result.Add(logfromentry(e));
            }

            return result;
        }

        public ILogEntry GetLogEntry(ILogSearch search)
        {
            return GetLogEntries(search).FirstOrDefault();
        }

        public bool StoreLogEntry(ILogEntry entry)
        {
            //because of how NoSQL we are storing the entry multiple times for fast query
            try
            {
                var te = entryfromLog(entry, _partition);

                te.RowKey = "id:" + entry.ID;
                _table.Insert<LogTableEntry>(te);

                te.RowKey = "serverity: " + entry.Serverity.ToString() + ":" + entry.ID;
                _table.Insert<LogTableEntry>(te);

                te.RowKey = "logdate:" + ((JB2Date)entry.LogDate).DateKey + ":" + entry.ID;
                _table.Insert<LogTableEntry>(te);


                te.PartitionKey = te.PartitionKey + ":" + ((JB2Date)entry.LogDate).DateKey;
                te.RowKey = "id:" + entry.ID;
                _table.Insert<LogTableEntry>(te);

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }




        #region private static


        private static LogTableEntry entryfromLog(ILogEntry e, string partition)
        {
            return new LogTableEntry(partition, "id:" + e.ID)
            {
                Exception = e.Exception == null ? string.Empty : e.Exception.ToString(),
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

        private static TableQuery<LogTableEntry> azurequeryfromSearch(ILogSearch s,string partitionKey)
        {
            
            List<string> conditions = new List<string>();
            var partitionValue = partitionKey;

            //if IDs,Severity or LogDate is empty just return the top logs
            if ((s.IDs == null) && (s.Serveritys == null) && (s.LogDate == DateTime.MinValue))
            {
                string searchStr = "id:";
                char lastChar = searchStr[searchStr.Length - 1];
                char nextLastChar = (char)((int)lastChar + 1);
                string nextSearchStr = searchStr.Substring(0, searchStr.Length - 1) + nextLastChar;
                string condition = TableQuery.CombineFilters(
                    TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.GreaterThanOrEqual, searchStr),
                    TableOperators.And,
                    TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.LessThan, nextSearchStr)
                );
                conditions.Add(condition);
            }




            //search by IDs

            if (s.IDs != null)
            {
                foreach (string id in s.IDs)
                {
                    string searchStr = "id:" + id;
                    char lastChar = searchStr[searchStr.Length - 1];
                    char nextLastChar = (char)((int)lastChar + 1);
                    string nextSearchStr = searchStr.Substring(0, searchStr.Length - 1) + nextLastChar;
                    string condition = TableQuery.CombineFilters(
                        TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.GreaterThanOrEqual, searchStr),
                        TableOperators.And,
                        TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.LessThan, nextSearchStr)
                    );
                    conditions.Add(condition);
                }
            }

            //search by Serverity
            if (s.Serveritys != null)
            {
                foreach (LogServerityType severity in s.Serveritys)
                {
                    string searchStr = "serverity:" + severity.ToString() + ":";
                    char lastChar = searchStr[searchStr.Length - 1];
                    char nextLastChar = (char)((int)lastChar + 1);
                    string nextSearchStr = searchStr.Substring(0, searchStr.Length - 1) + nextLastChar;
                    string condition = TableQuery.CombineFilters(
                        TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.GreaterThanOrEqual, searchStr),
                        TableOperators.And,
                        TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.LessThan, nextSearchStr)
                    );
                    conditions.Add(condition);
                }
            }


            //search by Date
            if(s.LogDate != DateTime.MinValue)
            {
                string searchStr = "logdate:" + ((JB2Date)s.LogDate).DateKey + ":";
                char lastChar = searchStr[searchStr.Length - 1];
                char nextLastChar = (char)((int)lastChar + 1);
                string nextSearchStr = searchStr.Substring(0, searchStr.Length - 1) + nextLastChar;
                string condition = TableQuery.CombineFilters(
                    TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.GreaterThanOrEqual, searchStr),
                    TableOperators.And,
                    TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.LessThan, nextSearchStr)
                );
                conditions.Add(condition);

                partitionKey = partitionKey + ":" + ((JB2Date)s.LogDate).DateKey;
            }


            //combine the conditions
            var prefixCondition = string.Join("and", conditions.ToArray());

            string filterString = TableQuery.CombineFilters(
                TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey),
                TableOperators.And,
                prefixCondition
                );


            return new TableQuery<LogTableEntry>().Where(filterString);

        }





            




        //    public static IEnumerable<TElement> StartsWith<TElement>
        //(this CloudTable table, string partitionKey, string searchStr,
        //string columnName = "RowKey", int recordLimit = 1000, bool decrypt = false) where TElement : ITableEntity, new()
        //{
        //    if (string.IsNullOrEmpty(searchStr)) return null;

        //    char lastChar = searchStr[searchStr.Length - 1];
        //    char nextLastChar = (char)((int)lastChar + 1);
        //    string nextSearchStr = searchStr.Substring(0, searchStr.Length - 1) + nextLastChar;
        //    string prefixCondition = TableQuery.CombineFilters(
        //        TableQuery.GenerateFilterCondition(columnName, QueryComparisons.GreaterThanOrEqual, searchStr),
        //        TableOperators.And,
        //        TableQuery.GenerateFilterCondition(columnName, QueryComparisons.LessThan, nextSearchStr)
        //        );

        //    string filterString = TableQuery.CombineFilters(
        //        TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey),
        //        TableOperators.And,
        //        prefixCondition
        //        );
        //    var query = new TableQuery<TElement>().Where(filterString);
        //    return table.ExecuteQuery<TElement>(query).Take(recordLimit);
        //}


        #endregion private static


    }
}
