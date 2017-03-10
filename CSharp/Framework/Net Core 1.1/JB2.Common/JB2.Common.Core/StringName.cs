using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public class StringName : NameValue<string>
    {
        #region Fields
        protected string _name;
        #endregion Fields

        public override string GetName()
        {
            return _name;
        }
    }
}
