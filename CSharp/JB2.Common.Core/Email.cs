using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using JB2.Common;

namespace JB2.Common
{
    public class Email
    {
        #region Fields

        private bool _isverified;
        private string _address;
        private string _displayName;
        

        #endregion Fields

        #region Constructors

        public Email(string address) : this(address, string.Empty)
        {

        }

        public Email(string address, string displayName)
        {
            _address = address;
            _displayName = displayName;
            _isverified = false;

        }

        #endregion Constructors

        #region Properties

        public string Address
        {
            get
            {
                return _address;
            }
        }

        public string DisplayName
        {
            get
            {
                return _displayName;
            }
        }

        public string User
        {
            get
            {
                return _address;
            }
        }

        public string Host
        {
            get
            {
                return _address;
            }
        }

        public bool IsVerified
        {
            get
            {
                return _isverified;
            }
            set
            {
                _isverified = true;
            }
        }

        public bool IsValid
        {
            get
            {
                return _address.IsEmail();
            }
        }

        #endregion Properties

        #region Operators

        public static implicit operator string (Email e)
        {
            return e.Address;
        }

        public static implicit operator Email(string s)
        {
            return new Email(s);
        }



        #endregion Operators

        #region ToString

        public override string ToString()
        {
            return (string)this;
        }

        #endregion
    }
}
