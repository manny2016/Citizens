using System;
using System.Collections.Generic;
using System.Text;

namespace Citizens.Core.Sync.Models
{
    public class Resource
    {

        public string Url { get; set; }

        public string Prefix { get; set; }

        public string[] ArticleKeyNames { get; set; }

        public string PublishToWhichChannel { get; set; }

        public string Description { get; set; }

        public string Host { get; set; }
        public string DefaultImage { get; set; }
        public string Name { get; set; }
    }
}
