using System;
using System.Collections.Generic;
using System.Text;

namespace Citizens.Core
{
    public interface IProcessingResultService<ProcessingResult> : IDisposable
    {
        void Save(IEnumerable<ProcessingResult> results);
    }
}
