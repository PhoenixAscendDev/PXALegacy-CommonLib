using System;
using System.Collections.Generic;
using System.Text;

namespace JB2.Common.Scheduler
{
    public class SimpleWorkflow : WorkFlow
    {
        #region Fields

        protected ILogger _logger;
        protected bool _logenabled;

        #endregion Fields

        #region Constructors

        public SimpleWorkflow(ILogger logger, IEnumerable<WorkflowTask> tasks) : this(logger)
        {
            _logger = logger;
            foreach(var task in tasks)
            {
                this.Add(task);
            }

        }

        public SimpleWorkflow(ILogger logger) : this()
        {
            _logger = logger;
        }

        public SimpleWorkflow()
        {

        }

        #endregion Constructors




        public override ILogger GetLogger()
        {
            return _logger;
        }

        public override bool LoggingEnabled()
        {
            return (_logger != null && _logenabled);
        }
    }
}
