using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    //http://www.jondavis.net/techblog/post/2010/08/30/Four-Methods-Of-Simple-Caching-In-NET.aspx

    public interface IExpirable<TValue>
    {
        TValue Value { get; set; }
        DateTime TimeStamp { get; set; }
        TimeSpan TimeToLive { get; set; }
        DateTime Expires { get; set; }
        bool HasExpired { get; }
    }
}
