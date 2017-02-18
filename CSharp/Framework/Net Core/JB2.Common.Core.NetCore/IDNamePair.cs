using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public class IDNamePair : IDNamePair<string,string>
    {
        public IDNamePair() : base(string.Empty, string.Empty)
        {

        }
        public IDNamePair(string id,string name) : base(id,name)
        {
            if (string.IsNullOrEmpty(id))
                _id = JB2.Common.NewID.ShortGuid();

        }
        
        
    }

    public class IDNamePair<TKey, TName> : IDValue<TKey>, IIDNamePair<TKey, TName>
        where TKey : IComparable
        where TName : IComparable
    {
        #region Fields

        protected TKey _id;
        protected TName _name;

        #endregion Fields

        #region Constructors

        public IDNamePair(TKey id, TName name)
        {
            _id = id;
            _name = name;
        }

        public IDNamePair()
        {
            
        }

        #endregion Constructors


        #region Properties

        public override TKey ID
        {
             get
            {
                return _id;
            }

            set
            {
                _id = value;
            }
        }

        public virtual TName Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }

        public override TKey GetID()
        {
            return _id;
        }

        public virtual TName GetName()
        {
            return _name;
        }



        #endregion Properties
    }
}
