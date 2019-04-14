


namespace Citizens.Core.Service.Sync
{
    using System;
    using System.Threading;
    using Citizens.Core.Models;
    using Citizens.Core.Sync.Models;
    using System.Collections.Generic;
    using System.Linq;
    using HtmlAgilityPack;

    public class GovnewsSynchronousService : SynchronousService// IProcessService<HtmlContext>
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(YxHouseSynchronousService));

        protected override IEnumerable<Resource> GetResources()
        {
            yield return new Resource()
            {
                Key = "gov-news",
                Url = "http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index.shtml",
                Channel = "yxzw",
                Description = "宜兴政务-要闻中心",
                HtmlContextKeyNames = new string[] { },
                Prefix = "govcn.news.",
                Host = "http://www.yixing.gov.cn/"
            };
        }

        protected override IEnumerable<HtmlContext> Genernate(HtmlDocument document, Resource resource)
        {
            var list = document.DocumentNode.SelectNodes(@"//div[@class='zxzx_list']/*/a");
            if (list == null) yield break;

            foreach (var node in list)
            {
                var url = node.Attributes["href"].Value.TrimHttplink(resource.Host);
                var title = node.InnerText;
                var datetime = node.SelectSingleNode(@"./span").InnerText.TrytoDateTime();
                var contextId = url.GetIdfromurl();
                yield return Genernate(resource.Host, string.Concat(resource.Prefix, "id"), contextId, title, url);
            }
        }
        private HtmlContext Genernate(string host, string prefix, string contextId, string title, string link)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(link.GetUriContent());
            var node = doc.DocumentNode.SelectSingleNode(@"//div[@class='show_content']");
            var selectors = new string[] { "./input" };
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
                Innerlink = true
            };
        }

    }
}
