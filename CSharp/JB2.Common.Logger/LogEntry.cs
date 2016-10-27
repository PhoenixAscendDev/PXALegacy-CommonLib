using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;
using JB2.Common.Enum;

namespace JB2.Common.Log
{
    public  class LogEntry : LogEntry<string,LogServerityType>, ILogEntry
    {

        #region Constructors
            : base(id,serverity,message,exception,logDate,logCode)
        {
        }

        #endregion Constructors

        public static LogEntry NewLogEntry(LogServerityType serverity, string message,string logcode="")
        {

            return new LogEntry(JB2.Common.ShortGuid.NewGuid(), serverity, message,null,DateTime.Now,logcode);
        }

        public static LogEntry NewLogEntry(LogServerityType serverity, Exception exception,string logcode = "")
        {
            return new LogEntry(JB2.Common.ShortGuid.NewGuid(), serverity, exception.Message, exception, DateTime.Now,logcode);
            
        }
 
    }



    public class LogEntry<TKey,TServerity> : JB2Class, ILogEntry<TKey,TServerity>
    {
        #region Fields

        private readonly Exception _exception;
        private readonly TKey _id;
        private readonly string _message;
        private readonly DateTime _logDate;
        private TServerity _serverity;

        #endregion Fields

        #region Constructor

        {
            _exception = exception;
            _id = id;
            _logDate = logDate;
            _message = message;
            _serverity = serverity;
            _props.SetProperty<string>("LogCode", logCode);

        }

        #endregion Constructor

        #region Properties
        public Exception Exception { get { return _exception; } }
        public TKey ID { get { return _id; } }
        public DateTime LogDate { get { return _logDate; } }
        public string Message { get { return _message; } }
        public TServerity Serverity { get { return _serverity; } }

        public string LogCode
        {
            get
            {
                return _props.GetProperty<string>("LogCode", string.Empty);
            }
            set
            {
                _props.SetProperty<string>("LogCode", value);
            }
        }
        #endregion Properties


        #region ITags
        public IEnumerable<Tag> GetTags()
        {
        }

        public bool AddTag(Tag tag)
        {
        }

        public bool RemoveTag(Tag tag)
        {
        }
        #endregion ITags

        public static LogEntry<TKey,TServerity> NewLogEntry(TKey id, TServerity serverity,string message)
        {
            return new LogEntry<TKey, TServerity>(id, serverity, message, null, DateTime.Now);
        }
        public static LogEntry<TKey, TServerity> NewLogEntry(TKey id, TServerity serverity, Exception exception)
        {
             return new LogEntry<TKey, TServerity>(id, serverity, exception.Message, exception, DateTime.Now);
        }

        
    }
}
