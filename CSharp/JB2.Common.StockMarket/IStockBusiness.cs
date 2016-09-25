using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Common
{
    public interface IStockBusiness<TKey,TStockValue> : IBusiness<TKey>, IStockable<TStockValue>
        where TKey : IComparable
    {
        int GetShareCount();

        IShareHolder<TKey,TStockValue> GetShareHolder(string accountID);

        TKey ExchangeID { get; set; }
    }
}
