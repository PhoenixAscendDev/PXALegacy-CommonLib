using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;

using JB2.Common;

namespace JB2.Infrastructure.Data
{
    public class ReleaseVersion : JB2.Common.Data.AzureTableEntity, JB2.Common.IVersionNumber
    {
        #region Fields
        protected VersionNumber _versionNumber;
        #endregion Fields
        public ReleaseVersion(string partitionKey,string rowKey)
        {

        }

        public ReleaseVersion()
        {

        }
        public string VersionNumber
        {
            get
            {
                return _versionNumber.ToString();
            }
            set
            {
                _versionNumber = JB2.Common.VersionNumber.FromString(value,'.');
            }
        }

        public DateTime ReleaseDate { get; set; }

        public string ReleaseStatus { get; set; }
        
        public string GetBuild()
        {
            return _versionNumber.GetBuild();
        }

        public string GetMajor()
        {
            return _versionNumber.GetMajor();
        }

        public string GetMinor()
        {
            return _versionNumber.GetMinor();
        }

        public DateTime GetReleaseDate()
        {
            return this.ReleaseDate;
        }

        public string GetRevision()
        {
            return _versionNumber.GetRevision();
        }

        public string GetVersion()
        {
            return (string)_versionNumber;
        }

        public bool IsPublic()
        {
            switch(this.ReleaseStatus.ToUpper())
            {               
                case "RELEASED":
                    return true;
                default:
                    return false;
            }
        }

        public int ToInt()
        {
            return _versionNumber.ToInt();
        }
    }
}
