using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Common
{
    public abstract class JB2Message : JB2.Common.JB2Class, IMessage
    {

        #region Fields

        protected BaseCollection<Tag> _tags;

        #endregion Fields

        public virtual string Body
        {
            get
            {
                return GetProperity<string>("BODY");
            }

            set
            {
                SetProperty<string>("BODY", value);
            }
        }

        public virtual DateTime DateSent
        {
            get
            {
                return GetProperity<DateTime>("DATESENT");
            }

            set
            {
                SetProperty<DateTime>("DATESENT", value);
            }
        }

        public virtual string From
        {
            get
            {
                return GetProperity<string>("FROM");
            }

            set
            {
                SetProperty<string>("FROM", value);
            }
        }

        public abstract bool IsRead { get; }


        public virtual string Subject
        {
            get
            {
                return GetProperity<string>("SUBJECT");
            }

            set
            {
                SetProperty<string>("SUBJECT", value);
            }
        }

        public virtual string To
        {
            get
            {
                return GetProperity<string>("TO");
            }

            set
            {
                SetProperty<string>("TO", value);
            }
        }

        public virtual bool AddTag(Tag tag)
        {
            return _tags.Add(tag);
        }

        public abstract IEnumerable<IMessageAttachment> GetAttachments();


        public abstract IMessageHeader GetHeaderInfo();


        public virtual string GetID()
        {
            return this.GetID();
        }

        public abstract IPerson<string> GetSender();

        public virtual IEnumerable<Tag> GetTags()
        {
            return _tags;
        }

        public abstract void Read();


        public bool RemoveTag(Tag tag)
        {
            return _tags.Remove(tag);
        }

        public abstract void Send();

    }
}
