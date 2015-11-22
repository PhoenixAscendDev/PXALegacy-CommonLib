using System;
using System.Collections.Generic;
using System.Text;

namespace JB2.Common
{
    public interface IBusiness<TKey> : IIDNamePair<TKey,string>
        where TKey : IComparable
        
    {
        IPerson<TKey> POC { get; set; }
        IAddress MailingAddress { get; set; }
    }
}
