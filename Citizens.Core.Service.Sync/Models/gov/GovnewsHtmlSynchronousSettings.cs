

namespace Citizens.Core.Sync.Models
{
    using Citizens.Core.Models;
    using Citizens.Core.Service.Sync;

    public class GovnewsHtmlSynchronousSettings : SynchronousSettings
    {


        public override IProcessService<WebArticle> GenerateProcessService()
        {
            return new GovnewsSynchronousService(this);
        }
    }
}
