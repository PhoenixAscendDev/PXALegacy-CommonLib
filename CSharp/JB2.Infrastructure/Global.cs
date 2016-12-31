using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading;
using Microsoft.WindowsAzure.Storage.Table;


namespace JB2
{
    public static class Global
    {
        private static ushort _rng;
        private static ushort _lastSavedRNG;
        private static JB2.Common.Data.AzureTableRepository repo = JB2.Infrastructure.Storage.GeneralAccount.GetTable("global");
        private static bool _isRNGInit;
        private static Thread updateRNGThread = null;

        private static void initializeRNG()
        {

            if (!_isRNGInit)
            {
                ushort rng = JB2.Common.RNG.Randy;
                try
                {
                    var p = repo.GetEntity<Microsoft.WindowsAzure.Storage.Table.DynamicTableEntity>("global", "rng");
                    rng = (ushort)p.Properties["LastUsedRNG"].Int32Value.GetValueOrDefault();
                }

                catch (Exception ex)
                {
                    //Do something
                }

                _rng = rng;
                _lastSavedRNG = rng;

                if (updateRNGThread == null)
                {
                    updateRNGThread = new Thread(new ThreadStart(() =>
                    {
                        while (true)
                        {
                            updateRNGStorage();
                            Thread.Sleep(TimeSpan.FromSeconds(5));
                        }
                    }));
                    updateRNGThread.Start();
                }

                if (_rng != 0)
                    _isRNGInit = true;
            }
        }
        private static void updateRNGStorage()
        {

            if (_lastSavedRNG != _rng)
            {
                var entity = new DynamicTableEntity("global", "rng", "*",
                        new Dictionary<string, EntityProperty>{
                    {"LastUsedRNG", new EntityProperty(_rng)},
                    {"LastUpdatedTicks", new EntityProperty(System.DateTime.Now.Ticks)}
                        });

                repo.Insert<DynamicTableEntity>(entity, true);
                _lastSavedRNG = _rng;

            }


        }
        public static int RNG
        {
            get
            {
                initializeRNG();
               
                _rng = JB2.Common.RNG.Plumber(_rng);
               
                return (int)_rng;

            }
        }




    }
}
