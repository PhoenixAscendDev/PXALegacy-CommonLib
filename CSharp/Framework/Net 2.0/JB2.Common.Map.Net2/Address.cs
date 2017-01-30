using System;
using System.Collections.Generic;
using System.Text;

namespace JB2.Common
{
    public abstract class Address<TState,TCountry> : IAddress<TState, TCountry>,IAddress,IAddressable
    {
        #region Fields
       


        #endregion Fields

      

        #region Properties

        public virtual string AddressLine1
        {
            get;
            set;
        }
        public virtual string AddressLine2
        {
            get;
            set;
        }
        public virtual string City
        {
            get;
            set;
        }

        public virtual string CityAndState
        {
            get
            {
                StringBuilder sBuilder = new StringBuilder();
                if (!string.IsNullOrEmpty(this.City))
                    sBuilder.Append(this.City);
                if (!string.IsNullOrEmpty(this.StateProvince.ToString()))
                {
                    if (!string.IsNullOrEmpty(this.City))
                        sBuilder.Append(", ");
                    sBuilder.Append(this.StateProvince);
                }
                return sBuilder.ToString();
            }
        }

        public virtual string PostalCode
        {
            get;
            set;
        }

        public virtual IGeolocation GeoLocation
        {
            get; set;
        }

        public virtual string Building
        {
            get;set;
           
        }

        public virtual TCountry CountryRegion
        {
            get; set;
        }

        public virtual string FloorLevel
        {
            get; set;
        }

        public virtual TState StateProvince
        {
            get; set;
        }

        public virtual string FullAddress
        {
            get
            {
                return ToUSMailStandard();
            }
        }

        #endregion Properties

        #region Methods

        public abstract string ToUSMailStandard();
        

        #endregion Methods

        #region ToString

        //TODO:  need to define this more
        override public string ToString()
        {
            System.Text.StringBuilder sBuilder = new System.Text.StringBuilder();
            sBuilder.AppendLine(this.AddressLine1);

            if (!string.IsNullOrEmpty(this.AddressLine2)) sBuilder.AppendLine(this.AddressLine2);
            sBuilder.Append(this.City);
            sBuilder.Append(", ");
            sBuilder.Append(this.StateProvince);
            sBuilder.Append(" ");
            sBuilder.Append(this.PostalCode);
            sBuilder.Append(" ");
            sBuilder.Append(this.CountryRegion);

            return sBuilder.ToString();
        }

        #endregion ToString

        #region IAddress

        public string GetAddressLine1()
        {
            return AddressLine1;
        }
        public string GetAddressLine2()
        {
            return AddressLine2;
        }
        public string GetBuilding()
        {
            return Building;
        }
        public string GetCity()
        {
            return City;
        }

        public string GetCityState()
        {
            return CityAndState;
        }
        public object GetCountryRegion()
        {
            return CountryRegion;
        }
        public string GetFloorLevel()
        {
            return FloorLevel;
        }
        public string GetPostalCode()
        {
            return PostalCode;
        }
        public object GetStateProvince()
        {
            return StateProvince;
        }
        public string GetFullAddress()
        {
            return FullAddress;
        }
       

        #endregion IAddress


        #region IAddressable

        public IAddress ToAddress()
        {
            return this;
        }


        #endregion IAddressable

        #region IGeoLocationable
        public IGeolocation ToGeoLocation()
        {
            return GeoLocation;
        }
        #endregion IGeoLocationable
    }
}
