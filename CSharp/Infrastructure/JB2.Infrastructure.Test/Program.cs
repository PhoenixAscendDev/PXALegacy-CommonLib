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

            for (int i = 1; i <= 20; i++)
            {
                var id = JB2.Infrastructure.Counter.GetNext("dewdrop");
                Console.WriteLine(id);
            }
            //foreach(long i in list)
            //{
            //    Console.WriteLine( JB2.Common.NewID.Base62(i));
            //}
                       
            Console.ReadLine();
        }
    }
}
