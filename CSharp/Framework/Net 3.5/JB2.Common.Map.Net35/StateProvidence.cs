using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public class StateProvince
    {
        #region
        protected IIDNamePair<string, string> _lookup;
        #endregion

        #region Constructor
        public StateProvince(string abbreviation,string fullname )
        {
            _lookup = new IDNamePair<string, string>(abbreviation, fullname);
        }

        #endregion Constructor

        #region Properties

        public string Abbreviation
        {
            get
            {
                return _lookup.ID;
            }
            set
            {
                _lookup.ID = value;
            }
        }
        public string Fullname
        {
            get
            {
                return _lookup.Name;
            }
            set
            {
                _lookup.Name = value;
            }
        }

        #endregion Properties

        #region Implicit Operators

        public static implicit operator string(StateProvince s)
        {
            return s.Abbreviation;
        }

        public static implicit operator IDNamePair<string,string>(StateProvince s)
        {
            return (IDNamePair<string,string>)s._lookup;
        }

        public static implicit operator StateProvince(IDNamePair<string,string> i)
        {
            return new StateProvince(i.ID, i.Name);
        }

        #endregion Implicit Operators

        #region ToString

        public override string ToString()
        {
            return _lookup.ID;
        }

        public string ToUpper()
        {
            return ToString().ToUpper();
        }

        #endregion ToString



    }
}
