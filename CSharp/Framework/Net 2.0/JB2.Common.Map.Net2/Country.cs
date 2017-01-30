using System;
using System.Collections.Generic;
using System.Text;

namespace JB2.Common
{
    public class Country : IEquatable<Country>, IComparable,IComparable<Country>
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

        #region IEqualable
        public bool Equals(Country other)
        {
            return (this.Abbreviation.Equals(other.Abbreviation) && this.FullName.Equals(other.FullName));
        }

        public override int GetHashCode()
        {
            return this.Abbreviation.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            Country c = obj as Country;
            if (c != null)
            {
                return Equals(c);
            }
            else
            {
                return false;
            }
        }

        #endregion IEqualable

        #region IComparable


        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Country other = obj as Country;
            if (other != null)
               return this.CompareTo(other);
            else
                throw new ArgumentException("Object is not a Country");
        }

        public int CompareTo(Country other)
        {
            if (other == null)
                return 1;
            return this.Abbreviation.CompareTo(other.Abbreviation);
        }

        #endregion IComparable
    }
}
