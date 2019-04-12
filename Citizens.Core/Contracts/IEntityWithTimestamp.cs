using System;
using System.Collections.Generic;
using System.Text;

namespace Citizens.Core
{
    public interface IEntityWithTimestamp
    {
        long Timestamp { get; }
    }
}
