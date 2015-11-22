using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public class PlayerGroup : BaseCollection<Player>, JB2.Common.IIDNamePair<string, string>
    {
        #region Fields

        private string _id;
        private string _name;
        private List<IPlayer> _admins;
        #endregion Fields

        #region Properties

        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public IPlayer[] Admins
        {
            get { return _admins.ToArray(); }
        }

        #endregion Properties
    }
}
