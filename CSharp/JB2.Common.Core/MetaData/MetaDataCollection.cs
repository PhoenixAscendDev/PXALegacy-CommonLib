using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public class MetaDataCollection : IEnumerable<IMetaData>, IObjectCollection<IMetaData>
    {
        private IDictionary<string, IMetaData> _dictionary;


        #region IObjectCollection
        public IMetaData this[int index]
        {
            get
            {
                return _dictionary.Values.ToArray()[index];
            }

            set
            {
                Add(value);
            }
        }

        public ServiceResult Add(IMetaData item)
        {
            if( !_dictionary.ContainsKey(item.PropertyName))
            {
                _dictionary.Add(item.PropertyName, item);
                return true;
            }
            else
            {
                return false;
            }
        }

        public int Count()
        {
            return _dictionary.Count();
        }

        public IMetaData Find(Func<IMetaData, bool> predicate)
        {
            return _dictionary.Values.ToList().FirstOrDefault(predicate);
        }

        #endregion IObjectCollection

        #region IEnumerable
        public IEnumerator<IMetaData> GetEnumerator()
        {
            return _dictionary.Values.GetEnumerator();

        }

        public ServiceResult Remove(IMetaData item)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<IMetaData>)_dictionary).GetEnumerator();
        }
        #endregion IEnumerable
    }
}
