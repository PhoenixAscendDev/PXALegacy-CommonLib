using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public interface IMetaDataValue
    {
        bool BooleanValue { get; }
        DateTime DateTimeValue { get; }
        short ShortValue { get; }
        int IntValue { get; }
        long LongValue { get; }
        double DoubleValue { get; }
        string StringValue { get; }
        object ObjectValue { get; }
        Guid GuidValue { get; }
    }
}
