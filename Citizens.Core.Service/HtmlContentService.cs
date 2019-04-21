
namespace Citizens.Core.Service
{
    using Citizens.Core.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    public class HtmlContentService : IProcessingResultService<WebArticle>
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(HtmlContentService));

        public void Dispose()
        {

        }

        public void Save(IEnumerable<WebArticle> results)
        {
            using(var database = DatabaseFactory.GenerateDatabase())
            {

            }
        }
    }
}
