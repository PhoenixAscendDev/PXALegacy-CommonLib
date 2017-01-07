using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public interface ISetting : ISetting<string,IPerson<string>,string>
    {


    }
    public interface ISetting<TKey,TUser,TUserKey> : IIDNamePair<TKey, string>
        where TUser : IPerson<TUserKey>
        where TUserKey : IComparable
        where TKey : IComparable
        
    {
        object Value { get; set; }
        DateTime UpdatedDate { get; set; }
        TUser UpdatedByUser { get; set; }
    }
}
