using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public class IDNameCollection<TKey, TName> : BaseCollection<IIDNamePair<TKey, TName>>
        where TKey : IComparable
        where TName : IComparable
    {

        #region Contructors
        public IDNameCollection() : base()
        {

        }

        public IDNameCollection(IIDNamePair<TKey, TName>[] array) : base(array)
        {

        }

        public IDNameCollection(List<IIDNamePair<TKey, TName>> list) : base(list)
        {

        }


        #endregion Contructors

        #region Properties

        public TName[] AllNames
        {
            get
            {
                List<TName> names = new List<TName>();
                foreach (IIDNamePair<TKey, TName> pair in _list)
                {
                    names.Add(pair.Name);
                }

                return names.ToArray();
            }
        }

        public TKey[] AllIDs
        {
            get
            {
                List<TKey> names = new List<TKey>();
                foreach (IIDNamePair<TKey, TName> pair in _list)
                {
                    names.Add(pair.ID);
                }

                return names.ToArray();

            }
        }

        #endregion Properties

        public IIDNamePair<TKey, TName> FindByID(TKey id)
        {
            IIDNamePair<TKey, TName> result = null;

            foreach (IIDNamePair<TKey, TName> pair in this._list)
            {
                if (pair.ID.Equals(id))
                {
                    result = pair;
                    break;
                }

            }

            return result;
        }

        public IIDNamePair<TKey, TName> FindByName(TName name)
        {
            IIDNamePair<TKey, TName> result = null;

            foreach (IIDNamePair<TKey, TName> pair in this._list)
            {
                if (pair.Name.Equals(name))
                {
                    result = pair;
                    break;
                }

            }

            return result;
        }

        public void Add(TKey id, TName name)
        {
            Add(new IDNamePair<TKey, TName>(id, name));

        }

        public void Add(IIDNamePair<TKey, TName> item)
        {
            _list.ToList().Add(item);

        }
    }
}
