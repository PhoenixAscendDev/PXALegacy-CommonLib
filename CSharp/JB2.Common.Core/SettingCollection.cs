using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public class SettingCollection<TSetting,TUser,TKey,TUserKey> : BaseCollection<TSetting>
        where TSetting : ISetting<TKey,TUser,TUserKey>
        where TUser : IPerson<TUserKey>
        where TUserKey : IComparable
        where TKey : IComparable
    {


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
