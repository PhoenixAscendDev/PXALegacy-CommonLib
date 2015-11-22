using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Common.Log.Enum;

namespace JB2.Common.Log
{
    public sealed class LogSearch : ILogSearch
    {
        //public readonly IEnumerable<string> IDs;

        #region Constructors

        public LogSearch(IEnumerable<string> ids, IEnumerable<Enum.LogServerityType> serveritys,DateTime logDate,string comparison,int maxRecords = 200)
        {
            IDs = ids;
            Serveritys = serveritys;
            LogDate = logDate;
            Comparison = comparison;
            MaxRecordReturned = maxRecords;
        }

        #endregion Constructors


        public string Comparison
        {
            get; set;
        }

        public IEnumerable<string> IDs
        {
            get; set;
        }

        public DateTime LogDate
        {
            get; set;
        }

        public int MaxRecordReturned
        {
            get; set;
        }

        public IEnumerable<LogServerityType> Serveritys
        {
            get; set;
        }


        #region Static Methods

        public static LogSearch SearchByID(string id,string comparison = JB2.Common.QueryComparison.Equal)
        {
            return new LogSearch(new string[1] { id }, null, DateTime.MinValue, comparison);
        }
        public static LogSearch SearchBySeverity(Enum.LogServerityType serverity, string comparison = JB2.Common.QueryComparison.Equal)
        {
            return new LogSearch(null, new Enum.LogServerityType[1] { serverity }, DateTime.MinValue, comparison);
        }

        public static LogSearch SearchByDate(DateTime date, string comparison = JB2.Common.QueryComparison.Equal)
        {
            return new LogSearch(null, null, date, comparison);
        }

        #endregion Static Methods
    }
}
