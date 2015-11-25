using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public class MetaDataCollection : IDNamePair, IEnumerable<IMetaData>, IObjectCollection<IMetaData>
    {
        #region Fields
        private IDictionary<string, IMetaData> _dictionary;
        #endregion Fields

        #region Constructors
        public MetaDataCollection(IMetaData thing, string id = null, string name = null)
            : this(new IMetaData[1] { thing },id,name )
        {
           
           
        }

        public MetaDataCollection(IEnumerable<IMetaData> things, string id = null, string name=null )
            : base(id,name)
        {
            _dictionary = new Dictionary<string, IMetaData>();
            foreach(IMetaData thing in things)
            {
                if (_dictionary.ContainsKey(thing.PropertyName))
                {
                    _dictionary.Add(thing.PropertyName, thing);
                }
                else
                    _dictionary[thing.PropertyName] = thing;
            }     
                 
        }

        

        #endregion Constructors

        public IEnumerable<string> PropertyNames
        {
            get
            {
                return _dictionary.Keys;
            }
        }

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

        public IMetaData this[string propertyName]
        {
            get
            {
                return _dictionary[propertyName];
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
            _dictionary.Remove(item.PropertyName);
            return true;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<IMetaData>)_dictionary).GetEnumerator();
        }
        #endregion IEnumerable


    }
}
