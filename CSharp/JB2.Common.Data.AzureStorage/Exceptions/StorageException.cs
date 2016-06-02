using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Common.Exceptions
{
    public class StorageTableException : System.ApplicationException
    {
        private string stackTraceOverride;
        protected Microsoft.WindowsAzure.Storage.CloudStorageAccount _account;
        protected string _tableName;

        public StorageTableException(string message, Microsoft.WindowsAzure.Storage.CloudStorageAccount account,string tableName, Exception innerException)
            : base(message)
        {
            _tableName = tableName;
            _account = account;
        }

        public StorageTableException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        public void SetStackTrace(string stackTrace)
        {
            var lines = new List<string>(stackTrace.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries));
            while (lines.Count > 0 && lines[0].IndexOf(".LogMessage(") > 0)
            {
                lines.RemoveAt(0);
            }

            this.stackTraceOverride = String.Join("\r\n", lines.ToArray());
        }

        public override string StackTrace
        {
            get
            {
                return this.stackTraceOverride ?? base.StackTrace;
            }
        }

        public Microsoft.WindowsAzure.Storage.CloudStorageAccount Account
        {
            get
            {
                return _account;
            }
        }

        public string TableName
        {
            get
            {
                return _tableName;
            }
        }
    }
}
