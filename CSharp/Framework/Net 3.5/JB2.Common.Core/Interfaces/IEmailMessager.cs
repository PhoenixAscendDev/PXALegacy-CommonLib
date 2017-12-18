using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public interface IEmailMessager : IEmailMessage<string>
    {

    }
    public interface IEmailMessager<TMessageKey> : IEmailMessager<TMessageKey,JB2.Common.Tag>
        where TMessageKey : IComparable
    {

    }

    public interface IEmailMessager<TMessageKey,TMessageTag> : IMessagerAsync<IEmailMessage<TMessageKey,TMessageTag>,TMessageKey,TMessageTag>
        where TMessageKey : IComparable
    {
    }

}
