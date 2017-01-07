using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace JB2.Common
{
    [Serializable()]
    public class BaseCollection<T> : IEnumerable<T>, IObjectCollection<T>
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

        public int Count()
        {
            return _list.Count();
        }

        virtual public ServiceResult Add(T item)
        {
            try
            {
                var l = _list.ToList();
                l.Add(item);
                _list = l;
                return true;
            }
            catch (Exception ex)
            {
                return new ServiceResult(ex);
            }
        }

        virtual public ServiceResult Remove(T item)
        {
            try
            {
                var l = _list.ToList();
                l.Remove(item);
                _list = l;
                return true;
            }
            catch (Exception ex)
            {
                return new ServiceResult(ex);
            }
        }

            

        

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

        public  T this[int index]
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
