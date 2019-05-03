

namespace Citizens.Core.Service.Sync
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading;
    using Citizens.Core.Models;
    using Citizens.Core.Sync.Models;
    using System.Collections.Generic;
    using System.Linq;
    using HtmlAgilityPack;
    using Microsoft.Extensions.Caching.Memory;

    public abstract class SynchronousService : IProcessService<WebArticle>
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(SynchronousService));
        private readonly IMemoryCache cache = CitizensHost.GetService<IMemoryCache>();
        protected static SmoothRequestingManagement smooth = new SmoothRequestingManagement(3, 1);
        protected SynchronousSettings Settings { get; private set; }
        public SynchronousService(SynchronousSettings settings)
        {
            this.Settings = settings;
            this.Settings.LeadSources = this.Settings.LeadSources ?? new LeadSource[] { };
        }
        public void Dispose()
        {

        }
        protected IEnumerable<Resource> LoadResourceFromSettings()
        {
            return this.GetResources().SelectMany((ctx) =>
             {
                 var list = this.Settings.LeadSources.Where(o => o.Prefix.Equals(ctx.Prefix))
                 .Select((lead) =>
                 {
                     return new Resource()
                     {
                         ArticleKeyNames = ctx.ArticleKeyNames,
                         DefaultImages = ctx.DefaultImages,
                         Description = ctx.Description,
                         Host = ctx.Host,
                         Name = ctx.Name,
                         Prefix = ctx.Prefix,
                         PublishToWhichChannel = ctx.PublishToWhichChannel,
                         Url = lead.Url
                     };
                 });
                 if (list == null || list.Count().Equals(0))
                 {
                     list = new List<Resource>() { ctx };
                 }
                 return list;
             });
        }

        public virtual void Process(Action<WebArticle> pass, CancellationToken token)
        {
            foreach (var resource in this.LoadResourceFromSettings())
            {
                var doc = new HtmlDocument();


                var html = resource.Url.GetUriContent();
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
                    }
                }
            }
        }
        protected bool HasChanged(string prefix, string html)
        {
            var oldmd5 = cache.GetOrCreate<string>(prefix, (entity) =>
            {
                entity.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1);
                return string.Empty;
            });
            var newmd5 = html.GetMD5FromString();
            var nochagne = oldmd5 == newmd5;
            if (nochagne == false)
            {
                cache.Set<string>(prefix, newmd5);
            }
            return nochagne;

        }
        protected abstract IEnumerable<Resource> GetResources();

        protected abstract IEnumerable<WebArticle> Genernate(HtmlDocument document, Resource resource);
        protected virtual string DetectConverImage(HtmlNode node, string[] defaultImages, string prefix)
        {
            var counter = CitizensHost.GetService<Counter>().Get(prefix);
            var image = node == null ? null : node.SelectSingleNode("./*/img");
            if (image == null)
            {
                if (defaultImages != null)
                {
                    return defaultImages[Math.Abs(counter % defaultImages.Length)];
                }
                else
                {
                    return null;
                }
            };
            return image.Attributes["src"].Value;
        }
        protected virtual string[] DetectImages(HtmlNode node, string[] defaultImages, string prefix)
        {
            var counter = CitizensHost.GetService<Counter>().Get(prefix);
            var images = node == null ? null : node.SelectNodes("./*/img");
            if (images == null || images.Count.Equals(0))
            {
                if (defaultImages != null)
                {
                    return new string[] { defaultImages[Math.Abs(counter % defaultImages.Length)] };
                }
            };
            return images.Select(o => o.Attributes["src"].Value).ToArray();
        }
        protected abstract WebArticle Genernate(Resource resource, string originalId, string title, string originalUrl, DateTime? datetime);
    }
}
