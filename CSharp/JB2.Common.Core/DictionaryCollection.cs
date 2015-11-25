using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public class DictionaryCollection<T> : IEnumerable<T>, IObjectCollection<T>
    {
        #region Fields
        protected IDictionary<>

        #endregion Fields

        #region IEnumerable
        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)_list).GetEnumerator();
        }

        #endregion IEnumerable
    }
}
