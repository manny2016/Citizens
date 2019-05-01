

namespace Citizens.Core.Sync.Models
{
    public class LeadSource
    {
        public LeadSource(string prefix,string url)
        {
            this.Prefix = prefix;
            this.Url = url;
        }
        public string Prefix { get; set; }
        public string Url { get; set; }
    }
}
