using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public interface IEmailMessage : IMessage
    {

    }
    public interface IEmailMessage<Tkey> : IMessage<Tkey>
        where Tkey : IComparable
    {

    }
    public interface IEmailMessage<Tkey, TTag> : IMessage<Tkey,TTag>
        where Tkey : IComparable
    {

    }
}
