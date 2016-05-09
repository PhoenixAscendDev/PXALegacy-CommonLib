using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Common.Data
{
    public abstract class AzureRepositoryWithCache<TEntity, TKey, TSearchFilter,TCache,TCacheKey> : AzureRepository, JB2.Common.IRepositoryWithCache<TEntity, TKey, TSearchFilter>
        where TCache : JB2.Common.IExpirableDictionary<TCacheKey,TEntity>, new()
    {

        #region Fields
            protected JB2.Common.IExpirableDictionary<TCacheKey, TEntity> _cache;
        #endregion Fields

        #region Constructors

        public AzureRepositoryWithCache() :  base()
        {
            _cache = new TCache();
        }

        public AzureRepositoryWithCache(int cacheMinutesToLive) : this()
        {
            _cache.DefaultTimeToLive = TimeSpan.FromMinutes(cacheMinutesToLive);
        }

        public AzureRepositoryWithCache(TCache cache): base()
        {
            _cache = cache;
        }

        #endregion Constructors



        public virtual int DefaultMinutesToLive
        {
            get
            {
                return Convert.ToInt32(_cache.DefaultTimeToLive.TotalMinutes);
            }
            set
            {
                _cache.DefaultTimeToLive = TimeSpan.FromMinutes(value);
            }
        }

        public virtual bool ClearCache()
        {
            _cache.Clear();
            return true;
        }

        public abstract void Delete(TEntity entity);
        public abstract TEntity[] GetAll();
        public abstract TEntity[] GetAll(bool useCache = true);
        public abstract TEntity GetById(TKey id);
        public abstract TEntity GetById(TKey id, bool useCache = true);
        public abstract void Insert(TEntity entity);
        public abstract TEntity[] SearchFor();
        public abstract TEntity[] SearchFor(TSearchFilter filter);
        public abstract TEntity[] SearchFor(bool useCache = true);
        public abstract TEntity[] SearchFor(TSearchFilter filter, bool useCache = true);
    }
}
