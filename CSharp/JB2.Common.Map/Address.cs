using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public class Address : IAddress<StateProvince,Country,IGeolocation>
    {
        #region Fields
        private Country _country;

        #endregion Fields

        #region Constructor

        public Address()
        {

        }
        //public Address()
        //{
        //    if (countrycode.ToLower() == "us")
        //       // _country = "United States";
        //        //_country = new Lookup<string,string>( { Key = "US", Name = "United States", Id = "1", ItemType = "Country" };
        //}

        #endregion Constructor

        #region Properties

        public string AddressLine1
        {
            get;
            set;
        }
        public string AddressLine2
        {
            get;
            set;
        }
        public string City
        {
            get;
            set;
        }

        public string CityAndState
        {
            get
            {
                StringBuilder sBuilder = new StringBuilder();
                if (!string.IsNullOrEmpty(this.City))
                    sBuilder.Append(this.City);
                if (!string.IsNullOrEmpty(this.StateProvince))
                {
                    if (!string.IsNullOrEmpty(this.City))
                        sBuilder.Append(", ");
                    sBuilder.Append(this.StateProvince);
                }
                return sBuilder.ToString();
            }
        }
        
        public string PostalCode
        {
            get;
            set;
        }

        public IGeolocation GeoLocation
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string Building
        {
            get;set;
           
        }

        public Country CountryRegion
        {
            get
            {
                return _country;
            }
            set
            {
                _country = value;
            }
        }

        public string FloorLevel
        {
            get; set;
        }

        public StateProvince StateProvince
        {
            get; set;
        }

        public string FullAddress
        {
            get
            {
                return ToUSMailStandard();
            }
        }

        #endregion Properties

        #region Methods

        public virtual string ToUSMailStandard()
        {
            StringBuilder sBuilder = new StringBuilder();
            sBuilder.Append(this.AddressLine1.ToUpper());
            if (!string.IsNullOrEmpty(this.AddressLine2))
            {
                sBuilder.Append("\n");
                sBuilder.Append(this.AddressLine2.ToUpper());
            }
            sBuilder.Append("\n");
            sBuilder.Append(this.City.ToUpper() + " " + this.StateProvince.ToUpper());
            if (!string.IsNullOrEmpty(this.PostalCode))
                sBuilder.Append(this.PostalCode.ToUpper());

            return sBuilder.ToString();
        }

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
    }
}
