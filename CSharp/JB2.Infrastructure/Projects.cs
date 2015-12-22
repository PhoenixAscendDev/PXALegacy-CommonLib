using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Infrastructure
{
    public class Projects
    {
        public static JB2Project  Project(string projectCode)
        {
            return new JB2Project(projectCode);
        }
    }
}
