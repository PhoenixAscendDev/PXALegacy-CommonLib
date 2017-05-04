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
        #endregion Fields


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
        public abstract JB2.Common.ILogger GetLogger();

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
                                                                    job.ID.ToString()));
                        }
                        catch (Exception ex)
                        {
                            var schedulerEx = new SchedulerException(string.Format("The Job  \"{0}\" could not be started successfully (JobID:{1})",
                                                                job.Name,
                                                                job.ID.ToString()), ex);
                            if (LoggingEnabled())
                                logger.LogError(schedulerEx);
                        }
                    }
                    else
                    {
                        var schedulerEx = new SchedulerException(string.Format("The Job  \"{0}\" is not a valid Job (JobID:{1})",
                                                                    job.Name,
                                                                    job.ID.ToString()));
                        if (LoggingEnabled())
                            logger.LogError(schedulerEx);
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
            if (Stopped != null)
                Stopped(schedule);
        }

        public void OnStarted(IScheduler<IJobAsync<TJobKey, TJobParameter>, TJobKey, TJobParameter> schedule)
        {
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
