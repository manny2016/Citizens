using System.Collections.Generic;
using Citizens.Core.Models;
using Citizens.Core.Service;

namespace Citizens.Core.Sync.Models
{
    public class YxhouseHtmlSynchronousState : ProcessState<HtmlContext>
    {
        public YxhouseHtmlSynchronousState(IProcessSetting<HtmlContext> setting) : base(setting)
        {

        }
        public override string Name
        {
            get
            {
                return "宜兴房产";
            }
        }
        public override IProcessingResultService<HtmlContext> GenerateProcessingResultService()
        {
            return new HtmlContentService();
        }
    }
}
