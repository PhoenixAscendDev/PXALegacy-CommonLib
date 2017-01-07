using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public class StringID : IDValue<string>
    {
        #region Fields
        protected string _id;
        #endregion Fields

        #region Constructors

        public StringID() : this(Enum.NewIDType.Guid)
        {
            
        }
        public StringID(Enum.NewIDType newidType)
        {
            switch(newidType)
            {
                case Enum.NewIDType.Guid:
                    _id = NewID.Guid();
                    break;
                case Enum.NewIDType.Base62:
                    _id = NewID.Base62();
                    break;
                case Enum.NewIDType.ShortGuid:
                    _id = NewID.ShortGuid();
                    break;
                case Enum.NewIDType.TickHask:
                    _id = NewID.TickHash();
                    break;
                case Enum.NewIDType.TimeHash:
                    _id = NewID.TimeHash();
                    break;
                default:
                    _id = NewID.Guid();
                    break;          
            }
        }

        public StringID(string id)
        {
            _id = id;
        }

        #endregion Constructors

        public override string GetID()
        {
            return _id;
        }
    }
}
