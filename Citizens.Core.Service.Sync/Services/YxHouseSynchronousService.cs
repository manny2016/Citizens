
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
        const string Plan_NewsPlan = "yxhouse.newplan.";
        //public void Process(Action<WebArticle> pass, CancellationToken token)
        //{
        //    foreach (var resource in this.GetResources())
        //    {
        //        var doc = new HtmlDocument();
        //        doc.LoadHtml(resource.Url.GetUriContent());
        //        foreach (var article in this.Genernate(doc, resource))
        //        {
        //            if (article != null)
        //            {
        //                pass(article);
        //            }
        //        }
        //    }
        //}

        protected override IEnumerable<WebArticle> Genernate(HtmlDocument document, Resource resource)
        {
            var xpath = resource.Prefix.Equals(Plan_NewsPlan)
                    ? @"//table[@class='xxgk-listz']/*/tr"
                    : @"//div[@class='news_list_content']/ul/li";
            var list = document.DocumentNode.SelectNodes(xpath);
            if (list == null) yield break;

            foreach (var node in list)
            {
                if (resource.Prefix.Equals(Plan_NewsPlan))
                {
                    var columns = node.SelectNodes("./td");
                    if (columns == null || columns.Count.Equals(0)) continue;
                    var title = string.Concat(columns[0].InnerText, columns[1].InnerText).Clearup();
                    var link = columns[1].SelectSingleNode("./a").Attributes["href"].Value.TrimHttplink(resource.Host);
                    var date = columns[2].InnerText.TrytoDateTime();
                    var originalId = link.GetQueryParameters(out string keyName, resource.ArticleKeyNames);
                }
                else
                {
                    var title = node.SelectSingleNode("./span[@class='news_list_list']/a").InnerText.Clearup();
                    var link = node.SelectSingleNode("./span[@class='news_list_list']/a").Attributes["href"].Value.TrimHttplink(resource.Host);
                    var date = node.SelectSingleNode("./span[@class='news_list_time']").InnerText.TrytoDateTime();
                    var originalId = link.GetQueryParameters(out string keyName, resource.ArticleKeyNames);
                    yield return Genernate(resource, originalId, title, link, date);
                }
            }
        }

        protected override IEnumerable<Resource> GetResources()
        {
            yield return new Resource()
            {
                Url = "https://www.yxhouse.com/tbs/morenews.action?typeId=6&&pageIndexName=first&&currentPage=0",
                PublishToWhichChannel = "104C37DE-E3CB-44BA-97B5-4001B0A27098",
                Description = "房产新闻",
                ArticleKeyNames = new string[] { "newsId", "buildingId" },
                Prefix = "yxhouse.news.",
                Host = "https://www.yxhouse.com/",
                Name = "房产新闻"
            };

            yield return new Resource()
            {
                Url = "https://www.yxhouse.com/tbs/morebuilding.action?typeId=3&&pageIndexName=first&&currentPage=0",
                PublishToWhichChannel = "DB7C2848-C7AD-4A76-ABF3-0D91029F7019",
                Description = "新房资讯",
                ArticleKeyNames = new string[] { "newsId", "buildingId" },
                Prefix = "yxhouse.newhouse.",
                Host = "https://www.yxhouse.com/",
                Name = "新房资讯"
            };

            yield return new Resource()
            {
                Url = "http://www.jsmlr.gov.cn/gtapp/nrglIndex.action?classID=2c9082b55bfc47ac015bffac4f23005b",
                PublishToWhichChannel = "736A9D06-D6E4-4308-AC9C-A2C56CB00E33",
                Description = "规划新闻",
                ArticleKeyNames = new string[] { "messageID" },
                Prefix = Plan_NewsPlan,
                Host = "http://www.jsmlr.gov.cn/",
                Name = "规划新闻"

            };
        }

        protected override WebArticle Genernate(Resource resource, string originalId, string title, string originalUrl, DateTime? datetime)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(originalUrl.GetUriContent());
            HtmlNode node = null;
            var selectors = new string[] { };

            if (resource.Prefix.Equals(Plan_NewsPlan))
            {
                var tables = doc.DocumentNode.SelectNodes(@"//table");
                tables.RemoveAt(0);
                tables.RemoveAt(tables.Count - 1);
                node = GenernateContainer();
                foreach (var table in tables) { node.ChildNodes.Add(table); }
                selectors = new string[] { "./*/td[@style='font-size:23px;color:#262626; font-weight" };
            }
            else
            {
                node = doc.DocumentNode.SelectSingleNode("//div[@class='news_show_content']");
                selectors = new string[] { "./div[@class='aboutread']", "./div[@class='aboutus']" };
            }
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

        public HtmlNode GenernateContainer()
        {
            var document = new HtmlDocument();
            document.Load("<div></div>");
            return document.DocumentNode;
        }
    }
}
