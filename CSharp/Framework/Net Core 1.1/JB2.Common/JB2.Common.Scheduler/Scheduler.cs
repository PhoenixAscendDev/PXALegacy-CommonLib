using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using JB2.Common.Log;
using JB2.Helpers;

namespace JB2.Common.Scheduler
{
   
    public abstract class ThreadScheduler<TJobKey,TJobParameter> : ISchedulerAsync<TJobKey,TJobParameter>
        where TJobKey : IComparable
    {
        #region Fields
        protected bool _isLogEnabled;
        protected bool _hasStarted;

        protected static bool _addErrorCodes;
        #endregion Fields

        #region Constructors

        public ThreadScheduler()
        {
            if (!_addErrorCodes)
            {
                JB2.Dictionary.ErrorCodes.Add(new StatusCode("E-01-100300", "Error in starting Schedule Job"));
                JB2.Dictionary.ErrorCodes.Add(new StatusCode("E-01-100301", "Scheduler Job is not a valid Job"));
                JB2.Dictionary.ErrorCodes.Add(new StatusCode("E-01-100302", "Scheduler Job completed successfully"));
                JB2.Dictionary.ErrorCodes.Add(new StatusCode("E-01-100303", "Scheduler Job failed"));
                JB2.Dictionary.ErrorCodes.Add(new StatusCode("E-01-100304", "Scheduler Job cancelled"));
                JB2.Dictionary.ErrorCodes.Add(new StatusCode("E-01-100305", "Scheduler Job has started"));
                JB2.Dictionary.ErrorCodes.Add(new StatusCode("E-01-100306", "Scheduler has started"));
                JB2.Dictionary.ErrorCodes.Add(new StatusCode("E-01-100307", "Scheduler has stopped"));

                _addErrorCodes = true;
            }


        }

#endregion Constructors


        //http://www.codeproject.com/Articles/591271/A-Simple-Scheduler-in-Csharp

        public void StartJobs()
        {
            StartJobsAsync();
        }

        public virtual void StopJobs()
        {
            StopJobAsync();
        }


        public abstract IEnumerable<IJobAsync<TJobKey, TJobParameter>> GetJobs();
        public abstract JB2.Common.ILoggerAsync GetLogger();

        public abstract void Add(IJobAsync<TJobKey, TJobParameter> job);
        public abstract void Remove(IJobAsync<TJobKey, TJobParameter> job);
        public abstract bool LoggingEnabled();

        public virtual Task StartJobsAsync()
        {
            _hasStarted = true;
            var logger = GetLogger();

            OnStarted(this);

            var jobs = GetJobs();
            
            if ((jobs != null) && (jobs.Count() > 0))
            {
                
                Thread thread = null;
                foreach (var job in jobs)
                {
                    
                    
                    if (SchedulerHelper.IsRealJob(job.GetType()))
                    {
                        try
                        {
                            //job.Start();
                            thread = new Thread(new ThreadStart(job.Start));
                            thread.Start();
                            if (LoggingEnabled())
                                logger.LogMessage(string.Format("The Job  \"{0}\" has been successfully been started (JobID:{1})",
                                                                    job.Name,
                                                                    job.ID.ToString()), "E-01-100305");
                        }
                        catch (Exception ex)
                        {
                            var schedulerEx = new SchedulerException(string.Format("The Job  \"{0}\" could not be started successfully (JobID:{1})",
                                                                job.Name,
                                                                job.ID.ToString()), ex, "E-01-100300");
                            if (LoggingEnabled())
                                logger.LogError(schedulerEx,logcode: "E-01-100300");
                        }
                    }
                    else
                    {
                        var schedulerEx = new SchedulerException(string.Format("The Job  \"{0}\" is not a valid Job (JobID:{1})",
                                                                    job.Name,
                                                                    job.ID.ToString()), "E-01-100301");
                        if (LoggingEnabled())
                            logger.LogError(schedulerEx,logcode: "E-01-100301");
                    }
                }
            }

            return Task.CompletedTask;


        }

        public virtual Task StopJobAsync()
        {
            var logger = GetLogger();
            if (_hasStarted)
            {
                var jobs = GetJobs();
                foreach (var job in jobs)
                {
                    job.Cancel();
                }

                if (LoggingEnabled())
                     logger.LogMessage("Scheduler has stopped jobs");

                OnStopped(this);
            }

            return Task.CompletedTask;
        }

        public event Action<IScheduler<IJobAsync<TJobKey, TJobParameter>, TJobKey, TJobParameter>> Started;
        public event Action<IScheduler<IJobAsync<TJobKey, TJobParameter>, TJobKey, TJobParameter>> Stopped;
        public event Action<IScheduler<IJobAsync<TJobKey, TJobParameter>, TJobKey, TJobParameter>, IJob<TJobKey, TJobParameter>, ServiceResult,TimeSpan> JobEnded;
        public event Action<IScheduler<IJobAsync<TJobKey, TJobParameter>, TJobKey, TJobParameter>, IJob<TJobKey, TJobParameter>, ServiceResult> JobStarted;
        public event Action<IScheduler<IJobAsync<TJobKey, TJobParameter>, TJobKey, TJobParameter>, IJob<TJobKey, TJobParameter>, ServiceResult> JobFailed;

        public void OnStopped(IScheduler<IJobAsync<TJobKey, TJobParameter>, TJobKey, TJobParameter> schedule)
        {
            var logger = GetLogger();

            if (LoggingEnabled())
                logger.LogMessage(string.Format("Scheduler has stopped"), "E-01-100307");

            if (Stopped != null)
                Stopped(schedule);
        }

        public void OnStarted(IScheduler<IJobAsync<TJobKey, TJobParameter>, TJobKey, TJobParameter> schedule)
        {
            var logger = GetLogger();

            if (LoggingEnabled())
                logger.LogMessage(string.Format("Scheduler has started"), "E-01-100306");

            if (Started != null)
                Started(schedule);

            



        }




        public void OnJobEnded(IScheduler<IJobAsync<TJobKey, TJobParameter>, TJobKey, TJobParameter> schedule, IJob<TJobKey, TJobParameter> job, ServiceResult result,TimeSpan duration)
        {
            if (JobEnded != null)
                JobEnded(schedule, job, result,duration);
        }

        public void OnJobStarted(IScheduler<IJobAsync<TJobKey, TJobParameter>, TJobKey, TJobParameter> schedule, IJob<TJobKey, TJobParameter> job, ServiceResult result)
        {
            if (JobStarted != null)
                JobStarted(schedule, job, result);
        }

        public void OnJobFailed(IScheduler<IJobAsync<TJobKey, TJobParameter>, TJobKey, TJobParameter> schedule, IJob<TJobKey, TJobParameter> job, ServiceResult result)
        {
            if (JobFailed != null)
                JobFailed(schedule, job, result);
        }

    }
}
