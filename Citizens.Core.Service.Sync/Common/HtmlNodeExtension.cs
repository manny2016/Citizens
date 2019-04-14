using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Citizens.Core.Service.Sync
{
    public static class HtmlNodeExtension
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(HtmlNodeExtension));
        public static HtmlNode Clearup(this HtmlNode node, string[] selectors)
        {
            foreach (string selector in selectors)
            {
                var remove = node.SelectSingleNode(selector);
                if (remove != null)
                {
                    node.RemoveChild(remove);
                }
            }
            return node;
        }
        public static HtmlNode TrimImageUrl(this HtmlNode parent, string root)
        {
            var x = parent.InnerHtml.IndexOf("<img");
            var nodes = parent.SelectNodes("./*/img");
            if (nodes != null)
            {
                foreach (var node in nodes)
                {
                    var url = node.Attributes["src"].Value;
                    node.SetAttributeValue("src", url.TrimHttplink(root));
                    //Logger.InfoFormat("trim image url: {0}->{1}", url, url.TrimHttplink(root));
                }
            }
            return parent;
        }
        public static HtmlNode TrimInsideUrl(this HtmlNode parent, string root)
        {
            var nodes = parent.SelectNodes("./*/a");
            if (nodes != null)
            {
                foreach (var node in nodes)
                {
                    var url = node.Attributes["href"].Value;
                    node.SetAttributeValue("href", url.TrimHttplink(root));
                    //Logger.InfoFormat("trim a href: {0}->{1}", url, url.TrimHttplink(root));
                }
            }
            return parent;
        }
        public static HtmlNode TrimStyles(this HtmlNode parent)
        {
            var ignores = new string[] { "#text", "#comment" };
            foreach (var node in parent.ChildNodes)
            {
                try
                {
                    if (!ignores.Any(o => o.Equals(node.Name.ToLower())))
                    {
                        node.RemoveClass(false);
                        node.SetAttributeValue("style", string.Empty);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error(ex.Message);
                }
            }
            return parent;
        }
    }
}
