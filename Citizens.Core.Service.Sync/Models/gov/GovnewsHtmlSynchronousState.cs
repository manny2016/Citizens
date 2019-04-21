


namespace Citizens.Core.Sync.Models
{
    using Citizens.Core.Models;
    using Citizens.Core.Service;

    public class GovnewsHtmlSynchronousState : ProcessState<WebArticle>
    {
        public GovnewsHtmlSynchronousState(IProcessSetting<WebArticle> setting) : base(setting)
        {

        }
        public override string Name
        {
            get
            {
                return "gov-news";
            }

        }
        public override IProcessingResultService<WebArticle> GenerateProcessingResultService()
        {
            return new HtmlContentService();
        }
    }
}
