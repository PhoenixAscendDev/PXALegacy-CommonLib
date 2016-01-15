using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Common
{
    public interface IMessage<Tkey,TTag> : IIDProp<Tkey>, ITagable<TTag>
        where Tkey : IComparable
    {
        IPerson<Tkey> GetSender();
        Tkey To { get; set; }
        Tkey From { get; set; }
        string Subject { get; set; }
        string Body { get; set; }
        DateTime DateSent { get; set; }

        bool IsRead { get; }
        IMessageHeader GetHeaderInfo();
        IEnumerable<IMessageAttachment> GetAttachments();
        void Send();
        void Read();
    }
}
