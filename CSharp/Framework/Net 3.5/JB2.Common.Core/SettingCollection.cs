using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{

    public class SettingCollection<TKey> : SettingCollection<ISetting,IPerson<string>,string,string>
        where TKey : IComparable
    {
        public SettingCollection() : base()
        {
            // List<T> list = new List<T>();
            //_list = list;
        }

        public SettingCollection(ISetting thing) : base(thing)
        {

        }

        public SettingCollection(IEnumerable<ISetting> things) : base(things)
        {
            //_list = things;
        }

    }

    public class SettingCollection<TSetting,TUser,TKey,TUserKey> : BaseCollection<TSetting>
        where TSetting : ISetting<TKey,TUser,TUserKey>
        where TUser : IPerson<TUserKey>
        where TUserKey : IComparable
        where TKey : IComparable
    {

        public SettingCollection() : base()
        {
           // List<T> list = new List<T>();
            //_list = list;
        }

        public SettingCollection(TSetting thing) : base(thing)
        {
           
        }

        public SettingCollection(IEnumerable<TSetting> things) : base(things)
        {
            //_list = things;
        }
         

        public TSetting this[TKey index]
        {
            get
            {
                var dic = _list.ToDictionary(x => x.ID, x => x);
                return dic[index];
            }
            set
            {
                var dic = _list.ToDictionary(x => x.ID, x => x);
                dic[index] = value;

                _list = dic.Values;
            }
        }


        virtual public void ChangeSetting(TKey settingKey,object value,TUser updatedby)
        {
            var dic = _list.ToDictionary(l => l.ID, l => l);
            if (dic.ContainsKey(settingKey))
            {
                dic[settingKey].Value = value;
                _list = dic.Values;
            }
        }
    }
}
