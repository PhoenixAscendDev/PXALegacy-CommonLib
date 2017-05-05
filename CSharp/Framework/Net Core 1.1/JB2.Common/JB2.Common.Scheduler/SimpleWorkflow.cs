using System;
using System.Collections.Generic;
using System.Text;

namespace JB2.Common.Scheduler
{
    public class SimpleWorkflow : WorkFlow
    {
        #region Fields

        protected ILoggerAsync _logger;
       

        #endregion Fields

        #region Constructors

        public SimpleWorkflow(ILoggerAsync logger, IEnumerable<WorkflowTask> tasks) : this(logger)
        {
            _logger = logger;
            foreach(var task in tasks)
            {
                this.Add(task);
            }

        }

        public SimpleWorkflow(ILoggerAsync logger) : this()
        {
            _logger = logger;
        }

        public SimpleWorkflow()
        {

        }

        #endregion Constructors




        public override ILoggerAsync GetLogger()
        {
            return _logger;
        }

        public override bool LoggingEnabled()
        {
            return (_logger != null && _isLogEnabled);
        }
    }
}
