using System;
using System.Collections.Generic;
using System.Text;

namespace JB2.Common
{

    public interface IAddress : IAddress<string,string,IGeolocation>
    {

    }

    public interface IAddress<TStateProvince,TCountry,TGeoLocation>
    {
        string AddressLine1 { get; set; }
        string AddressLine2 { get; set; }
        string Building { get; set; }
        string City { get; set; }
        TCountry CountryRegion { get; set; }
        string FloorLevel { get; set; }
        string PostalCode { get; set; }
        TStateProvince StateProvince { get; set; }
        string FullAddress { get;  }
        IGeolocation GeoLocation { get; set; }

        string CityAndState { get; }

        string ToUSMailStandard();
    }
}
