

namespace Citizens.Core.Sync.Models
{
    using Citizens.Core.Models;
    using Citizens.Core.Service.Sync;

    public class GovnewsHtmlSynchronousSettings : IProcessSetting<WebArticle>
    {
        public IProcessService<WebArticle> GenerateProcessService()
        {
            return new GovnewsSynchronousService();
        }
    }
}
