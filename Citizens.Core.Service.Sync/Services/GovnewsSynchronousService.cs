


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
                PublishToWhichChannel = "4ec12bb8-f454-11e4-aa81-0010030f0c17",
                Description = "宜兴政务-要闻中心",
                ArticleKeyNames = new string[] { },
                Prefix = "govcn.news.",
                Host = "http://www.yixing.gov.cn/",
                Name = "要闻中心",
                DefaultImages = new string[] {
                    "../assets/img/zwxx0.jpg",
                    "../assets/img/zwxx1.jpg",
                    "../assets/img/zwxx2.jpg",
                    "../assets/img/zwxx3.jpg",
                    "../assets/img/zwxx4.jpg",
                    "../assets/img/zwxx5.jpg",
                    "../assets/img/zwxx6.jpg",
                    "../assets/img/zwxx7.jpg",
                    "../assets/img/zwxx8.jpg",
                    "../assets/img/zwxx9.jpg",
                    "../assets/img/zwxx10.jpg",
                    "../assets/img/zwxx11.jpg",
                    "../assets/img/zwxx12.jpg",
                    "../assets/img/zwxx13.jpg",
                    "../assets/img/zwxx14.jpg",
                    "../assets/img/zwxx15.jpg",
                    "../assets/img/zwxx16.jpg",
                    "../assets/img/zwxx17.jpg",
                    "../assets/img/zwxx18.jpg",
                    "../assets/img/zwxx19.jpg",
                    "../assets/img/zwxx20.jpg",
                    "../assets/img/zwxx21.jpg",
                }

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
            if (string.IsNullOrEmpty(originalId)) return null;
            var doc = new HtmlDocument();
            doc.LoadHtml(originalUrl.GetUriContent());
            var node = doc.DocumentNode.SelectSingleNode(@"//div[@class='show_content']");
            var selectors = new string[] { "./input" };           

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
                CoverImage = this.DetectConverImage(node, resource.DefaultImages, originalUrl.GetHashCode()) ?? null,
                Images = this.DetectImages(node, resource.DefaultImages, originalUrl.GetHashCode()) ?? new string[] { },
                HtmlContent = node.InnerHtml,
                Summary = node.InnerText.Clearup().TrySubstring(0, 260)
            };
        }
    }
}
