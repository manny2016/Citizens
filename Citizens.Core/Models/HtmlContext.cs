using System;
using System.Collections.Generic;
using System.Text;

namespace Citizens.Core.Models
{
    public class HtmlContext : IEntityWithTimestamp
    {
        public string OriginalUrl { get; set; }
        public string Id { get; set; }
        public string Prefix { get; set; }
        public string Context { get; set; }
        public bool Innerlink { get; set; }
        public long Timestamp { get; set; }
    }
}
