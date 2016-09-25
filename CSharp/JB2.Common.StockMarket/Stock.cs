using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Common
{

   
    public abstract class Stock<TKey, TStockValue> : JB2Class, IStock<TKey, TStockValue>
    {
        public virtual IStockable<TStockValue> Company
        {
            get
            {
                return this.GetProperity<IStockable<TStockValue>>("COMPANY");
            }

            set
            {
                this.SetProperty<IStockable<TStockValue>>("COMPANY", value);
            }
        }

        public virtual DateTime DatePurchased
        {
            get
            {
                return this.GetProperity<DateTime>("DATE");
            }

            set
            {
                this.SetProperty<DateTime>("DATE", value);
            }
        }

        public virtual string ExchangeTransactionID
        {
            get
            {
                return this.GetProperity<string>("TRANSID");
            }

            set
            {
                this.SetProperty<string>("TRANSID", value);
            }
        }

        public virtual TStockValue PurchaseAmount
        {
            get
            {
                return this.GetProperity<TStockValue>("AMOUNT");
            }

            set
            {
                this.SetProperty<TStockValue>("AMOUNT", value);
            }
        }

        public virtual int Quantity
        {
            get
            {
                return this.GetProperity<int>("QUANTITY");
            }

            set
            {
                this.SetProperty<int>("QUANTITY", value);
            }
        }

        public virtual IShareHolder<TKey, TStockValue> ShareHolder
        {
            get
            {
                return this.GetProperity<IShareHolder<TKey, TStockValue>>("HOLDER");
            }

            set
            {
                this.SetProperty<IShareHolder<TKey, TStockValue>>("HOLDER", value);
            }
        }
    }
}
