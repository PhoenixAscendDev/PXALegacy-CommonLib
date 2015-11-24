using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;

namespace JB2.Common.Scheduler
{
    public abstract class Job : IDNamePair, IJob
    {
        #region Fields
        private DateTime _exTime;
        private bool _inprogress;
        

        #endregion Fields
        public virtual void Start()
        {
            if(IsRepeatable())
            {
                while(true)
                {
                    DoWork();

                    Thread.Sleep(GetCoolDownSeconds());
                }
            }
            else
            {
                DoWork();
            }
        }

        public void Cancel()
        {

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
        public event Action<IJob<string, object>, int> ProgressChanged;
    }
}
