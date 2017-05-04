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
        protected Scheduler.JobWork _workDelegate;

 

        #endregion Fields
        public virtual async void Start()
        {
            await StartAsync();

        }

        public Scheduler.JobWork  WorkDelegate
        {
            get
            {
                return _workDelegate;
            }
            set
            {
                _workDelegate = value;
            }
        }

        public virtual async void Cancel()
        {
            await CancelAsync();
        }

        public virtual Task CancelAsync()
        {
            _inprogress = false;
            _cancelled = true;
            if (ProgressChanged != null)
                ProgressChanged(this, "Job manually canceled", 1);

            return Task.CompletedTask;
        }

        public virtual async Task StartAsync()
        {
            _cancelled = false;
            if (IsRepeatable())
            {
                while (!_cancelled)
                {
                    await DoWorkAndSetFlags();
                    
                }
            }
            else
               await DoWorkAndSetFlags();
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

            var result = await  DoWorkAsync();
            _inprogress = false;

            if(result)
            {
                if (Completed != null)
                    Completed(this, _exTime - DateTime.Now);
            }
            else
            {
                if (Failed != null)
                    Failed(this, result);
            }

            
            
        }

        public virtual async Task DoWorkAndSetFlags( JobWork work)
        {
            this.WorkDelegate = work;
            await DoWorkAndSetFlags();
        }


        public abstract bool IsRepeatable();
        public virtual ServiceResult DoWork()
        {
            return DoWorkAsync().Result;
        }


        public virtual Task<ServiceResult> DoWorkAsync()
        {
            try
            {
                WorkDelegate?.Invoke();

                return Task.FromResult<ServiceResult>(true);
            }
            catch (Exception ex)
            {
                return Task.FromResult<ServiceResult>(new ServiceResult(ex));
            }
        }

        public virtual Task<ServiceResult> DoWorkAsync(JobWork work)
        {
            this.WorkDelegate = work;

            return DoWorkAsync();
        }


        public abstract int GetCoolDownSeconds();

        public virtual void Dispose()
        {
            this._id = null;
            this._name = null;
        }

        public event Action<IJob<string, object>> Started;
        public event Action<IJob<string, object>,TimeSpan> Completed;
        public event Action<IJob<string, object>, string, int> ProgressChanged;
        public event Action<IJob<string, object>, ServiceResult> Failed;
    }
}
