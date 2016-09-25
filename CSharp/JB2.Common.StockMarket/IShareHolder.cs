using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Common
{
    public interface IShareHolder<TKey, TStockValue> : IStockHolderable<TKey>
    {
        TStockValue GetTotalValue();

        IEnumerable<IStock<TKey, TStockValue>> GetShares(TKey stockSymbol);


    }
}
