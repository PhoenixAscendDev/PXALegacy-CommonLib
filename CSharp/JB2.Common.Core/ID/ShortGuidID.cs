using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public class ShortGuidID : IDValue<string>
    {
        #region Fields
        private string _id;
        #endregion Fields

        #region Constructors

        public ShortGuidID() 
        {
            _id = JB2.Common.NewID.ShortGuid();
        }

        public ShortGuidID(string id)
        {
            _id = id;
        }

        #endregion Constructors

        public override string GetID()
        {
            return _id;
        }
    }
}
