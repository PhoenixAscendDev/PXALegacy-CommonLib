using System;
using System.Collections.Generic;
using System.Text;

namespace JB2.Common.Scheduler
{
    public class WorkflowRule<TJobKey>
    {
        public TJobKey JobID { get; set; }
        public Enum.StepType OnSuccess { get; set; }

        public Enum.StepType OnFailure { get; set; }

        public string JumpToJobID { get; set; }

        public bool LogSuccess { get; set; }
        public bool LogFailure { get; set; }

        public int StepNumber { get; set; }
    }
}
