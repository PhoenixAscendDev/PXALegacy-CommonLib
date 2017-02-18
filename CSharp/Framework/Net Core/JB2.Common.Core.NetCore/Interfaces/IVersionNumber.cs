using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public interface IVersionNumber
    {
        string GetVersion();
        DateTime GetReleaseDate();
        bool IsPublic();
        string GetMajor();
        string GetMinor();
        string GetBuild();
        string GetRevision();

        int ToInt();
    }
}
