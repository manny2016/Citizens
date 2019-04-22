


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
                Url = "http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index.shtml",
                PublishToWhichChannel = "yxzw",
                Description = "宜兴政务-要闻中心",
                ArticleKeyNames = new string[] { },
                Prefix = "govcn.news.",
                Host = "http://www.yixing.gov.cn/",
                Name = "要闻中心"

            };
        }

        protected override IEnumerable<WebArticle> Genernate(HtmlDocument document, Resource resource)
        {
            var list = document.DocumentNode.SelectNodes(@"//div[@class='zxzx_list']/*/a");
            if (list == null) yield break;

            foreach (var node in list)
            {
                var url = node.Attributes["href"].Value.TrimHttplink(resource.Host);
                var title = node.InnerText;
                var datetime = node.SelectSingleNode(@"./span").InnerText.TrytoDateTime();
                var contextId = url.GetIdfromurl();
                yield return Genernate(resource, contextId, title, url, datetime);
            }
        }
        protected override WebArticle Genernate(Resource resource, string originalId, string title, string originalUrl, DateTime? datetime)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(originalUrl.GetUriContent());
            var node = doc.DocumentNode.SelectSingleNode(@"//div[@class='show_content']");
            var selectors = new string[] { "./input" };
            var innerText = node.InnerText;
            node.Clearup(selectors)
                .TrimImageUrl(resource.Host)
                .TrimInsideUrl(resource.Host)
                .TrimStyles();
            return new WebArticle(resource.Prefix, originalId, resource.PublishToWhichChannel)
            {
                OriginalUrl = originalUrl,
                ArticleSourceName = resource.Name,
                ArticleTime = datetime ?? DateTime.Now,
                ArticleTitle = title,
                ArticleVisit = 0,
                ArticleWriter = string.Empty,
                CoverImage = this.DetectConverImage(node) ?? resource.DefaultImage,
                Images = this.DetectImages(node) ?? new string[] { resource.DefaultImage },
                HtmlContent = node.InnerHtml,
                Summary = innerText.TrySubstring(0, 2000)
            };
        }
    }
}
