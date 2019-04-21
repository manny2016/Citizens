using System.Collections.Generic;
using Citizens.Core.Models;
using Citizens.Core.Service;

namespace Citizens.Core.Sync.Models
{
    public class YxcicHtmlSynchronousState : ProcessState<WebArticle>
    {
        public YxcicHtmlSynchronousState(IProcessSetting<WebArticle> setting) : base(setting)
        {

        }
        public override string Name
        {
            get
            {
                return "yxhouse";
            }
        }
        public override IProcessingResultService<WebArticle> GenerateProcessingResultService()
        {
            return new HtmlContentService();
        }
    }
}
