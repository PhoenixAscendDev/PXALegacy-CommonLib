using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public interface IExpirableDictionary<TKey, TValue> : IDictionary<TKey, TValue>, IDisposable
    {

        event EventHandler<Events.ExpirableRemovedEventArgs<TKey, TValue>> ItemExpired;

        TimeSpan DefaultTimeToLive { get; set; }
        TimeSpan AutoClearExpiredItemsFrequency { get; set; }
        Dictionary<TKey, IExpirable<TValue>> ExpirableItems { get;  }

        void Add(TKey key, TValue value, TimeSpan timeToLive);
        void Add(TKey key, TValue value, DateTime expires);
        void Add(KeyValuePair<TKey, IExpirable<TValue>> item);
        void Add(TKey key, IExpirable<TValue> value);

        TValue this[TKey key, TimeSpan timeToLive] { set; }
        TValue this[TKey key, DateTime expires] { set;}

        void ClearExpiredItems();

        void Update(TKey key, DateTime resetTimestamp);

        TValue GetWithUpdateOrCreate(TKey key);






    }
}
