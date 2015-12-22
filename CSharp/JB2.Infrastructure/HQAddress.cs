using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Common;

namespace JB2.Infrastructure
{
    public class HQAddress : JB2.Common.IAddress
    {
        
        public string GetAddressLine1()
        {
            return "5667 Stone Road";
        }

        public string GetAddressLine2()
        {
            return "Suite 560";
        }

        public string GetBuilding()
        {
            return string.Empty;
        }

        public string GetCity()
        {
            return "Centreville";
        }

        public string GetCityState()
        {
            return "Centreville,VA";
        }

        public object GetCountryRegion()
        {
            return "USA";
        }

        public string GetFloorLevel()
        {
            return string.Empty;
        }

        public string GetFullAddress()
        {
            return ToUSMailStandard();
        }

        public string GetPostalCode()
        {
            return "20120";
        }

        public object GetStateProvince()
        {
            return "VA";
        }

        public IGeolocation ToGeoLocation()
        {
            return new JB2.Common.Geolocation(38.85230,77.45071);
        }

        public string ToUSMailStandard()
        {
            StringBuilder sBuilder = new StringBuilder();
            sBuilder.Append(this.GetAddressLine1().ToUpper());
            if (!string.IsNullOrEmpty(this.GetAddressLine2()))
            {
                sBuilder.Append("\n");
                sBuilder.Append(this.GetAddressLine2().ToUpper());
            }
            sBuilder.Append("\n");
            sBuilder.Append(this.GetCity().ToUpper() + " " + this.GetStateProvince().ToString().ToUpper());
            if (!string.IsNullOrEmpty(this.GetPostalCode()))
                sBuilder.Append(this.GetPostalCode().ToUpper());

            return sBuilder.ToString();
        }
    }
}
