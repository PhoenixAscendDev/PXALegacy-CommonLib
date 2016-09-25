using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Common
{
    public abstract class StockExchange<TKey, TStockValue> : JB2Class,IStockExchange<TKey, TStockValue>
        where TKey : IComparable
    {


        public virtual TKey ID
        {
            get
            {
                return this.GetProperity<TKey>("ID");
            }

            set
            {
                this.SetProperty<TKey>("ID", value);
            }
        }

        public virtual string Name
        {
            get
            {
                return this.GetProperity<string>("NAME");
            }

            set
            {
                this.SetProperty<string>("NAME", value);
            }
        }

        public virtual IBusiness<TKey> Owner
        {
            get
            {
                return this.GetProperity<IBusiness<TKey>>("OWNER");
            }

            set
            {
                this.SetProperty<IBusiness<TKey>>("OWNER", value);
            }
        }

        public abstract IStockBusiness<TKey, TStockValue> GetCompany(string stockSymbol);
        

        public virtual TKey GetID()
        {
            return ID;
        }

        public virtual string GetName()
        {
            return Name;
        }

        public abstract IShareHolder<TKey, TStockValue> GetShareHolder(TKey accountID);

        public abstract IStock<TKey, TStockValue> GetStockTran(string transactionID);

    }
}
