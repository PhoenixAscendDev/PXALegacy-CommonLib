using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Common;

using JB2.Common.Log;

namespace JB2.Infrastructure
{
    public class ProjectLogger : JB2.Common.Log.Logger
    {
        #region Fields

        private ILogRepo _repo;

        #endregion Fields

        #region Constructor

        public ProjectLogger(ILogRepo repo)
        {
            _repo = repo;
        }

        #endregion Constructor

        public override void Log(ILogEntry entry)
        {
            _repo.StoreLogEntry(entry);
        }
    }
}
