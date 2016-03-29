using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public abstract class ObjectWithMetadata<TKind, TKey, TTag> : IDNamePair<TKey,string>, IObject<TKind, TKey, TTag>,IMetaDatable
        where TKey : IComparable
    {
        #region Fields
        protected BaseCollection<TTag> _tags;
        protected MetaDataCollection _metadata;

        #endregion Fields

        #region IObject
        public virtual bool AddTag(TTag tag)
        {
            return _tags.Add(tag);
        }
        public abstract TKind GetKind();
        public abstract DateTime GetLastUpdate();
        public virtual IEnumerable<TTag> GetTags()
        {
            return _tags;
        }
        public virtual bool RemoveTag(TTag tag)
        {
            return _tags.Remove(tag);
        }



        #endregion IObject

        public virtual IMetaData MetaData(string propertyName)
        {
            return _metadata[propertyName];
        }

        public virtual bool AddMetaData(IMetaData item)
        {
            return _metadata.Add(item);
        }

        public virtual bool ChangeMetaDataValue(string propertyname, object value)
        {
            _metadata[propertyname].UpdateValue(value);
            return true;
        }
        
        public virtual bool RemoveMetaData(IMetaData item)
        {
            return _metadata.Remove(item);
        }

        public virtual IEnumerable<IMetaData> GetMetaData()
        {
            return _metadata;
        }


    }
}
