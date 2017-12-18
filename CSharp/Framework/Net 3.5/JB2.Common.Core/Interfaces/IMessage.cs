using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{

    public interface IMessage : IMessage<string>
    {
    }
    public interface IMessage<Tkey> : IMessage<Tkey,JB2.Common.Tag>
        where Tkey : IComparable
    {

    }

    public interface IMessage<Tkey, TTag> : IIDProp<Tkey>, ITagable<TTag>
        where Tkey : IComparable
    {
        Tkey To { get; set; }
        Tkey From { get; set; }
        string Subject { get; set; }
        string Body { get; set; }
        DateTime DateSent { get; set; }
    }
}
