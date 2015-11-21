using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Common.Log
{
    public class LogManager
    {
        private static ILogger _logger;
        private static ILogRepo _logrepo;

        public static void Configure( ILogger logger, ILogRepo repository )
        {
             _logger = logger;
            _logrepo = repository;
        }

        public static ILogger Logger
        {
            get
            {
                return _logger;
            }
        }

        public static ILogRepo Repository
        {
            get
            {
                return _logrepo;
            }
        }






    }
}
