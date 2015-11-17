using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Common
{
    public class StateProvidence 
    {
        #region
        protected IIDNamePair<string, string> _lookup;
        #endregion

        #region Constructor
        public StateProvidence(string abbreviation,string fullname )
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

        public static implicit operator string(StateProvidence s)
        {
            return s.Abbreviation;
        }

        public static implicit operator IDNamePair<string,string>(StateProvidence s)
        {
            return (IDNamePair<string,string>)s._lookup;
        }

        public static implicit operator StateProvidence(IDNamePair<string,string> i)
        {
            return new StateProvidence(i.ID, i.Name);
        }

        #endregion Implicit Operators

        #region ToString

        public override string ToString()
        {
            return _lookup.ID;
        }

        #endregion ToString



    }
}
