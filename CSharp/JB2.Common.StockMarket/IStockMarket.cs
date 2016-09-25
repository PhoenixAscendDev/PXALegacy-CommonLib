using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Common
{
    public interface IStockExchange<TKey, TStockValue> : IIDNamePair<TKey, string>
        where TKey : IComparable
    {
        JB2.Common.IBusiness<TKey> Owner { get; set; }
        IStockBusiness<TKey, TStockValue> GetCompany(string stockSymbol);

        IShareHolder<TKey,TStockValue> GetShareHolder(TKey accountID);

    }
}
