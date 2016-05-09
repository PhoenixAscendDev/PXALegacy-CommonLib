using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common.Interfaces
{
    public interface IRepositoryWithCache<TEntity, TKey, TSearchFilter> : IRepository<TEntity, TKey, TSearchFilter>
    {
        int DefaultMinutesToLive { get; set; }

        bool ClearCache();
        TEntity[] SearchFor(bool useCache = true);

        TEntity[] SearchFor(TSearchFilter filter, bool useCache = true);
        TEntity[] GetAll(bool useCache = true);
        TEntity GetById(TKey id, bool useCache = true);


    }
}
