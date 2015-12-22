using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;
namespace JB2.Infrastructure
{
    public class JB2Project : JB2.Common.IProject<string>
    {

        #region Fields
        protected string _description;
        protected string _uri;

        protected string _latestversion;
        protected string _googlecode;
        

        #endregion Fields

        public JB2Project(string code)
        {
            ID = code;
        }
        public string ID { get; set; }

        public string Name { get; set; }

        public ProductLine GetProductLine()
        {
            string codeprefix = this.ID.Substring(0, 2).ToUpper();
            switch(codeprefix)
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

        public string GetID()
        {
            return this.ID;
        }

        public string GetName()
        {
            return this.Name;
        }

        public IBusiness<string> GetOwner()
        {
            throw new NotImplementedException();
        }

        public IPerson<string> GetPOC()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IReleaseNote<string>> GetReleaseNotes(IVersionNumber versionNumber)
        {
            throw new NotImplementedException();
        }

        public string GetUri()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IVersionNumber> GetVersions()
        {
            throw new NotImplementedException();
        }

        public string GetGoogleAnalyicCode()
        {
            return _googlecode;
        }
    }
}
