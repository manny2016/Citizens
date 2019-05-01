using Citizens.Core.Models;
using Citizens.Core.Service.Sync;
using System;
using System.Collections.Generic;
using System.Text;

namespace Citizens.Core.Sync.Models
{
    public class YxhouseHtmlSynchronousSettings : SynchronousSettings
    {
        public override IProcessService<WebArticle> GenerateProcessService()
        {

            return new YxHouseSynchronousService(this);
        }

        
    }
}
