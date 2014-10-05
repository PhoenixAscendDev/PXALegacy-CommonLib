using System;
using System.Collections.Generic;
using System.Text;

namespace JB2.Common.Database
{
    public interface IDatabaseHandler
    {

        void ExecuteNonQuery(string query);
        DataTable ExecuteQuery(string query);
        void ExecuteScript(string script);

    }
}
