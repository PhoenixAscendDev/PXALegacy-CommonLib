using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{

    public interface IReleaseNote : IReleaseNote<string>
    {

    }
    public interface IReleaseNote<TKey> : IIDProp<TKey>, IUpdateable
        where TKey : IComparable
    {
        bool IsInternal();
        string GetPlainText();
        string GetHtmlText();
        IPerson<string> GetAuthor();
    }
}
