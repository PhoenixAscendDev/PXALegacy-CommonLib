using System;
using System.Collections.Generic;
using System.Text;

namespace JB2.Common
{
    public interface IBusiness<TKey> : IIDNamePair<TKey,string>
    {
        IPerson<TKey> POC { get; set; }
        IAddress MailingAddress { get; set; }
    }
}
