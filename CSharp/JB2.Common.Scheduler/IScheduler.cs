using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Common.Scheduler
{

    public interface IScheduler: IScheduler<string,object>
    { }
    public interface IScheduler<TJobKey,TJobParameter>
        where TJobKey: IComparable
    {
        IEnumerable<IJob<TJobKey, TJobParameter>> GetJobs();
        void Add(IJob<TJobKey, TJobParameter> job);
        void Remove(IJob<TJobKey, TJobParameter> job);

        JB2.Common.Log.ILogger GetLogger();

        void DoJobs();
        void StopJobs();

        event Action<IScheduler<TJobKey,TJobParameter>> Started;
    }
}
