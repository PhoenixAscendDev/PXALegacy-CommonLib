using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public interface ITagable<T>
    {
        IEnumerable<T> GetTags();
        bool AddTag(T tag);
        bool RemoveTag(T tag);
    }
}
