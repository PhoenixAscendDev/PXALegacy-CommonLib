using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public interface IFile : IObject<int,string,string>
    {
        byte[] GetFileContent();
        int GetFileSize();

        bool IsImage { get; }

    }
}
