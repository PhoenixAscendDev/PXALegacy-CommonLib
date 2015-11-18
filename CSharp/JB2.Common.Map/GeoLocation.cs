using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public class Geolocation : IGeolocation
    {
        private double mLatitude { get; set; }
        private double mLongitude { get; set; }

        public Geolocation(double latitude, double longitude)
        {
            mLatitude = latitude;
            mLongitude = longitude;
        }

        #region Public Members
        public double Latitude
        {
            get
            {
                return mLatitude;
            }
        }
        public double Longitude
        {
            get
            {
                return mLongitude;
            }
        }
        #endregion Public Members

        #region Public Methods

        public double GetDistanceTo(IGeolocation othergeo)
        {
            return JB2.Common.MapHelper.GetDistance(this.Latitude, this.Longitude, othergeo.Latitude, othergeo.Longitude, Enum.GeoDistanceType.Miles);

        }
        #endregion Public Methods

    }
}
