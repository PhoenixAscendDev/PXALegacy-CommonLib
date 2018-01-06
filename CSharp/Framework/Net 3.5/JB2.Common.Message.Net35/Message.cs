using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JB2.Common
{
    public abstract class JB2Message : JB2Message<string>
    {

    }
    public abstract class JB2Message<TKey> : JB2Message<TKey,Tag>
                where TKey : IComparable
    {

    }
    
    public abstract class JB2Message<TKey,TTag> : JB2.Common.JB2Class, JB2.Common.Message.IMessage<TKey,TTag>
        where TKey : IComparable
    {

        #region Fields
        
        protected BaseCollection<TTag> _tags;

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

        public virtual TKey From
        {
            get
            {
                return GetProperity<TKey>("FROM");
            }

            set
            {
                SetProperty<TKey>("FROM", value);
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

        public virtual TKey To
        {
            get
            {
                return GetProperity<TKey>("TO");
            }

            set
            {
                SetProperty<TKey>("TO", value);
            }
        }

        public virtual bool AddTag(TTag tag)
        {
            return _tags.Add(tag);
        }

        public virtual JB2.Common.ServiceResult LoadTags(IEnumerable<TTag> tags)
        {

            try
            {
                var w = from tag in tags select _tags.Add(tag);
                return true;
            }
            catch (Exception ex)
            {
                return new ServiceResult(ex);
            }
        }
        public abstract IEnumerable<IMessageAttachment> GetAttachments();


        public abstract JB2.Common.Message.IMessageHeader GetHeaderInfo();


        public virtual TKey GetID()
        {
            return this.GetProperity<TKey>("ID");
        }

        public abstract IPerson<TKey> GetSender();

        public virtual IEnumerable<TTag> GetTags()
        {
            return _tags;
        }

        public abstract void Read();


        public bool RemoveTag(TTag tag)
        {
            return _tags.Remove(tag);
        }

        public abstract void Send();

    }
}
