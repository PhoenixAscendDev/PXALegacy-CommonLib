using System;
using System.Collections.Generic;
using System.Text;

namespace JB2.Common
{
    public class Singleton<TClass>
        where TClass : class, new()
    {
        #region Fields
        internal static TClass _instance = null;
        internal static readonly object padlock = new object();

        #endregion Fields

        #region Constructors

        public Singleton()
        {

        }

        private Singleton(TClass c)
        {
            _instance = c;
        }

        #endregion Constructors


        public static TClass Instance
        {
            get
            {
                lock (padlock)
                {
                    if (_instance == null)
                    {
                        TClass c = new TClass(); 
                    }
                    return _instance;
                }

            }
        }
    }
}
