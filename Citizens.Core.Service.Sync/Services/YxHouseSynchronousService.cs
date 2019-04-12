
namespace Citizens.Core.Service.Sync
{
    using Citizens.Core.Models;
    using Citizens.Core.Sync.Models;
    using MariGold.HtmlParser;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Linq;
    public class YxHouseSynchronousService : IProcessService<HtmlContext>
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(YxHouseSynchronousService));
        private readonly YxhouseHtmlSynchronousSettings Settings;
        private readonly string[] urls = new string[] {
            "https://www.yxhouse.com/tbs/morenews.action?typeId=6&&pageIndexName=first&&currentPage=0",//房产新闻
            "https://www.yxhouse.com/tbs/morebuilding.action?typeId=3&&pageIndexName=first&&currentPage=0",//新房资讯

        };
        public YxHouseSynchronousService(YxhouseHtmlSynchronousSettings settings)
        {
            this.Settings = settings;
        }
        public void Dispose()
        {

        }

        public void Process(Action<HtmlContext> pass, CancellationToken token)
        {
            foreach (var url in urls)
            {
                var content = url.GetUriContent();
                var html = new HtmlTextParser(content);
                var node = html.Current.Children.FirstOrDefault(o => o.Attributes["class"].Equals("news_list_content"));

            }
        }
    }
}
