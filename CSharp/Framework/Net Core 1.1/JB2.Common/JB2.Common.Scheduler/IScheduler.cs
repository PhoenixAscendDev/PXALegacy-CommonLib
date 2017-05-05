using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Common.Scheduler
{
    public interface IScheduler: IScheduler<IJob<string,object>,string,object>
    {

    }

    public interface IScheduler<TJobKey,TJobParameter> : IScheduler<IJob<TJobKey,TJobParameter>,TJobKey,TJobParameter>
        where TJobKey : IComparable
    {

    }
    public interface IScheduler<TJob,TJobKey,TJobParameter>
        where TJob : IJob<TJobKey,TJobParameter>
        where TJobKey: IComparable
    {
        IEnumerable<TJob> GetJobs();
        void Add(TJob job);
        void Remove(TJob job);

        JB2.Common.ILoggerAsync GetLogger();

        void StartJobs();
        void StopJobs();

        bool LoggingEnabled();

        #region Events

        event Action<IScheduler<TJob,TJobKey, TJobParameter>> Started;
        event Action<IScheduler<TJob, TJobKey, TJobParameter>,IJob<TJobKey, TJobParameter>, ServiceResult> JobStarted;
        event Action<IScheduler<TJob, TJobKey, TJobParameter>, IJob<TJobKey, TJobParameter>, ServiceResult,TimeSpan> JobEnded;
        event Action<IScheduler<TJob, TJobKey, TJobParameter>, IJob<TJobKey, TJobParameter>, ServiceResult> JobFailed;
        event Action<IScheduler<TJob,TJobKey, TJobParameter>> Stopped;

        void OnStopped(IScheduler<TJob, TJobKey, TJobParameter> schedule);
        void OnStarted(IScheduler<TJob, TJobKey, TJobParameter> schedule);

        void OnJobEnded(IScheduler<TJob, TJobKey, TJobParameter> schedule, IJob<TJobKey, TJobParameter> job, ServiceResult result, TimeSpan duration);

        void OnJobStarted(IScheduler<TJob, TJobKey, TJobParameter> schedule, IJob<TJobKey, TJobParameter> job, ServiceResult result);
       

        #endregion Events
    }

    public interface ISchedulerAsync : ISchedulerAsync<string, object>
    {

    }

    

    public interface ISchedulerAsync<TJobKey, TJobParameter> : IScheduler<IJobAsync<TJobKey, TJobParameter>,TJobKey,TJobParameter>
        where TJobKey : IComparable
    {
        Task StartJobsAsync();
        Task StopJobAsync();
    }
}
