using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateImport
{
    public class JB2TempEntry : Microsoft.WindowsAzure.Storage.Table.TableEntity
    {


        public JB2TempEntry(string partitionKey, string rowKey, JB2.Common.Temperature d)
        {
            this.PartitionKey = partitionKey;
            this.RowKey = rowKey;

            this.Celcius = d.Celcius;
            this.Fahrenheit = d.Fahrenheit;
            this.Kelvin = d.Kelvin;

            var strKelvin = string.Format("{0:0.000}", d.Kelvin);

            var key = "1" + strKelvin.Replace('.', '0').PadLeft(6, '0');

            this.TempKey = Convert.ToInt32(key);

        }



        public double Kelvin { get; set; }
        public double Fahrenheit { get; set; }
        public double Celcius { get; set; }
        public int TempKey { get; set; }
    }

}
