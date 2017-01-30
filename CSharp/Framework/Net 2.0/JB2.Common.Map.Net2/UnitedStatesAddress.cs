using System;
using System.Collections.Generic;
using System.Text;

namespace JB2.Common
{
    public class UnitedStatesAddress : Address<StateProvince, Country>
    {
        public override string ToUSMailStandard()       
        {
            StringBuilder sBuilder = new StringBuilder();
            sBuilder.Append(this.AddressLine1.ToUpper());
            if (!string.IsNullOrEmpty(this.AddressLine2))
            {
                sBuilder.Append("\n");
                sBuilder.Append(this.AddressLine2.ToUpper());
            }
            sBuilder.Append("\n");
            sBuilder.Append(this.City.ToUpper() + " " + this.StateProvince.ToString().ToUpper());
            if (!string.IsNullOrEmpty(this.PostalCode))
                sBuilder.Append(this.PostalCode.ToUpper());

            return sBuilder.ToString();
        }

        public override Country CountryRegion
        { 
            get
            {
                return Helpers.MapHelper.GetCountryByCountryCode("USA");
            }

            set
            {
                base.CountryRegion = value;
            }
        }
    }
}
