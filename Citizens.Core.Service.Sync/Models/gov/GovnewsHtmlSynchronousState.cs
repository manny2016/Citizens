


namespace Citizens.Core.Sync.Models
{
    using Citizens.Core.Models;
    using Citizens.Core.Service;

    public class GovnewsHtmlSynchronousState : ProcessState<HtmlContext>
    {
        public GovnewsHtmlSynchronousState(IProcessSetting<HtmlContext> setting) : base(setting)
        {

        }
        public override string Name
        {
            get
            {
                return "gov-news";
            }

        }
        public override IProcessingResultService<HtmlContext> GenerateProcessingResultService()
        {
            return new HtmlContentService();
        }
    }
}
