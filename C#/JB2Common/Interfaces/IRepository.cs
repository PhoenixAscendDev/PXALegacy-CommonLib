using System;
using System.Collections.Generic;
using System.Text;

namespace JB2.Common.Interfaces
{
    public interface IRepository<T,Tkey>
    {
        void Insert(T entity);
        void Delete(T entity);
        T[] SearchFor();
        T[] GetAll();
        T GetById(Tkey id);
        
    }
}
