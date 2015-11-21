using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Common.Log
{
    public  class LogManager
    {
        private static ILogger _logger;

        public static void Configure( ILogger logger )
        {
             _logger = logger;
        }

        public static ILogger Logger
        {
            get
            {
                return _logger;
            }
        }




    }
}
