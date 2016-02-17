using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.WindowsAzure.Storage.Table;

namespace JB2.Infrastructure
{
    public static class Counter
    {
        private static JB2.Common.Data.AzureTableRepository repo = JB2.Infrastructure.Storage.GeneralAccount.GetTable("counters");


        public static long GetNext(string counterName,long defaultStart=1)
        {
            return GetNext(counterName, 1, defaultStart).FirstOrDefault();
        }

        public static IEnumerable<long> GetNext(string counterName, int incrementCount,long defaultStart=1)
        {
            long current = Current(counterName, defaultStart);
            List<long> result = new List<long>(incrementCount);
            for(long i=1; i <= incrementCount; i++)
            {
                result.Add(current + i);
            }
            updateCounter(counterName, current + incrementCount);
            return result;
        }

        public static long Current(string counterName, long defaultStart=1)
        {
            var e = repo.GetEntity<DynamicTableEntity>("counter", "name:" + counterName);
            if (e == null)
                return defaultStart;
            return e.Properties["value"].Int64Value == null ? defaultStart : (long)e.Properties["value"].Int64Value;
        }

        private static void updateCounter(string counterName, long newValue)
        {
            var entity = new DynamicTableEntity("counter", "name:" + counterName, "*",
                new Dictionary<string, EntityProperty>{
                    {"name", new EntityProperty(counterName)},
                    {"value", new EntityProperty(newValue)},
                    {"dateupdate", new EntityProperty(System.DateTime.Now)}
            });

            var loge = new DynamicTableEntity("log", "name:" + counterName + "_" + newValue.ToString(), "*",
                new Dictionary<string, EntityProperty>{
                    {"name", new EntityProperty(counterName)},
                    {"value", new EntityProperty(newValue)},
                    {"dateupdate", new EntityProperty(System.DateTime.Now)}
            });


            repo.Insert<DynamicTableEntity>(entity, true);
            repo.Insert<DynamicTableEntity>(loge, false);
        }

        //private static DynamicTableEntity counterEntity
        //{
        //    get
        //    {
        //        var e = new DynamicTableEntity();
        //        e.Properties.Add( )
        //        var e = new DynamicTableEntity(partitionKey, rowKey, "*",
        //        new Dictionary<string, EntityProperty>{
        //        {"Prop1", new EntityProperty("stringVal")},
        //        {"Prop2", new EntityProperty(DateTimeOffset.UtcNow)},
        //        });
        //    }
        //}
    }
}
