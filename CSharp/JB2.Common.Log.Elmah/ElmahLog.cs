using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Data;

using System.Web;



using Elmah;

namespace JB2.Common.Log
{

    public class ElmahLogManagerModule : ErrorLogModule
    {
        #region Fields
        private ErrorLog prevErrorLog;
        private static bool enableExceptionsToRepo;
        private ElmahLogManager prevErrorLogManager;
        private ReaderWriterLockSlim cacheLock = new ReaderWriterLockSlim();
        private List<string> exceptionTypesFiltered;
        private ExceptionFilterEventHandler exceptionTypeFilter;

        private ILogRepo repository;
        #endregion Fields

        public ElmahLogManagerModule()
        {
            this.Logged += new Elmah.ErrorLoggedEventHandler(ErrorLog_Logged);
            enableExceptionsToRepo = JB2.Common.Log.LogManager.IsRepoEnabled;
            this.repository = JB2.Common.Log.LogManager.Repository;
        }

        public void ErrorLog_Logged(object sender, ErrorLoggedEventArgs args)
        {
            //Attempt to log the Error to the database,  if enabled
            try
            {
                if (this.repository != null)
                {
                    this.repository.StoreLogEntry(CovertToLogEntry(args));
                }

            }
            catch (Exception ex)
            {

            }
        }

        protected override ErrorLog GetErrorLog(HttpContext context)
        {
            var result = base.GetErrorLog(context);
            this.cacheLock.EnterUpgradeableReadLock();
            try
            {
                var ignoreTypes = "ViewStateException";
                if (this.exceptionTypeFilter == null && !String.IsNullOrEmpty(ignoreTypes))
                {
                    this.exceptionTypesFiltered = new List<string>(ignoreTypes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
                    this.exceptionTypesFiltered.Sort();
                    this.exceptionTypeFilter = new ExceptionFilterEventHandler((sender, e) =>
                    {
                        var exceptionTypeNames = new List<string>();
                        var exception = e.Exception;
                        while (exception != null)
                        {
                            exceptionTypeNames.Add(exception.GetType().Name);
                            exception = exception.InnerException;
                        }

                        foreach (var exceptionTypeName in exceptionTypeNames)
                        {
                            if (this.exceptionTypesFiltered.BinarySearch(exceptionTypeName) >= 0)
                            {
                                e.Dismiss();
                            }
                        }
                    });
                    this.Filtering += this.exceptionTypeFilter;
                }

                if (result != this.prevErrorLog)
                {
                    this.cacheLock.EnterWriteLock();
                    try
                    {
                        this.prevErrorLog = result;
                        this.prevErrorLogManager = new ErrorLogManager(result);
                    }
                    finally
                    {
                        this.cacheLock.ExitWriteLock();
                    }
                }

                result = this.prevErrorLogManager;
            }
            finally
            {
                this.cacheLock.ExitUpgradeableReadLock();
            }

            return result;
        }

        public static ILogEntry CovertToLogEntry(ErrorLoggedEventArgs args)
        {
            Elmah.ErrorLogEntry e = args.Entry;
            return new LogEntry(e.Id, Enum.LogServerityType.Error, e.Error.Message, e.Error.Exception, e.Error.Time);
        }

    }
}







