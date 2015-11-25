using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public class BaseSetting : ISetting
    {
        #region Fields
        private string _id;
        private string _name;
        private IPerson<string> _updatedBy;
        private DateTime _updatedByDate;
        private object _value;
        #endregion Fields

        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public IPerson<string> UpdatedByUser
        {
            get { return _updatedBy; }
            set { _updatedBy = value; }
        }

        public DateTime UpdatedDate
        {
            get { return _updatedByDate; }
            set { _updatedByDate = value; }
        }

        public object Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }
}
