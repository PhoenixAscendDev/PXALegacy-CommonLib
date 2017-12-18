using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JB2.Common
{
    public interface ISaveStateable<T,TKey>
    {
        ISaveState<T,TKey> ToSaveState();   
        
           
    }
}
