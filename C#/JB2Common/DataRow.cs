using System;
using System.Collections.Generic;
using System.Text;

namespace JB2.Common
{
    public class DataRow : Dictionary<string, object>
    {
        public new object this[string column]
        {
            get
            {
                if (ContainsKey(column))
                {
                    return base[column];
                }

                return null;
            }
            set
            {
                if (ContainsKey(column))
                {
                    base[column] = value;
                }
                else
                {
                    Add(column, value);
                }
            }
        }
    }
}
