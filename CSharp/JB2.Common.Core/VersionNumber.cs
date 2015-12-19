using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    //public struct VersionNumber: VersionNumber<byte,byte,byte,int>
    //{

    //}
    public struct VersionNumber<TMajor,TMinor,TBuild,TRevision>
    {
        #region Fields
        private TMajor _major;
        private TMinor _minor;
        private TBuild _build;
        private TRevision _revision;
        private bool _isPrerelease;
        private string _format;
        #endregion Fields

        #region Constructors
        public VersionNumber(TMajor major, TMinor minor, TBuild build, TRevision revision)
        {
            _major = major;
            _minor = minor;
            _build = build;
            _revision = revision;
            _isPrerelease = false;
            _format = "{0}.{1}.{2}.{3}";
        }
        #endregion Constructors

        public TMajor Major
        {
            get { return _major; }
            set { _major = value; }
        }

        public TMinor Minor
        {
            get { return _minor; }
            set { _minor = value; }
        }

        public TBuild Build
        {
            get { return _build; }
            set { _build = value; }
        }

        public TRevision Revision
        {
            get { return _revision; }
            set { _revision = value; }
        }

        public bool isPreRelease
        {
            get { return _isPrerelease; }
            set { _isPrerelease = value; }
        }

        public override string ToString()
        {
            return string.Format(_format, _major.ToString(), _minor.ToString(), _build.ToString(), _revision.ToString());            
        }






        #endregion Constructors

    }
}
