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
   
    public abstract class ThreadScheduler<TJobKey,TJobParameter> : IScheduler<TJobKey,TJobParameter>
        where TJobKey : IComparable
    {
        #region Fields
        private bool _isLogEnabled;
        private bool _hasStarted;
        #endregion Fields


        //http://www.codeproject.com/Articles/591271/A-Simple-Scheduler-in-Csharp

        public void StartJobs()
        {

            _hasStarted = true;
            var logger = GetLogger();

            if (Started != null)
                Started(this);                            

            var jobs = GetJobs();
            if ((jobs != null) && (jobs.Count() > 0))
            {
                Thread thread = null;
                foreach (var job in jobs)
                {
                    if( SchedulerHelper.IsRealJob(job.GetType()))
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
                        catch(Exception ex)
                        {
                            var schedulerEx = new SchedulerException(string.Format("The Job  \"{0}\" could not be started successfully (JobID:{1})",
                                                                job.Name,
                                                                job.ID.ToString()), ex);
                            if(LoggingEnabled())
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
        }

        public virtual void StopJobs()
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

                if (Stopped != null)
                    Stopped(this);
            }
        }


        public abstract IEnumerable<IJob<TJobKey, TJobParameter>> GetJobs();
        public abstract JB2.Common.ILogger GetLogger();

        public abstract void Add(IJob<TJobKey, TJobParameter> job);
        public abstract void Remove(IJob<TJobKey, TJobParameter> job);
        public abstract bool LoggingEnabled();

        public event Action<IScheduler<IJob<TJobKey, TJobParameter>, TJobKey, TJobParameter>> Started;
        public event Action<IScheduler<IJob<TJobKey, TJobParameter>, TJobKey, TJobParameter>> Stopped;




    }
}
