﻿
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
    using Citizens.Core;

    public class YxHouseSynchronousService : SynchronousService
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(YxHouseSynchronousService));

        private readonly SmoothRequestingManagement smooth = new SmoothRequestingManagement(1, 1);
        public YxHouseSynchronousService(SynchronousSettings settings) : base(settings)
        {

        }

        const string Plan_NewsPlan = "yxhouse.newplan.";


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
                    var title = columns[1].SelectSingleNode("./a").Attributes["title"].Value.Clearup(); //string.Concat(columns[0].InnerText, columns[1].InnerText).Clearup();
                    var link = columns[1].SelectSingleNode("./a").Attributes["href"].Value.TrimHttplink(resource.Host);
                    var date = columns[2].InnerText.TrytoDateTime();
                    var originalId = link.GetQueryParameters(out string keyName, resource.ArticleKeyNames);
                    yield return Genernate(resource, originalId, title, link, date);
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
                Name = "房产新闻",
                DefaultImages = new string[] {
                    "../assets/img/fcxw0.png",
                    "../assets/img/fcxw1.png",
                    "../assets/img/fcxw2.png",
                    "../assets/img/fcxw3.png",
                    "../assets/img/fcxw4.png"
                }

            };

            yield return new Resource()
            {
                Url = "https://www.yxhouse.com/tbs/morebuilding.action?typeId=3&&pageIndexName=first&&currentPage=0",
                PublishToWhichChannel = "DB7C2848-C7AD-4A76-ABF3-0D91029F7019",
                Description = "新房资讯",
                ArticleKeyNames = new string[] { "newsId", "buildingId" },
                Prefix = "yxhouse.newhouse.",
                Host = "https://www.yxhouse.com/",
                Name = "新房资讯",
                DefaultImages = new string[] {
                    "../assets/img/fcxw5.png",
                    "../assets/img/fcxw6.png",
                    "../assets/img/fcxw7.png",
                    "../assets/img/fcxw8.png",
                    "../assets/img/fcxw9.png",
                }
            };

            yield return new Resource()
            {
                Url = "http://www.jsmlr.gov.cn/gtapp/nrglIndex.action?classID=2c9082b55bfc47ac015bffac4f23005b",
                PublishToWhichChannel = "736A9D06-D6E4-4308-AC9C-A2C56CB00E33",
                Description = "规划新闻",
                ArticleKeyNames = new string[] { "messageID" },
                Prefix = Plan_NewsPlan,
                Host = "http://www.jsmlr.gov.cn/",
                Name = "规划新闻",
                DefaultImages = new string[] {
                    "../assets/img/ghxw0.png",
                    "../assets/img/ghxw1.png",
                    "../assets/img/ghxw2.png",
                    "../assets/img/ghxw3.png",
                    "../assets/img/ghxw4.png",
                    "../assets/img/ghxw5.png",
                    "../assets/img/ghxw6.png",
                    "../assets/img/ghxw7.png"
                }
            };
        }

        protected override WebArticle Genernate(Resource resource, string originalId, string title, string originalUrl, DateTime? datetime)
        {
            if (string.IsNullOrEmpty(originalId)) return null;
            var doc = new HtmlDocument();
            doc.LoadHtml(smooth.Request<string>(() =>
            {
                return originalUrl.GetUriContentDirectly();
            }));
            HtmlNode node = null;
            var selectors = new string[] { };

            if (resource.Prefix.Equals(Plan_NewsPlan))
            {
                var header = doc.DocumentNode.SelectNodes("//table")[1];
                var body = doc.DocumentNode.SelectSingleNode("//table/*/td[@id='myTd']");
                node = GenernateContainer();
                node.AppendChild(header);
                node.AppendChild(GenernateContainer().AppendChild(body));
            }
            else
            {
                node = doc.DocumentNode.SelectSingleNode("//div[@class='news_show_content']");
                selectors = new string[] { "./div[@class='aboutread']", "./div[@class='aboutus']" };
            }
            if (node == null)
            {
                Logger.Error($"node is null {originalUrl},{resource.Url}");
                return null;
            }

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
                CoverImage = this.DetectConverImage(node, resource.DefaultImages, resource.Prefix) ?? null,
                Images = this.DetectImages(node, resource.DefaultImages, resource.Prefix) ?? null,
                HtmlContent = node.InnerHtml,
                Summary = node.InnerText.Clearup().TrySubstring(0, 260)
            };

        }

        public HtmlNode GenernateContainer()
        {
            var document = new HtmlDocument();
            document.LoadHtml("<div></div>");
            return document.DocumentNode;
        }
    }
}
