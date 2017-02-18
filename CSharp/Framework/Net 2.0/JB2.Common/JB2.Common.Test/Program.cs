using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common.Test
{

    public class testClass : JB2Class
    {
        public testClass() : base()
        {
        }
        public int SuitValue
        {
            get
            {
                return _props.GetProperty<int>("SuitValue", 0);
            }

            set
            {
                _props.SetProperty<int>("SuitValue", value);

            }
        }
    }
    class Program
    {

        
        static void Main(string[] args)
        {


            var test = new testClass();
            Console.WriteLine(test.SuitValue);
            Console.WriteLine(JB2.Common.NewID.ProductID());
            Console.ReadLine();
        }
    }
}
