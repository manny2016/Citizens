

namespace Citizens.Core.Service.Sync
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Citizens.Core.Models;
    using Citizens.Core.Sync.Models;
    using HtmlAgilityPack;

    /// <summary>
    /// 市民卡数据采集
    /// </summary>
    public class YxcicSynchronousService : SynchronousService
    {

        protected override IEnumerable<WebArticle> Genernate(HtmlDocument document, Resource resource)
        {
            var list = document.DocumentNode.SelectNodes(@"//li[@class='point']");
            if (list == null) yield break;

            foreach (var node in list)
            {
                var link = node.SelectSingleNode("./*/a").Attributes["href"].Value.TrimHttplink(resource.Host);
                var title = node.SelectSingleNode("./*/a").InnerText;
                var originalId = link.GetIdfromurl();
                var date = node.SelectSingleNode("./span[@class='t_titile_time']").InnerText.TrytoDateTime();
                yield return Genernate(resource, originalId, title, link, date);
            }
        }
        protected override WebArticle Genernate(Resource resource, string originalId, string title, string originalUrl, DateTime? datetime)
        {
            if (string.IsNullOrEmpty(originalId)) return null;
            if (IsWxPage(originalUrl))
            {
                return new WebArticle(resource.Prefix, originalId, resource.PublishToWhichChannel)
                {
                    OriginalUrl = originalUrl,
                    ArticleSourceName = resource.Name,
                    ArticleTitle = title,
                    ArticleTime = datetime ?? DateTime.Now,
                    OpenUrl = originalUrl,
                    Summary = title,
                    CoverImage = this.DetectConverImage(null,resource.DefaultImages,originalUrl.GetHashCode()),
                    Images = this.DetectImages(null,resource.DefaultImages,originalUrl.GetHashCode())

                };
            }
            else
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(originalUrl.GetUriContent());
                var selector = @"//div[@style='padding:10px;font-size:14px;text-indent:28px;line-height:28px;']";
                var node = doc.DocumentNode.SelectSingleNode(selector);
                var selectors = new string[] { };
                
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
                    CoverImage = this.DetectConverImage(node, resource.DefaultImages, originalUrl.GetHashCode()),
                    Images = this.DetectImages(node, resource.DefaultImages, originalUrl.GetHashCode()) ?? new string[] { },
                    HtmlContent = node.InnerHtml,
                    Summary = node.InnerText.Clearup().TrySubstring(0, 260)

                };
            }
        }
        protected bool IsWxPage(string url)
        {
            return url.StartsWith("https://mp.weixin.qq.com") || url.StartsWith("http://mp.weixin.qq.com");
        }
        protected override IEnumerable<Resource> GetResources()
        {
            yield return new Resource()
            {
                Url = "http://www.yxcic.com/tzgg/index.jhtml",
                Description = "宜兴市民卡通知公告",
                Host = "http://www.yxcic.com/",
                PublishToWhichChannel = "5e4a02c3-f446-11e4-aa81-0010030f0c17",
                ArticleKeyNames = new string[] { "id" },
                Prefix = "yxcic.",
                Name = "市民卡通知公告",
                DefaultImages = new string[] {
                    "../assets/img/tzgg0.jpg",
                    "../assets/img/tzgg1.jpg",
                    "../assets/img/tzgg2.jpg",
                    "../assets/img/tzgg3.jpg",
                    "../assets/img/tzgg4.jpg",
                    "../assets/img/tzgg5.jpg",
                    "../assets/img/tzgg6.jpg",
                    "../assets/img/tzgg7.jpg",
                    "../assets/img/tzgg8.jpg",
                    "../assets/img/tzgg9.jpg"
                }
            };
        }
    }
}
