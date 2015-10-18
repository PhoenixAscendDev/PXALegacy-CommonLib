using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Common
{
    public class JB2Color : JB2.Common.IIDNamePair<string,string>
    {
        private string _id;
        private string _name;
        private System.Drawing.Color _systemColor;
        private int _rbg;

        public JB2Color()
        {
            _id = JB2.Common.ShortGuid.NewGuid();
            _name = "color-" + _id;
        }

        public JB2Color(string id,string name)
        {
            _id = id;
            _name = name;
        }
        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }
        public string Name
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

        public string HexString
        {
            get
            {
                return string.Empty;
            }
        }

        public int ARGB
        {
            get
            {
                return 0;

            }

        }

        public int RGB
        {
            get
            {
                return 0;

            }
        }

    }
}
