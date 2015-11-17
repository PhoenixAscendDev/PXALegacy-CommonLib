using System;
using System.Collections.Generic;
using System.Text;

namespace JB2.Common
{
    public interface IAddress
    {
        string AddressLine1 { get; set; }
        string AddressLine2 { get; set; }
        string Building { get; set; }
        string City { get; set; }
        string CountryRegion { get; set; }
        string FloorLevel { get; set; }
        string PostalCode { get; set; }
        string StateProvince { get; set; }
        string FullAddress { get; set; }
        IGeolocation GeoLocation { get; set; }

        string CityAndState { get; }

        string ToUSMailStandard();
    }
}
