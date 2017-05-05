using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace JB2.Common.Scheduler
{
    public abstract class WorkFlow : ThreadScheduler<string, object>, ISchedulerAsync<string, object>
    {
        #region Fields

        protected IDictionary<string, WorkflowRule<string>> _rules;

        protected IDictionary<string, IJobAsync<string, object>> _jobs;

        protected int _currentStep = 0;
        protected IJobAsync<string, object> _lastJobRan;

        #endregion Fields

        #region Constructors

        public WorkFlow()
        {
            _rules = new Dictionary<string, WorkflowRule<string>>();
            _jobs = new Dictionary<string, IJobAsync<string, object>>();
        }

        #endregion Constructors


        #region IScheduler

        public override void Add(IJobAsync<string, object> job)
        {
            var rule = new WorkflowRule<string>();

            rule.JobID = job.ID;
            rule.LogSuccess = true;
            rule.LogFailure = false;
            rule.OnSuccess = Enum.StepType.GotoNext;
            rule.OnFailure = Enum.StepType.GotoNext;
            rule.StepNumber = _jobs.Count;

            job.Started += jobStartHandler;
            job.Completed += jobEndHandler;
            job.Failed += jobFailedHandler;

            _jobs.Add(job.ID, job);
            _rules.Add(job.ID, rule);
        }

        public override IEnumerable<IJobAsync<string, object>> GetJobs()
        {
            if (_jobs != null)
                return _jobs.Values;
            else
                return new IJobAsync<string, object>[0]; 
        }



        #endregion IScheduler

        public void Add(WorkflowTask task)
        {
           

            task.Started += jobStartHandler;
            task.Completed += jobEndHandler;
            task.Failed += jobFailedHandler;

            _jobs.Add(task.ID, task);
            _rules.Add(task.ID, task.GetRule());


        }



        public void Add(JobWork worktoDo, Enum.StepType onSuccess = Enum.StepType.GotoNext, Enum.StepType onFail = Enum.StepType.GotoNext, int stepNumber = 0)
        {
            var rule = new WorkflowRule<string>();

            var id = JB2.Common.NewID.ShortGuid();
            rule.JobID = id;
            rule.LogSuccess = false;
            rule.LogFailure = false;
            rule.StepNumber = stepNumber == 0 ? _jobs.Count : stepNumber;
            rule.OnFailure = onFail;
            rule.OnSuccess = onSuccess;

            var task = new WorkflowTask(id, worktoDo, rule);

            Add(task);
        }

        public override Task StopJobAsync()
        {
            return base.StopJobAsync();
        }

        public override async Task StartJobsAsync()
        {
            this._hasStarted = true;
            var logger = GetLogger();

            OnStarted(this);

            var job = getNextJobToRun(true);

            await runJob(job);


        }

        private async Task runJob(IJobAsync<string, object> task)
        {
            var logger = this.GetLogger();
            if (JB2.Helpers.SchedulerHelper.IsRealJob(task.GetType()))
            {
                try
                {                                
                    await task.StartAsync();
                }
                catch (Exception ex)
                {
                    var schedulerEx = new SchedulerException(string.Format("The Job  \"{0}\" could not be started successfully (JobID:{1})",
                                                        task.Name,
                                                        task.ID.ToString()), ex);
                    if (LoggingEnabled())
                        logger.LogError(schedulerEx);
                }
            }
            else
            {
                var schedulerEx = new SchedulerException(string.Format("The Job  \"{0}\" is not a valid Job (JobID:{1})",
                                                            task.Name,
                                                            task.ID.ToString()));
                if (LoggingEnabled())
                    logger.LogError(schedulerEx);
            }
        }

        public override void Remove(IJobAsync<string, object> job)
        {
            _jobs.Remove(job.ID);
            _rules.Remove(job.ID);
        }

        public void EnableLoging()
        {
            this._isLogEnabled = true;
        }

        private void jobStartHandler(IJob<string, object> task)
        {
            var logger = GetLogger();
            if (LoggingEnabled())
                logger.LogMessage(string.Format("The Job  \"{0}\" has been successfully been started (JobID:{1})",
                                                    task.Name,
                                                    task.ID.ToString()));
            _currentStep = _rules[task.ID].StepNumber;
            _lastJobRan = _jobs[task.ID];
            OnJobStarted(this, task, true);
        }

        private async void jobEndHandler(IJob<string, object> task, TimeSpan executeDuration)
        {
            //log it
            var logger = GetLogger();
            if (LoggingEnabled())
                logger.LogMessage(string.Format("The Job  \"{0}\" has been successfully completed (JobID:{1})",
                                                    task.Name,
                                                    task.ID.ToString()));
            //trigger event
            OnJobEnded(this, task, true,executeDuration);

            //run the next task
            var nextTask = getNextJobToRun(false, true);

            // if no nextTask then end the workflow
            if (nextTask == null)
                await this.StopJobAsync();
            else
                await runJob(nextTask);

        }

        private void jobCancelHander(IJob<string, object> task, TimeSpan duration)
        {
            var logger = GetLogger();
            if (LoggingEnabled())
                logger.LogMessage(string.Format("The Job  \"{0}\" has been cancelled (JobID:{1})",
                                                    task.Name,
                                                    task.ID.ToString()));
            OnJobEnded(this, task,false,duration);
        }

        private async void jobFailedHandler(IJob<string, object> task, ServiceResult result)
        {
            var logger = GetLogger();
            if (LoggingEnabled())
            {
                logger.LogMessage(string.Format("The Job  \"{0}\" has failed (JobID:{1})",
                                                    task.Name,
                                                    task.ID.ToString()));
                logger.LogError(result, result.Validation[0].Message);
            }
            
            OnJobFailed(this, task, result);
            //run the next task
            var nextTask = getNextJobToRun(false,result);

            // if no nextTask then end the workflow
            if (nextTask == null)
                await this.StopJobAsync();
            else
                await runJob(nextTask);

        }

        private IJobAsync<string,object> getNextJobToRun(bool first,bool success = true)
        {

            //blank last job means start from beginning
            if (first)
            {
                _currentStep = 0;
                var jobID = nextStepJobID();
                return _jobs[jobID];
            }

            else if (_lastJobRan != null)
            {
                var rule = _rules[_lastJobRan.ID];
                if (rule != null)
                {
                    Enum.StepType stepType = success ? rule.OnSuccess : rule.OnFailure;

                    switch (stepType)
                    {
                        default:
                        case Enum.StepType.GotoNext:
                            var jobid = nextStepJobID();
                            return string.IsNullOrEmpty(jobid) ? null : _jobs[jobid];
                        case Enum.StepType.JumptoJob:
                            var jobid2 = rule.JumpToJobID;
                            return string.IsNullOrEmpty(jobid2) ? null : _jobs[jobid2];
                        case Enum.StepType.Retry:
                            return _lastJobRan;
                        case Enum.StepType.Stop:
                            return null;
                    }
                }
                else
                {
                    return null;
                }

            }
            else
                return null;

        }

        private string nextStepJobID()
        {
            WorkflowRule<string> rule = null;
            for (int i = _currentStep; i < 100; i++)
            {
                var list = _rules.Values.Where(x => x.StepNumber == i && (_lastJobRan == null || x.JobID != _lastJobRan.ID));
                if(list.Count() > 0)
                {
                    rule = list.FirstOrDefault();
                    
                    break;
                }
            }

            if (rule != null)
                return rule.JobID;
            else
                return string.Empty;
            
                

        }

 



    }
}
