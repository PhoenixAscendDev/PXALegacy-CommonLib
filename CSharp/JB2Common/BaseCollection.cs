using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public class BaseCollection<T> : IEnumerable<T>
    {
        protected IEnumerable<T> _list;

        #region Constructors

        public BaseCollection()
        {
            List<T> list = new List<T>();
            _list = list;
        }

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

        #region Methods

        

        #endregion Methods



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

        T this[int index]
        {
            get
            {
                return _list.ToArray()[index];
            }
            set
            {
                T[] newlist = _list.ToArray();
                newlist[index] = value;
                _list = newlist;
            }
        }


        public T Find(Func<T, bool> predicate)
        {
            return _list.FirstOrDefault(predicate);
        }



    }
}
