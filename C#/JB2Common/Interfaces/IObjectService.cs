using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public interface IObjectService<T,Tresult,Tsearch>
    {
        List<T> Retrieve();

        List<T> Retrieve(bool isActive);

        List<T> Retrieve(Tsearch request);

        T RetrieveById(string id);

        T RetrieveByName(string name);

        Tresult Remove(T entity);

        Tresult Save(T entity);

    }
}
