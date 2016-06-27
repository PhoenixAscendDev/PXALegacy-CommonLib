using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Common;

namespace JB2.Common
{
    public abstract class SaveState<TObjectType,TObjectKey> : JB2.Common.ISaveState<TObjectType, TObjectKey>
    {

        #region Fields
        protected DateTime _saveDate;
        protected MetaDataCollection _metadata;
        protected TObjectKey _objectID;
        protected TObjectType _objectType;
        #endregion Fields

        public SaveState()
        {
            _metadata = new MetaDataCollection(new MetaData<string>("ID",string.Empty));
            _saveDate = System.DateTime.Now;
            
        }

        public SaveState(TObjectType type, TObjectKey objectID): this()
        {
            _metadata["ID"].UpdateValue(objectID);
            _objectID = objectID;
            _objectType = type;
        }

        public virtual DateTime DateSaved
        {
            get
            {
                return _saveDate;
            }

            set
            {
                _saveDate = value;
            }
        }

        public virtual TObjectKey ObjectID
        {
            get
            {
                return _objectID;
            }

            set
            {
                _objectID = value;
            }
        }

        public virtual TObjectType ObjectType
        {
            get
            {
                return _objectType;
            }

            set
            {
                _objectType = value;
            }
        }

        public virtual MetaDataCollection Properties
        {
            get
            {
                return _metadata;
            }

            set
            {
                _metadata = value;
            }
        }

        public abstract string ToJSON();
        
    }
}
