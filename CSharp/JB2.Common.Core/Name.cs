using System;
using System.Collections.Generic;
using System.Text;

namespace JB2.Common
{
    public class Name : IComparable, IComparable<Name>
    {
        public string First { get; set; }
        public string Middle { get; set; }
        public string Last { get; set; }
        public string FullName { get; set; }
        public string Salutaion { get; set; }


        public static implicit operator string(Name n)
        {
            return string.Format("First {0}: Middile {1}: Last {2}", n.First, n.Middle, n.Last);
        }

        #region ToString

        public override string ToString()
        {
            return (string)this;
        }

        #endregion ToString

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Name other = obj as Name;
            if (other != null)
                return this.CompareTo(other);
            else
                throw new ArgumentException("Object is not a Name");
        }

        public int CompareTo(Name other)
        {
            if (other == null)
                return 1;
            return this.ToString().CompareTo(other.ToString());
        }
       
    }
}
