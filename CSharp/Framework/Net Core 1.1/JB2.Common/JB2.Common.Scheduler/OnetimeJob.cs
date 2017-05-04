using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Common.Scheduler
{
    public abstract class OnetimeJob : Job
    {
        #region Fields

        #endregion Fields

        #region IJob

        public override bool IsRepeatable()
        {
            return false;
        }


        public override int GetCoolDownSeconds()
        {
            throw new NotImplementedException();
        }

        #endregion IJob
    }
}