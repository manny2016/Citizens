using Citizens.Core.Models;
using Citizens.Core.Service.Sync;
using System;
using System.Collections.Generic;
using System.Text;

namespace Citizens.Core.Sync.Models
{
    public class YxcicHtmlSynchronousSettings : IProcessSetting<WebArticle>
    {
        public IProcessService<WebArticle> GenerateProcessService()
        {

            return new YxcicSynchronousService();
        }
    }
}
