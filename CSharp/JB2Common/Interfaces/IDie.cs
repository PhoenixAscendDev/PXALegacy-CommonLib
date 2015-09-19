using System;
using System.Collections.Generic;
using System.Text;

namespace JB2.Common
{
    public interface IDie
    {
        Enum.DieType DieType { get; }

        Dieface[] DieFaces { get;  }

        int CurrentFaceNumber { get; set; }
    }
}
