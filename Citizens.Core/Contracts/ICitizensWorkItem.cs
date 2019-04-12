using System;
using System.Collections.Generic;
using System.Text;

namespace Citizens.Core
{
    public interface ICitizensWorkItem
    {
        object WorkItemState { get; }

        void Execute();

        void Abort();
    }
}
