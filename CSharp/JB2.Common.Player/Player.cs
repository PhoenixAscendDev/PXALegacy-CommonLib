using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public abstract class Player : ShortGuidID,IPlayer
    {
        #region Fields
        protected Name _nameinfo;
       
        
        #endregion Fields

        #region Constructors

        

        #endregion Constructors

        #region Properties

        public virtual string DisplayName { get; set; }
        public virtual JB2Date Birthdate { get; set; }
        public virtual string ID
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
        public virtual string MasterUsername { get; set; }
        public virtual Email MasterEmail { get; set; }
        public virtual ProfileCollection Profiles { get; set; }
        public virtual PlayerProfile DefaultProfile { get; set; }
        public virtual int BitScore { get; set; }    
        public virtual string jBeanWalletID { get; set; }

        public  virtual  string FamilyID { get; set; }
        
        public virtual Name Name
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

       

        #endregion Properties


        #region IMetaDatable

        abstract public IMetaData MetaData(string propertyName);
        

        #endregion IMetaDatable


    }
}
