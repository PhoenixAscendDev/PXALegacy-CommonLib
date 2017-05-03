using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Common.Scheduler
{
    public interface IScheduler: IScheduler<IJob<string,object>,string,object>
    { }

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

        JB2.Common.ILogger GetLogger();

        void StartJobs();
        void StopJobs();

        bool LoggingEnabled();

        event Action<IScheduler<TJob,TJobKey, TJobParameter>> Started;
        event Action<IScheduler<TJob,TJobKey, TJobParameter>> Stopped;
    }

    public interface ISchedulerAsync : ISchedulerAsync<string, object>
    {

    }

    

    public interface ISchedulerAsync<TJobKey, TJobParameter> : IScheduler<IJobAsync<TJobKey, TJobParameter>,TJobKey,TJobParameter>
        where TJobKey : IComparable
    {
        Task StartJobsAsync();
        Task StartJobAsync();
    }
}
