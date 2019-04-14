

namespace Citizens.Core.Sync.Models
{
    using Citizens.Core.Models;
    using Citizens.Core.Service.Sync;

    public class GovnewsHtmlSynchronousSettings : IProcessSetting<HtmlContext>
    {
        public IProcessService<HtmlContext> GenerateProcessService()
        {
            return new GovnewsSynchronousService();
        }
    }
}
