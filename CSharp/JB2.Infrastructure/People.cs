using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Infrastructure
{
    public static class People
    {
        public static StaffMember JB
        {
            get
            {
                var jb = new StaffMember();
                jb.ID = "jb566";
                jb.Name = new Common.Name()
                {
                    First = "Joshua",
                    Last = "Bennett",
                    Middle = "Allen"
                };
                return jb;
            }
        }
    }
}
