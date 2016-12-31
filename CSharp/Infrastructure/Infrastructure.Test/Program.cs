using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(JB2.Info.Project);


            var rng = JB2.Global.RNG;

            Console.WriteLine(rng.ToString());
            Console.ReadLine();
        }
    }
}
