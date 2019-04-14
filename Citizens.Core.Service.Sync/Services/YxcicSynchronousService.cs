

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

        protected override IEnumerable<HtmlContext> Genernate(HtmlDocument document, Resource resource)
        {
            var list = document.DocumentNode.SelectNodes(@"//li[@class='point']");
            if (list == null) yield break;

            foreach (var node in list)
            {
                var link = node.SelectSingleNode("./*/a").Attributes["href"].Value.TrimHttplink(resource.Host);
                var title = node.SelectSingleNode("./*/a").InnerText;
                var contextId = link.GetIdfromurl();
                var date = node.SelectSingleNode("./span[@class='t_titile_time']").InnerText.TrytoDateTime();
                yield return Genernate(resource.Host, string.Concat(resource.Prefix, "id"), contextId, title, link);
            }
        }
        private HtmlContext Genernate(string host, string prefix, string contextId, string title, string link)
        {
            var doc = new HtmlDocument();
            var context = new HtmlContext() { Id = contextId, Context = string.Empty, Innerlink = false, OriginalUrl = link };
            doc.LoadHtml(link.GetUriContent());
            if (this.IsWxPage(link))
            {
                context.Innerlink = false;
            }
            else
            {
                context.Innerlink = true;
                try
                {
                    var selector = @"//div[@style='padding:10px;font-size:14px;text-indent:28px;line-height:28px;']";
                    var node = doc.DocumentNode.SelectSingleNode(selector);
                    var selectors = new string[] { };
                    node.Clearup(selectors)
                        .TrimImageUrl(host)
                        .TrimInsideUrl(host)
                        .TrimStyles();
                    context.Context = node.InnerHtml;
                }
                catch (NullReferenceException ex)
                {

                }
            }

            return context;
        }
        protected bool IsWxPage(string url)
        {
            return url.StartsWith("https://mp.weixin.qq.com")|| url.StartsWith("http://mp.weixin.qq.com");
        }
        protected override IEnumerable<Resource> GetResources()
        {
            yield return new Resource()
            {
                Url = "http://www.yxcic.com/tzgg/index.jhtml",
                Description = "宜兴市民卡通知公告",
                Host = "http://www.yxcic.com/",
                Channel = "市民卡通知公告",
                HtmlContextKeyNames = new string[] { "id" },
                Key = "yxcic.tzgg",
                Prefix = "yxcic."
            };


        }
    }
}
