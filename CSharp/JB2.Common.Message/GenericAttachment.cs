using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Common
{
    public abstract class GenericAttachment : IDNamePair<string,string>, IFile
    {
        #region Fields
        protected List<string> _tags;
        protected DateTime _updateDate;
        #endregion Fields


        #region IFile
        public abstract bool IsImage
        {
            get;
        }

        public bool AddTag(string tag)
        {
            if (_tags != null)
                _tags = new List<string>(1);

            _tags.Add(tag);

            return true;
        }

        public abstract byte[] GetFileContent();


        public abstract int GetFileSize();
       

        public virtual string GetKind()
        {
            return "file";
        }

        public virtual DateTime GetLastUpdate()
        {
            return _updateDate;
        }

        public IEnumerable<string> GetTags()
        {
            return _tags;
        }

        public bool RemoveTag(string tag)
        {
            return _tags.Remove(tag);
        }
        #endregion IFile
    }
}
