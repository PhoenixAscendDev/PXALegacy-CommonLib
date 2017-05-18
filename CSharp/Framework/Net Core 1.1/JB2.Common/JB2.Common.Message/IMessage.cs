using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Common.Message
{

    public interface IMessage: IMessage<string,JB2.Common.Tag>
    {

    }


    public interface IMessage<Tkey,TTag> : JB2.Common.IMessage<Tkey,TTag>
        where Tkey : IComparable
    {
        IPerson<Tkey> GetSender();

        bool IsRead { get; }
        IMessageHeader GetHeaderInfo();
        IEnumerable<IMessageAttachment> GetAttachments();
        void Send();
        void Read();
    }
}
