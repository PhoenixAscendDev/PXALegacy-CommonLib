using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public class BaseCollection<T> : IEnumerable<T>
    {
        private IEnumerable<T> _list;

        #region Constructors

        public BaseCollection(T thing)
        {
            T[] array = new T[1] { thing};
            _list = array;
            
        }

        public BaseCollection(IEnumerable<T> things)
        {
            _list = things;
        }

        #endregion Constructors

        #region Properties

        

        #endregion Properties



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
