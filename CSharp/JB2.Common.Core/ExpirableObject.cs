using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public abstract class ExpireableObject: ExpirableObject<object>
    {

    }
    public abstract class ExpirableObject<TValue> : IExpirable<TValue>
    {

        #region Constructors

        #endregion Constructors
        public virtual DateTime Expires
        {
            get
            {
                return TimeStamp + TimeToLive;
            }
            set
            {
                TimeToLive = value - TimeStamp;
            }
        }

        public virtual bool HasExpired
        {
            get
            {
                return DateTime.Now > Expires;
            }
        }

        public virtual DateTime TimeStamp
        {
            get; set;
        }

        public TimeSpan TimeToLive
        {
            get; set;
        }

        public TValue Value
        {
            get;set;
        }
    }
}
