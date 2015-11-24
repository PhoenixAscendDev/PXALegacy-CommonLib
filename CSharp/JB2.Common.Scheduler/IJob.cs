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
        event Action<IJob<TKey, TParameter>> Completed;
        event Action<IJob<TKey, TParameter>, int> ProgressChanged;
    }
}
