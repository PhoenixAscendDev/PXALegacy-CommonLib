using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    [Serializable()]
    public class MetaDataCollection : IDNamePair, IEnumerable<IMetaData>, IObjectCollection<IMetaData>
    {
        #region Fields
        private IDictionary<string, IMetaData> _dictionary;

        protected DateTime _lastupdate;
        protected bool _defaultchangeLastUpdate;
        #endregion Fields

        #region Constructors

        public MetaDataCollection(bool defaultchangeLastUpdate=false) : base()
        {
            _dictionary = new Dictionary<string, IMetaData>();
        }

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
                if (_dictionary.ContainsKey(propertyName))
                    return _dictionary[propertyName];
                else
                {
                    _dictionary.Add(propertyName, new MetaData<object>(propertyName, null));
                    return _dictionary[propertyName];
                }
            }
        }

        public virtual T GetProperty<T>(string index)
        {

            if (this[index] != null)
            {
                this.Add(new MetaData<T>(index, default(T)));
            }

            return (T)this[index].GetValue().ObjectValue;
        }

        public virtual T GetProperty<T>(string index, T defaultValue)
        {            
            if (this[index] != null)
            {
                this.Add(new MetaData<T>(index, defaultValue));
            }
            return (T)this[index].GetValue().ObjectValue;
        }

        public virtual void SetProperty<T>(string index, T newValue)
        {
            SetProperty<T>(index, newValue, _defaultchangeLastUpdate);
        }

        public virtual void SetProperty<T>(string index, T newValue, bool changeLastUpdate)
        {
            MetaData<T> newMeta = new MetaData<T>(index, newValue);

            //if property already exists and different then  dump and add
            if (this[index] != null)
            {
                this[index].UpdateValue(newValue);
            }
            else
            {
                this.Add(newMeta);
            }

            //update Last Update if needed
            if (changeLastUpdate)
                _lastupdate = System.DateTime.Now;
        }
        public virtual DateTime GetLastUpdate()
        {
            return _lastupdate;
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

        public static MetaDataCollection Empty
        {
            get
            {
                var result = new MetaDataCollection(new List<MetaData<string>>(0));
                return result;
            }
        }

        public static implicit operator Dictionary<string, IMetaData>(MetaDataCollection mdc)
        {
            return (Dictionary<string,IMetaData>)mdc._dictionary;
        }

        public IDictionary<string,IMetaData> ToDictionary()
        {
            return this._dictionary;
        }

    }
}
