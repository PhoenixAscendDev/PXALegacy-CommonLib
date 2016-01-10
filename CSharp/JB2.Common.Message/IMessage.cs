using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Common
{
    public interface IMessage<Tkey> : IIDProp<Tkey>
    {
        IPerson<Tkey> GetSender();
        Tkey To { get; set; }
        Tkey From { get; set; }
        string Subject { get; set; }
        string Body { get; set; }
        IMessageHeader GetHeaderInfo();
        IEnumerable<IMessageAttachment> GetAttachments();


    }
}
