using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Common
{
    public class Country
    {
        #region Fields
        protected IIDNamePair<string, string> _lookup;
        
        #endregion Fields

        #region Constructor

        public Country(string abbrevition, string fullname) : this()
        {
            _lookup.ID = abbrevition;
            _lookup.Name = fullname;
        }

        public Country()
        {
            _lookup = new IDNamePair<string, string>();
        }
        #endregion Constructor

        #region Property

        public string FullName
        {
            get
            {
               return  _lookup.Name;
            }
            set
            {
                _lookup.Name = value;
            }
        }

        public string Abbreviation
        {
            get
            {
                return _lookup.ID;
            }
            set
            {
                _lookup.Name = value;
            }
        }

        #endregion Property

        #region Implicit Operatiors

        public static implicit operator string(Country c)
        {
            return c.Abbreviation;
        }

        #endregion Implicit Operators

        #region ToString()

        public override string ToString()
        {
            return (string)this;
        }

        public string ToUpper()
        {
            return ToString().ToUpper();
        }
        #endregion ToString()
    }
}
