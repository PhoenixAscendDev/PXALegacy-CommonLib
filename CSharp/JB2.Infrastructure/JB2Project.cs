using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;

using JB2.Common;

using JB2.Infrastructure.Enum;
namespace JB2.Infrastructure
{
    public class JB2Project : JB2.Common.Data.AzureTableEntity, JB2.Common.IProject<string>
    {
        #region Fields
        protected string _description;
        protected string _uri;

        protected string _latestversion;
        protected string _googlecode;

        protected Dictionary<string, IAPIKeySecretPair> _apikeys;
        internal ushort _rng;
        protected ILogger _logger;


        #endregion Fields

        public JB2Project(string partitionKey, string rowKey)
        {

        }
        public JB2Project()
        {

        }

        public JB2Project(string code)
        {
            ID = code;
        }

        public string LatestVersion { get; set; }

        public string TermsofServiceUri { get; set; }

        public string PrivacyPolicyUri { get; set; }

        public string FacebookAppID { get; set; }

        public ILogger Logger
        {
            get
            {
                return _logger;
            }
            set
            {
                _logger = value;
            }
        }

        public JB2.Infrastructure.Enum.ProductLine GetProductLine()
        {
            string codeprefix = this.ID.Substring(0, 2).ToUpper();
            switch (codeprefix)
            {
                case "GA":
                    return ProductLine.Game;
                case "LF":
                    return ProductLine.Life;
                case "SV":
                    return ProductLine.Service;
                default:
                    return ProductLine.None;
            }
        }

        public string GetDescription()
        {
            throw new NotImplementedException();
        }

        public ushort GetRNG()
        {
            ushort newRandy = JB2.Common.RNG.Plumber(_rng);
            _rng = newRandy;

            JB2.Infrastructure.Projects.UpdateProjectRNG(this.GetID(), newRandy);



            return newRandy;
        }



        public IBusiness<string> GetOwner()
        {
            return JB2.Info.HQ;
        }

        public IPerson<string> GetPOC()
        {
            return JB2.Infrastructure.People.JB;
        }

        public IEnumerable<IReleaseNote<string>> GetReleaseNotes(IVersionNumber versionNumber)
        {
            return JB2.Infrastructure.Projects.ReleaseNotes(this.ID, versionNumber);
        }

        public string GetUri()
        {
            throw new NotImplementedException();
        }

        public IVersionNumber GetLatestVersion()
        {
            return (VersionNumber)this.LatestVersion;
        }
        public IEnumerable<IVersionNumber> GetVersions()
        {
            return JB2.Infrastructure.Projects.Versions(this.GetID());
        }

        public string GetGoogleAnalyicCode()
        {
            return _googlecode;
        }

        public IAPIKeySecretPair GetAPIKey(string apiName)
        {
            switch (apiName.ToLower())
            {
                case "facebook":
                    return new ApiKeySecretPair() { APIkey = apiName };

            }
            return _apikeys[apiName];
        }

        public ILogger GetLogger()
        {
            return _logger;
        }

        public static JB2Project Empty
        {
            get
            {
                return new JB2Project("JB-000");
            }
        }
    }
}
