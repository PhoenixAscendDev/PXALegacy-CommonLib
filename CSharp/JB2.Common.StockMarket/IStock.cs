using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Common
{
    public interface IStock<TKey,TValue>
    {
        IStockable<TValue> Company { get; set; }
        IShareHolder<TKey,TValue> ShareHolder { get; set; }
        DateTime DatePurchased { get; set; }
        int Quantity { get; set; }
        TValue PurchaseAmount { get; set; }

        string ExchangeTransactionID { get; set; }
    }
}
