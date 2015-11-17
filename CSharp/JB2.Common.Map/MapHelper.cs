using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;

namespace JB2.Common
{
    public static class MapHelper
    {
        public const double EarthRadiusInMiles = 3956.0;
        public const double EarthRadiusInKilometers = 6367.0;
        private static USAStates _allUSAStates;

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

        public static USAStates AllUSAStates()
        {
            if (_allUSAStates == null)
                _allUSAStates = new USAStates();

            return _allUSAStates;
        }

        public static StateProvidence GetUSAState(Enum.USAStateType type)
        {
            return _allUSAStates[type];
        }

    }
}
