using Citizens.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Citizens.Core.Sync.Models
{
    public abstract class SynchronousSettings : IProcessSetting<WebArticle>
    {
        public virtual LeadSource[] LeadSources { get; set; }
        public abstract IProcessService<WebArticle> GenerateProcessService();

    }
}
