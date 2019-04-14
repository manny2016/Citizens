
namespace Citizens.Core.Service.Sync
{
    using Citizens.Core.Models;
    using Citizens.Core.Sync.Models;

    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Linq;
    using HtmlAgilityPack;

    public class YxHouseSynchronousService : SynchronousService
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(YxHouseSynchronousService));
        private readonly YxhouseHtmlSynchronousSettings Settings;

        public YxHouseSynchronousService(YxhouseHtmlSynchronousSettings settings)
        {
            this.Settings = settings;
        }
        public void Dispose()
        {

        }

        public void Process(Action<HtmlContext> pass, CancellationToken token)
        {
            foreach (var resource in this.GetResources())
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(resource.Url.GetUriContent());
                var list = doc.DocumentNode.SelectNodes(@"//div[@class='news_list_content']/ul/li");

            }
        }

        protected override IEnumerable<HtmlContext> Genernate(HtmlDocument document, Resource resource)
        {
            var list = document.DocumentNode.SelectNodes(@"//div[@class='news_list_content']/ul/li");
            if (list == null) yield break;

            foreach (var node in list)
            {

                var title = node.SelectSingleNode("./span[@class='news_list_list']/a").InnerText.Clearup();
                var link = node.SelectSingleNode("./span[@class='news_list_list']/a").Attributes["href"].Value.TrimHttplink(resource.Host);
                var date = node.SelectSingleNode("./span[@class='news_list_time']").InnerText.TrytoDateTime();
                var contextId = link.GetQueryParameters(out string keyName, resource.HtmlContextKeyNames);
                yield return Genernate(resource.Host, string.Concat(resource.Prefix, keyName), contextId, title, link);
            }
        }

        protected override IEnumerable<Resource> GetResources()
        {
            yield return new Resource()
            {
                Key = "yxhouse-news",
                Url = "https://www.yxhouse.com/tbs/morenews.action?typeId=6&&pageIndexName=first&&currentPage=0",
                Channel = "宜兴房产",
                Description = "房产新闻",
                HtmlContextKeyNames = new string[] { "newsId", "buildingId" },
                Prefix = "yxhouse.",
                Host = "https://www.yxhouse.com/"
            };

            yield return new Resource()
            {
                Key = "yxhouse-newhouse-info",
                Url = "https://www.yxhouse.com/tbs/morebuilding.action?typeId=3&&pageIndexName=first&&currentPage=0",
                Channel = "宜兴房产",
                Description = "新房资讯",
                HtmlContextKeyNames = new string[] { "newsId", "buildingId" },
                Prefix = "yxhouse.",
                Host = "https://www.yxhouse.com/"
            };
        }

        private HtmlContext Genernate(string host, string prefix, string contextId, string title, string link)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(link.GetUriContent());
            var node = doc.DocumentNode.SelectSingleNode("//div[@class='news_show_content']");
            var selectors = new string[] { "./div[@class='aboutread']", "./div[@class='aboutus']" };
            node.Clearup(selectors)
                .TrimImageUrl(host)
                .TrimInsideUrl(host)
                .TrimStyles();

            return new HtmlContext()
            {
                Context = node.InnerHtml,
                Id = contextId,
                OriginalUrl = link,
                Prefix = prefix,
                Innerlink = true,
            };
        }

    }
}
