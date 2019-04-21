using System;
using System.Collections.Generic;
using System.Text;

namespace Citizens.Core.Models
{
    public class WebArticle : IEntityWithTimestamp
    {
        public WebArticle(string prefix, string originalId, string channel)
        {
            this.ArticleID = $"{prefix}{originalId}";
            this.AllowComent = "High";
            this.ArticleStatus = "Generated";
            this.ArticleType = "AutoCollection";
            this.CreatedBy = "Citizens.Agent.Synchronizer";
            this.CreatedTime = DateTime.Now;
            this.LastUpdatedBy = "Citizens.Agent.Synchronizer";
            this.LastUpdatedTime = DateTime.UtcNow;
            this.OverdueTime = DateTime.Now.AddYears(1);
            this.PublishToWhichChannel = channel;
            this.VersionNo = 1;
            this.VisitRights = "High";
            this.Visits = 0;
            this.IsDeleted = false;
            this.Keyword = string.Empty;
            this.OpenUrl = $"/Site/ArticleDetalis?articleid={this.ArticleID}";
        }
        public WebArticle() { }

        public string ArticleID { get; set; }

        public string ArticleTitle { get; set; }

        public string CoverImage { get; set; }

        public string AllowComent { get; set; }

        public int Visits { get; set; }

        public string ArticleStatus { get; set; }

        public DateTime OverdueTime { get; set; }

        public string VisitRights { get; set; }

        public string Keyword { get; set; }

        public string ArticleType { get; set; }

        public DateTime ArticleTime { get; set; }

        public string ArticleWriter { get; set; }

        public string ArticleSourceName { get; set; }

        public string OriginalUrl { get; set; }

        public bool IsDeleted { get; set; }

        public int VersionNo { get; set; }

        public string TransactionID { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedTime { get; set; }

        public string LastUpdatedBy { get; set; }

        public DateTime LastUpdatedTime { get; set; }

        public int ArticleVisit { get; set; }

        public string HtmlContent { get; set; }

        public string OpenUrl { get; set; }
        
        public string TextContent { get; set; }
        public string[] Images { get; set; }
        public string PublishToWhichChannel { get; set; }
        public string Summary { get; set; }
        public long Timestamp { get; set; }

    }
}

