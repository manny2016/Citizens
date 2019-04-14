using System;
using System.Collections.Generic;
using System.Text;

namespace Citizens.Core.Sync.Models
{
    public class Resource
    {
        public string Key { get; set; }

        public string Url { get; set; }

        public string Prefix { get; set; }

        public string[] HtmlContextKeyNames { get; set; }

        public string Channel { get; set; }

        public string Description { get; set; }

        public string Host { get; set; }
    }
}
