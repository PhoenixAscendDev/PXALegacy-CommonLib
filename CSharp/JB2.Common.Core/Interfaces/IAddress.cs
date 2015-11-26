using System;
using System.Collections.Generic;
using System.Text;

namespace JB2.Common
{
    public interface IAddress
    {
        string GetAddressLine1();
        string GetAddressLine2();
        string GetBuilding();
        string GetCity();
        string GetCityState();
        object GetCountryRegion();
        string GetFloorLevel();
        string GetPostalCode();
        object GetStateProvince();
        string GetFullAddress();
        object GetGeoLocation();

        string ToUSMailStandard();
    }   
}
