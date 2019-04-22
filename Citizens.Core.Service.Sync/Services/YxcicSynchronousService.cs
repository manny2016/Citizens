﻿

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
                    Summary = title
                };
            }
            else
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(originalUrl.GetUriContent());
                var selector = @"//div[@style='padding:10px;font-size:14px;text-indent:28px;line-height:28px;']";
                var node = doc.DocumentNode.SelectSingleNode(selector);
                var selectors = new string[] { };
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
                    Summary = innerText.TrySubstring(0, 300).Trim()

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
                Name = "市民卡通知公告"
            };
        }
    }
}
