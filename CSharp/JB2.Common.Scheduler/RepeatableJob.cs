using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JB2.Common.Scheduler
{
    public abstract class RepeatableJob : Job
    {
        #region Fields
        protected int _counter;
        
        #endregion Fields

        public int CurrentCounter
        {
            get
            {
                return _counter;
            }
        }

        public override ServiceResult DoWork()
        {
            throw new NotImplementedException();
        }

        public override int GetCoolDownSeconds()
        {
            throw new NotImplementedException();
        }

        public abstract int MaxCounter();

        public override bool IsRepeatable()
        {
            return true;
        }

        public override void Start()
        {
            int maxcounter = MaxCounter();
            _cancelled = false;
            if (IsRepeatable())
            {
                while (!_cancelled)
                {
                    DoWorkAndSetFlags();
                    Thread.Sleep(GetCoolDownSeconds());
                    _counter++;
                    if (_counter >= maxcounter)
                    {
                        Cancel();
                    }
                }
            }
            else
            {
                    DoWorkAndSetFlags();
                }
            }

    }
}
