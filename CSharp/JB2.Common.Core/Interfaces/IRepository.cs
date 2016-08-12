using System;
using System.Collections.Generic;
using System.Text;

namespace JB2.Common
{
    public interface IRepository<T,Tkey> : IRepository<T,Tkey,string>
    {

    }
    public interface IRepository<T,Tkey,Tfilter>
    {
        void Insert(T entity);
        void Delete(T entity);
        T[] SearchFor();

        T[] SearchFor(Tfilter filter);
        T[] GetAll();
        T[] GetAll(int? maxRecordCount);
        T GetById(Tkey id);
        
    }
}
