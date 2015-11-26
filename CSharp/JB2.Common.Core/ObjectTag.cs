using System;
using System.Collections.Generic;
using System.Text;

namespace JB2.Common
{
    public class ObjectTag :  IntID, IIDNamePair<int,string>
    {
        public override int ID
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
        public string Name { get; set; }   
        
        public string GetName()
        {
            return Name;
        }     
    }
}
