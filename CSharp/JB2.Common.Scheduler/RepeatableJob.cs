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
        protected int _counter = 0;
        
        #endregion Fields

        public int CurrentCounter
        {
            get
            {
                return _counter;
            }
        }

        public abstract override ServiceResult DoWork();


        public abstract override int GetCoolDownSeconds();
      
        public abstract int GetMaxCounter();

        public override bool IsRepeatable()
        {
            return true;
        }

        public override void Start()
        {
            int maxcounter = GetMaxCounter() == 0 ? int.MaxValue : GetMaxCounter();
            
            _cancelled = false;


            if (IsRepeatable())
            {
                while (!_cancelled)
                {
                    DoWorkAndSetFlags();
                    Thread.Sleep(GetCoolDownSeconds());
                    

                    if (_counter >= maxcounter)
                    {
                        if (CounterMaxReached != null)
                            CounterMaxReached(this);

                        if (_counter == int.MaxValue)
                            _counter = 0;
                        else
                            Cancel();
                    }
                    _counter++;

                }
            }
            else
            {
                    DoWorkAndSetFlags();
                }
            }



        public event Action<IJob<string, object>> CounterMaxReached;

    }
}
