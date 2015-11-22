using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public class PlayerProfile : IDNamePair<string, string>, IIDNamePair<string, string>
    {
        #region Fields

        private Name _name;
        private Email _email;
        private string _playerID;
        private string _username;
        private string _profilePic;
        private string _bio;
        private JB2Color _favColor;
        private IAddress<StateProvince, Country, IGeolocation> _address;
        private string _gender;
        private bool _isDefault;
        private JB2Date _birthdate;

        #endregion Fields

        #region Constructor

        public PlayerProfile(string id) : this()
        {
            base.ID = id;
        }

        public PlayerProfile()
        {
            base.ID = JB2.Common.ShortGuid.NewGuid();

        }

        #endregion Constructor

        #region Properties

        public bool IsDefault
        {
            get
            {
                return _isDefault;
            }
            set
            {
                _isDefault = value;
            }
        }

        public Name PlayerName
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public JB2Date Birthdate
        {
            get
            {
                return _birthdate;
            }
            set
            {
                _birthdate = value;
            }
        }

        public Email Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
            }
        }

        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
            }
        }

        public string ShortBio
        {
            get
            {
                return _bio;
            }
            set
            {
                _bio = value;
            }
        }

        public JB2Color FavoriateColor
        {
            get
            {
                return _favColor;
            }
            set
            {
                _favColor = value;
            }
        }

        public string PlayerID
        {
            get
            {
                return _playerID;
            }
            set
            {
                _playerID = value;
            }
        }

        public string ProfilePicUri
        {
            get
            {
                return _profilePic;
            }
            set
            {
                _profilePic = value;
            }
        }

        public IAddress<StateProvince, Country, IGeolocation> Address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
            }
        }

        public string Gender
        {
            get
            {
                return _gender;
            }
            set
            {
                _gender = value;
            }
        }

        #endregion Properties
    }
}
