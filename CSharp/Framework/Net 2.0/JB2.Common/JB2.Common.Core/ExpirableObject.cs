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

        public ExpirableObject() : this(20)
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ExpirableItem&lt;T&gt;"/> class.
        /// The value should be specified in the object initializer.
        /// </summary>
        public ExpirableObject(int minutes)
        {
            TimeStamp = DateTime.Now;
            TimeToLive = TimeSpan.FromMinutes(minutes);        
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpirableItem&lt;T&gt;"/> class,
        /// populating it with the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        public ExpirableObject(TValue value)
            : this()
        {
            this.Value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpirableItem&lt;T&gt;"/> class,
        /// populating it with the specified value and time-to-live.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="timeToLive">The time-to-live.</param>
        public ExpirableObject(TValue value, TimeSpan timeToLive)
            : this(value)
        {
            this.TimeToLive = timeToLive;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpirableItem&lt;T&gt;"/> class,
        /// populating it with the specified value and an explicit expiration date/time.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="expires">The expires.</param>
        public ExpirableObject(TValue value, DateTime expires)
            : this(value)
        {
            this.Expires = expires;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpirableItem&lt;T&gt;"/> class,
        /// populating it with the specified value, timestamp, and time-to-live.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="timeStamp">The time stamp.</param>
        /// <param name="timeToLive">The time-to-live.</param>
        public ExpirableObject(TValue value, DateTime timeStamp, TimeSpan timeToLive)
            : this(value, timeToLive)
        {
            this.TimeStamp = timeStamp;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpirableItem&lt;T&gt;"/> class,
        /// populating it with the specified value, timestamp, and explicit expiration date/time.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="timeStamp">The time stamp.</param>
        /// <param name="expires">The expires.</param>
        public ExpirableObject(TValue value, DateTime timeStamp, DateTime expires)
            : this(value)
        {
            this.TimeStamp = timeStamp;
            this.Expires = expires;
        }


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
