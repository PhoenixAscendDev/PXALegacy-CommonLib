using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Common.Scheduler
{
    public interface IJob : IJob<string>
    {

    }
    public interface IJob<TKey> : IJob<TKey,object>
        where TKey : IComparable
    {

    }

    public interface IJob<TKey,TParameter> : IDisposable, JB2.Common.IIDNamePair<TKey,string>
        where TKey : IComparable
    {
        DateTime ExecutionTime { get; }
        bool InProgress { get; }

        bool IsRepeatable();
        int GetCoolDownSeconds();
        TParameter GetParameters();
        void Start();
        void Cancel();
        ServiceResult DoWork();

        event Action<IJob<TKey, TParameter>> Started;
        event Action<IJob<TKey, TParameter>,TimeSpan> Completed;
        event Action<IJob<TKey, TParameter>, string,int> ProgressChanged;
        event Action<IJob<TKey, TParameter>, ServiceResult> Failed;
    }

    public interface IJobAsync : IJobAsync<string>
    {
       
    }
    public interface IJobAsync<TKey> : IJobAsync<TKey, object>
         where TKey : IComparable
    {

    }

    public interface IJobAsync<TKey, TParameter> : IJob<TKey, TParameter>
        where TKey : IComparable
    {
        Task StartAsync();
        Task CancelAsync();

        Task<ServiceResult> DoWorkAsync();

        //new event Action<IJobAsync<TKey, TParameter>> Started;
        //new event Action<IJobAsync<TKey, TParameter>> Completed;
        //new event Action<IJobAsync<TKey, TParameter>, string, int> ProgressChanged;
    }
}
