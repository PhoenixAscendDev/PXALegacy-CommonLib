using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using JB2.Common.Extensions;
namespace JB2.Common
{
    public class VersionNumber : VersionNumber<int, int, int, int>
    {


        #region Fields

        protected DateTime _daterelease;

        #endregion Fields

        #region Constructor
        public VersionNumber(int major, int minor, int build, int revision) : this()
        {
            _major = major;
            _minor = minor;
            _build = build;
            _revision = revision;
        }

        public VersionNumber(int major, int minor) : this(major, minor, 0, 0)
        {

        }

        public VersionNumber(System.Version version) : this()
        {
            _major = version.Major;
            _minor = version.Minor;
            _build = version.Build;
            _revision = version.Revision;
        }

        #endregion Constructor

        public VersionNumber()
        {
            _format = "{0}.{1}.{2}.{3}";
            _isPrerelease = false;
        }

        public static implicit operator int (VersionNumber v)
        {
            int result = (v.Major * 100000000) + (v.Minor * 1000000) + (v.Revision * 10000) + (v.Build);
            return result;
        }

        public static implicit operator string (VersionNumber v)
        {
            return v.ToFormattedString(v._format);
        }

        public static implicit operator DateTime(VersionNumber v)
        {
            return v._daterelease;
        }

        public override int ToInt()
        {
            return (int)this;
        }

        #region VersionNumber

        public override DateTime GetReleaseDate()
        {
            return (DateTime)this;
        }

        public override bool IsPublic()
        {
            return !_isPrerelease;
        }

        public override string GetMajor()
        {
            return _major.ToString();
        }

        public override string GetMinor()
        {
            return _minor.ToString();
        }

        public override string GetBuild()
        {
            return _build.ToString();
        }

        public override string GetRevision()
        {
            return _revision.ToString();
        }

        #endregion VersionNumber
    }
    public abstract class VersionNumber<TMajor, TMinor, TBuild, TRevision> : IComparable, IVersionNumber
        where TMajor : IComparable
        where TMinor : IComparable
        where TBuild : IComparable
        where TRevision : IComparable
    {
        #region Fields
        protected TMajor _major;
        protected TMinor _minor;
        protected TBuild _build;
        protected TRevision _revision;
        protected bool _isPrerelease;
        protected string _format;
        #endregion Fields

        #region Constructors

        #endregion Constructors

        #region Properties
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

        #endregion Properties

        #region ToString
        public override string ToString()
        {
            return ToFormattedString(_format);
        }

        #endregion ToString

        public abstract int ToInt();

        public virtual string ToFormattedString(string format)
        {
            string major = _major.IsNumber() ? Convert.ToInt16(_major).ToString("D2") : _major.ToString();
            string minor = _minor.IsNumber() ? Convert.ToInt16(_minor).ToString("D2") : _minor.ToString();
            string build = _build.IsNumber() ? Convert.ToInt16(_build).ToString("D2") : _build.ToString();
            string revision = _revision.IsNumber() ? Convert.ToInt16(_revision).ToString("D2") : _revision.ToString();

            return string.Format(format, major, minor, build, revision);
        }

        #region IComparable
        public virtual int CompareTo(object obj)
        {
            VersionNumber v = (VersionNumber)obj;
            return v.ToInt().CompareTo(this.ToInt());
        }


        #endregion IComparable

        public string GetVersion()
        {
            return ToFormattedString(_format);
        }

        public abstract DateTime GetReleaseDate();

        public abstract bool IsPublic();

        public abstract string GetMajor();

        public abstract string GetMinor();

        public abstract string GetBuild();

        public abstract string GetRevision();







    }
}
