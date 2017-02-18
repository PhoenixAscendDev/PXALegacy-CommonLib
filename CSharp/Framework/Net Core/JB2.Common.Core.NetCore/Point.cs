using System;
using System.Collections.Generic;
using System.Text;

using JB2.Common;

namespace JB2.Common
{
    public abstract class Point<T> : IPoint<T>
        where T : IComparable
    {

        #region Fields
        protected T _x;
        protected T _y;
        #endregion Fields
        public virtual T GetX()
        {
            return _x;
        }

        public virtual T GetY()
        {
            return _y;
        }

        public abstract void Offset(T x, T y);    
    }
}
