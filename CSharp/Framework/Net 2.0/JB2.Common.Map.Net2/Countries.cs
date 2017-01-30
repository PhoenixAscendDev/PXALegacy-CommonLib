using System;
using System.Collections.Generic;
using System.Text;

namespace JB2.Common
{
    public class Countries : JB2.Common.BaseCollection<Country>
    {
        #region Fields
        #endregion Fields

        #region Constructors

        public Countries(IEnumerable<Country> list) : base(list)
        {

        }


        public Countries(Country country) : base(country)
        {

        }

        public Countries() : base()
        {

        }

        #endregion Constructors

        #region Methods


        

        #endregion Methods
    }
}
