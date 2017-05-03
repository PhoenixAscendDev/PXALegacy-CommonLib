using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JB2.Common.Scheduler
{
    public abstract class Job : IDNamePair, IJobAsync
    {
        #region Fields
        protected DateTime _exTime;
        protected bool _inprogress;
        protected bool _cancelled;

        #endregion Fields
        public virtual async void Start()
        {
            await StartAsync();

        }

        public void Cancel()
        {
            _inprogress = false;
            _cancelled = true;
            if (ProgressChanged != null)
                ProgressChanged(this, "Job manually canceled", 1);
        }

        public async Task StartAsync()
        {
            _cancelled = false;
            if (IsRepeatable())
            {
                while (!_cancelled)
                {
                    await DoWorkAndSetFlags();
                    Thread.Sleep(GetCoolDownSeconds());
                }
            }
            else
               await DoWorkAndSetFlags();
        }

        public Task CancelAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult> DoWorkAsync()
        {
            throw new NotImplementedException();
        }


        public virtual Object GetParameters()
        {
            return null;
        }

        public DateTime ExecutionTime
        {
            get
            {
                return _exTime;
            }
        }

        public bool InProgress
        {
            get
            {
                return _inprogress;
            }
        }

        

        public virtual async Task DoWorkAndSetFlags()
        {
            if (Started != null)
                Started(this);

            _inprogress = true;
            _exTime = DateTime.Now;

            await DoWorkAsync();

            _inprogress = false;
            if (Completed != null)
                Completed(this);
        }


        public abstract bool IsRepeatable();
        public abstract ServiceResult DoWork();
        public abstract int GetCoolDownSeconds();

        public virtual void Dispose()
        {
            this._id = null;
            this._name = null;
        }



        public event Action<IJob<string, object>> Started;
        public event Action<IJob<string, object>> Completed;
        public event Action<IJob<string, object>, string,int> ProgressChanged;
    }
}
