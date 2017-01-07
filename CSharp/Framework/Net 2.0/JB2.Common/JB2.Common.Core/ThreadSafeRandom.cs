using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

using System.Threading;

namespace JB2.Common
{
    public static class ThreadSafeRandom
    {
        [ThreadStatic]
        private static Random Local;

        public static Random ThisThreadsRandom
        {
            get { return Local ?? (Local = new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
        }
    }

    

    
}
