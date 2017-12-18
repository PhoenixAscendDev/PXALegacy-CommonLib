using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public abstract class NameValue<T> : INameProp<T>
    {
        public virtual T Name
        {
            get
            {
                return GetName();
            }
            set
            {
                throw new NotImplementedException();
            }

        }

        public abstract T GetName();

    }
}
