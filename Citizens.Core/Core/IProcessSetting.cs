using System;
using System.Collections.Generic;
using System.Text;

namespace Citizens.Core
{
    public interface IProcessSetting<ProcessingResult>
    {
        IProcessService<ProcessingResult> GenerateProcessService();
    }
}
