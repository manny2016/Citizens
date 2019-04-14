using System.Collections.Generic;
using Citizens.Core.Models;
using Citizens.Core.Service;

namespace Citizens.Core.Sync.Models
{
    public class YxcicHtmlSynchronousState : ProcessState<HtmlContext>
    {
        public YxcicHtmlSynchronousState(IProcessSetting<HtmlContext> setting) : base(setting)
        {

        }
        public override string Name
        {
            get
            {
                return "yxhouse";
            }
        }
        public override IProcessingResultService<HtmlContext> GenerateProcessingResultService()
        {
            return new HtmlContentService();
        }
    }
}
