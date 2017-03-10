using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public interface IObjectCollection<T>
    {
        T this[int index] { get; set; }

        T Find(Func<T, bool> predicate);

        int Count();

        ServiceResult Add(T item);
        ServiceResult Remove(T item);

        
    }
}
