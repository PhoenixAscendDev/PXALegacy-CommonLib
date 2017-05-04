using System;
using System.Collections.Generic;
using System.Text;

namespace JB2.Common.Scheduler
{
    public class WorkflowTask : Job
    {

        #region Fields
        protected WorkflowRule<string> _rule;
        #endregion Fields

        #region Constructor

        public WorkflowTask(string id, JobWork worktoDo,WorkflowRule<string> rule) : base()
        {
            this._workDelegate = worktoDo;
            this._id = id;


        }

        #endregion Constructor

        public WorkflowRule<string> GetRule()
        {
            return _rule;
        }


        public override int GetCoolDownSeconds()
        {
            return 0;
        }

        public override bool IsRepeatable()
        {
            return true;
        }
    }
}
