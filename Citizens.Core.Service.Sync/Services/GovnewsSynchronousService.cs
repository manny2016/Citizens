


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

        private readonly string[] DefaultImages = new string[] {
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
                };
        public GovnewsSynchronousService(SynchronousSettings settings) : base(settings)
        {
        }

        protected override IEnumerable<Resource> GetResources()
        {
            yield return new Resource()
            {
                Url = "http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index.shtml",
                PublishToWhichChannel = "4ec12bb8-f454-11e4-aa81-0010030f0c17",
                Description = "宜兴政务-要闻中心",
                ArticleKeyNames = new string[] { },
                Prefix = "govcn.news.",
                Host = "http://www.yixing.gov.cn",
                Name = "要闻中心",
                DefaultImages = this.DefaultImages
            };

            yield return new Resource()
            {
                Url = "http://www.yixing.gov.cn/zgyx/zxzx/jcdt/index.shtml",
                PublishToWhichChannel = "e6b72975-f445-11e4-aa81-0010030f0c17",
                Description = "宜兴政务-基层动态",
                ArticleKeyNames = new string[] { },
                Prefix = "govcn.basic.",
                Host = "http://www.yixing.gov.cn",
                Name = "政务信息",
                DefaultImages = this.DefaultImages
            };

            yield return new Resource()
            {
                Url = "http://www.yixing.gov.cn/zgyx/zxzx/sjjd/index.shtml",
                PublishToWhichChannel = "e6b72975-f445-11e4-aa81-0010030f0c17",
                Description = "宜兴政务-基层动态",
                ArticleKeyNames = new string[] { },
                Prefix = "govcn.focus.",
                Host = "http://www.yixing.gov.cn",
                Name = "政务信息",
                DefaultImages = this.DefaultImages
            };

        }
        public override void Process(Action<WebArticle> pass, CancellationToken token)
        {
            foreach (var resource in this.LoadResourceFromSettings())
            {
                var doc = new HtmlDocument();
                var html = resource.Url.GetUriContentDirectly((http) =>
              {
                  http.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3";
                  http.Headers.Add("Accept-Encoding", "gzip, deflate");
                  http.Headers.Add("Accept-Language", "en-US,en;q=0.9");
                  http.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.108 Safari/537.36";
                  return http;
              });

                if (this.HasChanged(resource.Prefix, html))
                {
                    Logger.Info("There is no change in source page");
                    return;
                }
                doc.LoadHtml(html);
                foreach (var article in this.Genernate(doc, resource))
                {
                    if (article != null)
                    {
                        pass(article);
                        Logger.InfoFormat("Convert 1 article from {0}", resource.Host);
                    }
                }
            }
        }
        protected override IEnumerable<WebArticle> Genernate(HtmlDocument document, Resource resource)
        {
            var list = document.DocumentNode.SelectNodes(@"//div[@class='zxzx_list']/*/a");
            if (list == null) yield break;

            foreach (var node in list)
            {
                var datetime = node.SelectSingleNode(@"./span").InnerText.TrytoDateTime();
                var span = node.SelectSingleNode("./span");
                if (span != null)
                {
                    node.RemoveChild(span);
                }
                var url = node.Attributes["href"].Value.TrimHttplink(resource.Host);
                var title = node.InnerText;
                var contextId = url.GetIdfromurl();
                yield return Genernate(resource, contextId, title, url, datetime);
            }
        }
        protected override WebArticle Genernate(Resource resource, string originalId, string title, string originalUrl, DateTime? datetime)
        {
            if (string.IsNullOrEmpty(originalId)) return null;
            var doc = new HtmlDocument();
            var html = smooth.Request<string>(() =>
            {
                return originalUrl.GetUriContentDirectly((http) =>
                {
                    http.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3";
                    http.Headers.Add("Accept-Encoding", "gzip, deflate");
                    http.Headers.Add("Accept-Language", "en-US,en;q=0.9");
                    http.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.108 Safari/537.36";
                    http.KeepAlive = true;
                    http.Headers.Add("Cache-Control", "no-cache");
                    return http;
                });
            });
            doc.LoadHtml(html);
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
                CoverImage = this.DetectConverImage(node, resource.DefaultImages, resource.Prefix) ?? null,
                Images = this.DetectImages(node, resource.DefaultImages, resource.Prefix) ?? new string[] { },
                HtmlContent = node.InnerHtml,
                Summary = node.InnerText.Clearup().TrySubstring(0, 260)
            };
        }
    }
}
