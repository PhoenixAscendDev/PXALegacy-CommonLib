using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading;

using System.Collections.Concurrent;

using Microsoft.WindowsAzure.Storage.Table;

namespace JB2.Infrastructure
{
    public static class Counter
    {
        private static JB2.Common.Data.AzureTableRepository repo = JB2.Infrastructure.Storage.GeneralAccount.GetTable("counters");
        private static bool _isInit;
        private static Thread updateThread = null;

        private static ConcurrentDictionary<string, long> _counters;


        public static long GetNext(string counterName, long defaultStart = 1)
        {
            return GetNext(counterName, 1, defaultStart).FirstOrDefault();
        }

        public static IEnumerable<long> GetNext(string counterName, int incrementCount, long defaultStart = 1)
        {
            incrementCounter(counterName, incrementCount);

            long current = Current(counterName, defaultStart);
            List<long> result = new List<long>(incrementCount);
            for (long i = incrementCount-1; i >= 0; i--)
            {
                result.Add(current - i);
            }

            return result;
            //return result;
        }

        public static long Current(string counterName, long defaultStart = 1)
        {
            initialize(counterName, defaultStart);

            return _counters[counterName];
            //var e = repo.GetEntity<DynamicTableEntity>("counter", "name:" + counterName);
            //if (e == null)
            //    return defaultStart;
            //return e.Properties["value"].Int64Value == null ? defaultStart : (long)e.Properties["value"].Int64Value;
        }

        public static long RandomIndex(JB2.Common.ICounterItem citem)
        {
            return RandomIndex(citem.GetCounterName());
        }

        public static long RandomIndex(string counterName)
        {
            var current = repo.GetEntity<DynamicTableEntity>("counter", "name:" + counterName);
            long result = 0;
            if (current != null)
            {
                int max = Convert.ToInt32(current.Properties["value"].Int64Value.GetValueOrDefault());
                int min = Convert.ToInt32(current.Properties["startvalue"].Int64Value.GetValueOrDefault());

                int random = JB2.Common.Utility.RandomNumber(min, max);

                return Convert.ToInt64(random);
            }
            return result;
        }

        //private static void updateCounter(string counterName, long newValue)
        //{
        //    var previous = repo.GetEntity<DynamicTableEntity>("counter", "name:" + counterName);

        //    var entity = new DynamicTableEntity("counter", "name:" + counterName, "*",
        //        new Dictionary<string, EntityProperty>{
        //            {"name", new EntityProperty(counterName)},
        //            {"value", new EntityProperty(newValue)},
        //            {"dateupdate", new EntityProperty(System.DateTime.Now) },
        //            {"startvalue", new EntityProperty(previous == null ? newValue : previous.Properties["startvalue"].Int64Value) }
        //    });

        //    var loge = new DynamicTableEntity("log", "name:" + counterName + "_" + newValue.ToString(), "*",
        //        new Dictionary<string, EntityProperty>{
        //            {"name", new EntityProperty(counterName)},
        //            {"value", new EntityProperty(newValue)},
        //            {"dateupdate", new EntityProperty(System.DateTime.Now)},
        //            {"previousvalue", new EntityProperty(previous == null ? newValue : previous.Properties["value"].Int64Value) }
        //    });


        //    repo.Insert<DynamicTableEntity>(entity, true);
        //    repo.Insert<DynamicTableEntity>(loge, false);
        //}

        private static long incrementCounter(string counterName, int steps)
        {
            initialize(counterName);

            _counters[counterName] = _counters[counterName] + steps;
            return _counters[counterName];      
        }

        private static void initialize(string countername,long defaultstart=1)
        {
            if (!_isInit)
            {
                _counters = new ConcurrentDictionary<string, long>();

                var counters = repo.GetByPartitionKey<DynamicTableEntity>("counter", 1000);

                foreach (DynamicTableEntity e in counters)
                {
                    _counters.GetOrAdd(e["name"].StringValue, (long)e["value"].Int64Value);
                }

                if (updateThread == null)
                {
                    updateThread = new Thread(new ThreadStart(() =>
                    {
                        while (true)
                        {
                            updateTableStorage();
                            Thread.Sleep(TimeSpan.FromSeconds(5));
                        }
                    }));
                    updateThread.Start();
                }
            }
            _isInit = true;

            if( !_counters.ContainsKey(countername))
            {
                _counters.GetOrAdd(countername, defaultstart);
            }
            // if (e == null)
        }

        private static DynamicTableEntity storagevalue(string countername)
        {
            var e = repo.GetEntity<DynamicTableEntity>("counter","name:"+ countername);

            return e;
        }
        private static void updateTableStorage()
        {
            var counters = repo.GetByPartitionKey<DynamicTableEntity>("counter", 1000);

            foreach (var key in _counters.Keys)
            {
                long value = _counters[key];

                var e = storagevalue(key);

                if ((e == null) || (e.Properties["value"].Int64Value.GetValueOrDefault() != value))
                {
                    var entity = new DynamicTableEntity("counter", "name:" + key, "*",
                    new Dictionary<string, EntityProperty>{
                    {"name", new EntityProperty(key)},
                    {"value", new EntityProperty(value)},
                    {"dateupdate", new EntityProperty(System.DateTime.Now) },
                    {"startvalue", new EntityProperty(e == null ? value : e.Properties["startvalue"].Int64Value) }
                    });


                    var loge = new DynamicTableEntity("log", "name:" + key + "_" + value.ToString(), "*",
                        new Dictionary<string, EntityProperty>{
                    {"name", new EntityProperty(key)},
                    {"value", new EntityProperty(value)},
                    {"dateupdate", new EntityProperty(System.DateTime.Now)},
                    {"startvalue", new EntityProperty(e == null ? value : e.Properties["startvalue"].Int64Value) }
                    });

                    repo.Insert<DynamicTableEntity>(entity, true);
                    repo.Insert<DynamicTableEntity>(loge, true);

                }





            }
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
