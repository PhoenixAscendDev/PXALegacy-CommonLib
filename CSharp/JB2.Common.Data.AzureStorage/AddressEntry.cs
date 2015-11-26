using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Common.Data
{
    public class AzureAddressEntry<TStateProvince, TCountry, TGeoLocation> : Microsoft.WindowsAzure.Storage.Table.TableEntity, IAddress
    {
        public AzureAddressEntry(string partitionKey, string rowKey)
        {
            this.PartitionKey = partitionKey;
            this.RowKey = rowKey;
        }
        public AzureAddressEntry()
        {

        }

        public string AddressAddressLine1 { get; set; }
        public string AddressAddressLine2 { get; set; }
        public string AddressBuilding { get; set; }
        public string AddressCity { get; set; }
        public string AddressCountryRegion { get; set; }
        public string AddressFloorLevel { get; set; }
        public string AddressPostalCode { get; set; }
        public string AddressStateProvince { get; set; }
        public string AddressFullAddress { get; }
        public string AddressGeoLocation { get; set; }
        public string AddressCityAndState { get; }

        #region IAddress

        public string GetAddressLine1()
        {
            return AddressAddressLine1;
        }
        public string GetAddressLine2()
        {
            return AddressAddressLine2;
        }
        public string GetBuilding()
        {
            return AddressBuilding;
        }
        public string GetCity()
        {
            return AddressCity;
        }
        public string GetCityState()
        {
            return AddressCityAndState;
        }
        public object GetCountryRegion()
        {
            return (TCountry)Convert.ChangeType(AddressCountryRegion, typeof(TCountry));
        }
        public string GetFloorLevel()
        {
            return AddressFloorLevel;
        }
        public string GetPostalCode()
        {
            return AddressPostalCode;
        }
        public object GetStateProvince()
        {
            return (TStateProvince)Convert.ChangeType(AddressStateProvince, typeof(TStateProvince));
        }
        public string GetFullAddress()
        {
            return AddressFullAddress;
        }
        public object GetGeoLocation()
        {
            return (TGeoLocation)Convert.ChangeType(AddressCountryRegion, typeof(TGeoLocation));
        }

        public virtual string ToUSMailStandard()
        {
            throw new NotImplementedException();
        }

        #endregion IAddress



    }
}
