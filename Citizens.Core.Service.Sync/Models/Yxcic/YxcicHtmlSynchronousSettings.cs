using Citizens.Core.Models;
using Citizens.Core.Service.Sync;
using System;
using System.Collections.Generic;
using System.Text;

namespace Citizens.Core.Sync.Models
{
    public class YxcicHtmlSynchronousSettings : IProcessSetting<HtmlContext>
    {
        public IProcessService<HtmlContext> GenerateProcessService()
        {

            return new YxcicSynchronousService();
        }
    }
}
