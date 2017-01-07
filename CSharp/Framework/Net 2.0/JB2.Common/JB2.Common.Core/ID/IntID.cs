using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public class IntID : IDValue<int>
    {
        #region Fields
        protected int _id;
        #endregion Fields

        public IntID()
        {
            _id = 0;
        }

        public IntID(int id)
        {
            _id = id;
        }

        public override int GetID()
        {
            return _id;
        }
    }
}
