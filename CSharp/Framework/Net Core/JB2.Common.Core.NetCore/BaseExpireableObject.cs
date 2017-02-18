using System;
using System.Collections.Generic;
using System.Text;

namespace JB2.Common
{
    public class BaseExpirableObject<TValue> : ExpirableObject<TValue>
    {


        public BaseExpirableObject() : base()
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ExpirableItem&lt;T&gt;"/> class.
        /// The value should be specified in the object initializer.
        /// </summary>
        public BaseExpirableObject(int minutes) : base(minutes)
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpirableItem&lt;T&gt;"/> class,
        /// populating it with the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        public BaseExpirableObject(TValue value) : base(value)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpirableItem&lt;T&gt;"/> class,
        /// populating it with the specified value and time-to-live.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="timeToLive">The time-to-live.</param>
        public BaseExpirableObject(TValue value, TimeSpan timeToLive) : base(value,timeToLive)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpirableItem&lt;T&gt;"/> class,
        /// populating it with the specified value and an explicit expiration date/time.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="expires">The expires.</param>
        public BaseExpirableObject(TValue value, DateTime expires) : base(value,expires)
          { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpirableItem&lt;T&gt;"/> class,
        /// populating it with the specified value, timestamp, and time-to-live.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="timeStamp">The time stamp.</param>
        /// <param name="timeToLive">The time-to-live.</param>
        public BaseExpirableObject(TValue value, DateTime timeStamp, TimeSpan timeToLive) : base(value, timeStamp, timeToLive)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpirableItem&lt;T&gt;"/> class,
        /// populating it with the specified value, timestamp, and explicit expiration date/time.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="timeStamp">The time stamp.</param>
        /// <param name="expires">The expires.</param>
        public BaseExpirableObject(TValue value, DateTime timeStamp, DateTime expires) : base(value, timeStamp, expires)
        { }
    }
}
