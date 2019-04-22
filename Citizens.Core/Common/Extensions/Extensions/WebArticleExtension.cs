
namespace Citizens.Core
{

    using Citizens.Core.Models;
    using Microsoft.SqlServer.Server;
    using System;

    public static class WebArticleExtension
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(WebArticleExtension));
        public static SqlDataRecord ConvertToImportArticleDataRecord(this WebArticle article, string transactionId)
        {
            if (article == null) { return null; }

            try
            {
                var mappingid = Guid.NewGuid().ToString().ToUpper();
                var record = new SqlDataRecord(Constants.WebArticleStructure);
                for (var i = 0; i < Constants.WebArticleStructure.Length; i++)
                {
                    switch (Constants.WebArticleStructure[i].Name)
                    {
                        case "ArticleID":
                            SetStringToDataRecord(ref record, i, article.ArticleID);
                            break;
                        case "ArticleTitle":
                            SetStringToDataRecord(ref record, i, article.ArticleTitle);
                            break;
                        case "ArticleSubtitle":
                            SetStringToDataRecord(ref record, i, string.Empty);
                            break;
                        case "CoverImage":
                            SetStringToDataRecord(ref record, i, article.CoverImage);
                            break;
                        case "AllowComent":
                            SetStringToDataRecord(ref record, i, article.AllowComent);
                            break;
                        case "Visits":
                            SetInt32ToDataRecord(ref record, i, article.Visits);
                            break;
                        case "ArticleStatus":
                            SetStringToDataRecord(ref record, i, article.ArticleStatus);
                            break;
                        case "OverdueTime":
                            SetDateTimeToDataRecord(ref record, i, article.OverdueTime);
                            break;
                        case "HtmlContent":
                            SetStringToDataRecord(ref record, i, article.HtmlContent);
                            break;
                        case "TextContent":
                            SetStringToDataRecord(ref record, i, article.TextContent);
                            break;
                        case "VisitRights":
                            SetStringToDataRecord(ref record, i, article.VisitRights);
                            break;
                        case "Keyword":
                            SetStringToDataRecord(ref record, i, article.Keyword);
                            break;
                        case "ArticleType":
                            SetStringToDataRecord(ref record, i, article.ArticleType);
                            break;
                        case "ArticleTime":
                            SetDateTimeToDataRecord(ref record, i, article.ArticleTime);
                            break;
                        case "ArticleWriter":
                            SetStringToDataRecord(ref record, i, article.ArticleWriter);
                            break;
                        case "ArticleSourceName":
                            SetStringToDataRecord(ref record, i, article.ArticleSourceName);
                            break;
                        case "ArticleSource":
                            SetStringToDataRecord(ref record, i, article.OriginalUrl);
                            break;
                        case "IsDeleted":
                            SetBitToDataRecord(ref record, i, article.IsDeleted);
                            break;
                        case "VersionNo":
                            SetInt32ToDataRecord(ref record, i, article.VersionNo);
                            break;
                        case "TransactionID":
                            SetStringToDataRecord(ref record, i, transactionId);
                            break;
                        case "CreatedBy":
                            SetStringToDataRecord(ref record, i, article.CreatedBy);
                            break;
                        case "CreatedTime":
                            SetDateTimeToDataRecord(ref record, i, article.CreatedTime);
                            break;
                        case "LastUpdatedBy":
                            SetStringToDataRecord(ref record, i, article.LastUpdatedBy);
                            break;
                        case "LastUpdatedTime":
                            SetDateTimeToDataRecord(ref record, i, article.LastUpdatedTime);
                            break;
                        case "ArticleVisit":
                            SetInt32ToDataRecord(ref record, i, article.ArticleVisit);
                            break;
                        case "IsAnswer":
                            SetInt32ToDataRecord(ref record, i, 0);
                            break;
                        case "MappingID":
                            SetStringToDataRecord(ref record, i, mappingid);
                            break;
                        case "SvrID":
                            SetStringToDataRecord(ref record, i, "*");
                            break;
                        case "AppID":
                            SetStringToDataRecord(ref record, i, "APP.Foreground.WebPortals");
                            break;
                        case "ChannelID":
                            SetStringToDataRecord(ref record, i, article.PublishToWhichChannel);
                            break;
                        case "Summary":
                            SetStringToDataRecord(ref record, i, article.Summary);
                            break;
                        case "Images":
                            var images = article.Images == null ? string.Empty : string.Join(",", article.Images);
                            SetStringToDataRecord(ref record, i, images);
                            break;
                        case "Category":
                            SetStringToDataRecord(ref record, i, "ContentDetails");
                            break;
                        case "OpenStyle":
                            SetStringToDataRecord(ref record, i, null);
                            break;
                        case "Url":
                            SetStringToDataRecord(ref record, i, article.OpenUrl);
                            break;
                        case "PublishUserName":
                            SetStringToDataRecord(ref record, i, null);
                            break;
                    }
                }
                return record;

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return null;
            }
        }
        private static void SetStringToDataRecord(ref SqlDataRecord record, int index, string value)
        {
            if (value == null) { record.SetDBNull(index); }
            else { record.SetString(index, value); }
        }
        private static void SetInt32ToDataRecord(ref SqlDataRecord record, int index, int? value)
        {
            if (value.HasValue) { record.SetInt32(index, value.Value); }
            else { record.SetDBNull(index); }
        }
        private static void SetDoubleToDataRecord(ref SqlDataRecord record, int index, double? value)
        {
            if (value.HasValue) { record.SetDouble(index, value.Value); }
            else { record.SetDBNull(index); }
        }
        private static void SetDateTimeToDataRecord(ref SqlDataRecord record, int index, DateTime? value)
        {
            if (value.HasValue) { record.SetDateTime(index, value.Value); }
            else { record.SetDBNull(index); }
        }
        private static void SetBitToDataRecord(ref SqlDataRecord record, int index, bool? value)
        {
            try
            {
                if (value.HasValue) { record.SetSqlBoolean(index, value.Value); }
                else { record.SetDBNull(index); }
            }
            catch (Exception ex)
            {

            }

        }
    }
}
