using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Infrastructure.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            // var list = JB2.Infrastructure.Counter.GetNext("dewdrop", 10, 1000000);
            //var id = JB2.Infrastructure.Counter.GetNext("fivetwo_entryitem");
            //Console.WriteLine(id);

            //for (int i = 1; i <= 20; i++)
            //{
                
            //}

            var result = JB2.Configuration.GetAppSetting("JB2:gerbigStorageKey");

            Console.WriteLine(result);
            //foreach(long i in list)
            //{
            //    Console.WriteLine( JB2.Common.NewID.Base62(i));
            //}

            Console.ReadLine();
        }
    }
}
