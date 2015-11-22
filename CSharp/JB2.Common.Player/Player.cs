using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public class Player : IPlayer
    {
        #region Fields
        private Name _nameinfo;
        private string _id;
        private MetaDataCollection _metadata;
        #endregion Fields

        #region Constructors

        public Player() : this(JB2.Common.ShortGuid.NewGuid())
        {

        }

        public Player(string id)
        {
            _id = id;
        }

        #endregion Constructors

        #region Properties

        public string DisplayName { get; set; }
        public JB2Date Birthdate { get; set; }
        public string ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }
        public string MasterUsername { get; set; }
        public Email MasterEmail { get; set; }
        public ProfileCollection Profiles { get; set; }
        public PlayerProfile DefaultProfile { get; set; }
        public int BitScore { get; set; }    
        public string jBeanWalletID { get; set; }

        public string FamilyID { get; set; }
        
        public Name NameInfo
        {
            get
            {
                return _nameinfo;
            }

            set
            {
                _nameinfo = value;
            }
        }

        public string Name
        {
            get
            {
                return DisplayName;
            }

            set
            {
                DisplayName = value;
            }
        }

        #endregion Properties


        #region IMetaDatable

        public IMetaData MetaData(string propertyName)
        {
            return _metadata[propertyName];
        }

        #endregion IMetaDatable


    }
}
