using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public interface IProject<TKey> : IIDNamePair<TKey,string>
        where TKey : IComparable
    {
        IBusiness<TKey> GetOwner();
        IPerson<TKey> GetPOC();

        IEnumerable<IVersionNumber> GetReleaseVersions();
    }
}
