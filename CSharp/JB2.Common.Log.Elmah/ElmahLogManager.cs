using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Elmah;
namespace JB2.Common.Log
{

    public class ElmahLogManager : Elmah.ErrorLog
    {
        private ErrorLog _errorLog;
        private ILogRepo _repo;

        public ElmahLogManager(ErrorLog baseErrorLog, ILogRepo repo)
        {
            this._errorLog = baseErrorLog;
            this._repo = repo;
        }

        public override Elmah.ErrorLogEntry GetError(string id)
        {
            return this._errorLog.GetError(id);
        }

        public override int GetErrors(int pageIndex, int pageSize, System.Collections.IList errorEntryList)
        {
            return this._errorLog.GetErrors(pageIndex, pageSize, errorEntryList);



        }
        public override string Log(Elmah.Error error)
        {
            var logresult = this._errorLog.Log(error);

            return logresult;
        }
    }
}
