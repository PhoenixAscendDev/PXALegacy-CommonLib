using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public abstract class IDValue<T> : IIDProp<T>
    {
        public virtual T ID
        {
            get
            {
                return GetID();
            }
            set
            {
                throw  new NotImplementedException();
            }

        }

        public abstract T GetID();
        
    }
}
