

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

    public abstract class SynchronousService : IProcessService<HtmlContext>
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(SynchronousService));
        private readonly IMemoryCache cache = CitizensHost.GetService<IMemoryCache>();
        public void Dispose()
        {

        }


        public void Process(Action<HtmlContext> pass, CancellationToken token)
        {
            foreach (var resource in this.GetResources())
            {
                var doc = new HtmlDocument();
                var html = resource.Url.GetUriContent();
                if (this.HasChanged(resource.Key, html))
                {
                    Logger.Info("There is no change in source page");
                    return;
                }
                doc.LoadHtml(html);
                var contexts = Genernate(doc, resource);
                foreach (var content in contexts)
                {
                    pass(content);
                }
                Logger.Info($"Taken {contexts.Count()} contexts from {resource.Url}");
            }
        }
        private bool HasChanged(string key, string html)
        {
            var oldmd5 = cache.GetOrCreate<string>(key, (entity) =>
            {
                entity.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1);
                return string.Empty;
            });
            var newmd5 = html.GetMD5FromString();
            var nochagne = oldmd5 == newmd5;
            if (nochagne == false)
            {
                cache.Set<string>(key, newmd5);
            }
            return nochagne;

        }
        protected abstract IEnumerable<Resource> GetResources();

        protected abstract IEnumerable<HtmlContext> Genernate(HtmlDocument document, Resource resource);
    }
}
