using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

using JB2.Common;

namespace JB2.Common
{
    public static class MapHelper
    {
        public const double EarthRadiusInMiles = 3956.0;
        public const double EarthRadiusInKilometers = 6367.0;
        private static USAStates _allUSAStates;
        private static Countries _allCountries;

        public static double ToRadian(double val) { return val * (Math.PI / 180); }
        public static double DiffRadian(double val1, double val2) { return ToRadian(val2) - ToRadian(val1); }

        public static double GetDistance(double lat1, double lng1, double lat2, double lng2)
        {
            return GetDistance(lat1, lng1, lat2, lng2, Enum.GeoDistanceType.Miles);
        }

        public static double GetDistance(double lat1, double lng1, double lat2, double lng2, Enum.GeoDistanceType m)
        {
            double radius = EarthRadiusInMiles;

            if (m == Enum.GeoDistanceType.Kilometers) { radius = EarthRadiusInKilometers; }
            return radius * 2 * Math.Asin(Math.Min(1, Math.Sqrt((Math.Pow(Math.Sin((DiffRadian(lat1, lat2)) / 2.0), 2.0) + Math.Cos(ToRadian(lat1)) * Math.Cos(ToRadian(lat2)) * Math.Pow(Math.Sin((DiffRadian(lng1, lng2)) / 2.0), 2.0)))));
        }


        public static Countries AllCountries
        {
            get
            {
                //http://www.c-sharpcorner.com/uploadfile/0c1bb2/display-country-list-without-database-in-asp-net-c-sharp/
                if (_allCountries == null)
                {
                    //Creating list
                    List<Country> CultureList = new List<Country>();

                    //getting  the specific  CultureInfo from CultureInfo class
                    CultureInfo[] getCultureInfo = CultureInfo.GetCultures(CultureTypes.SpecificCultures);

                    foreach (CultureInfo getCulture in getCultureInfo)
                    {
                        //creating the object of RegionInfo class
                        RegionInfo GetRegionInfo = new RegionInfo(getCulture.LCID);
                        //adding each county Name into the arraylist
                        //if (!(CultureList.Contains(GetRegionInfo.EnglishName)))
                        //{
                            Country c = new Country(GetRegionInfo.ThreeLetterISORegionName, GetRegionInfo.EnglishName);
                            if(!CultureList.Contains<Country>(c))
                                CultureList.Add(c);
                        //}
                    }

                    _allCountries = new Countries(CultureList);
                }
                return _allCountries;
            }

        }
        public static USAStates AllUSAStates
        {
            get
            {
                if (_allUSAStates == null)
                    _allUSAStates = new USAStates();

                return _allUSAStates;
            }

        }


        public static Country GetCountryByCountryCode(string code)
        {
            var countries = AllCountries;
            return countries.Find(x => x.Abbreviation == code.ToUpper());
        }

        public static StateProvince GetUSAState(Enum.USAStateType type)
        {
            return _allUSAStates[type];
        }

        public static StateProvince GetStateByAbbreviation(string abbreviation)
        {
            return new StateProvince(abbreviation, "unknown");           
        }

    }
}
