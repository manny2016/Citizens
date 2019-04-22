

namespace Citizens.Core
{
    using Citizens.Core.Models;
    using Microsoft.SqlServer.Server;
    using System.Data;
    using System.Data;
    public static class Constants
    {
        
        public static readonly SqlMetaData[] WebArticleStructure = new SqlMetaData[]
        {

            new SqlMetaData("ArticleID"         ,SqlDbType.NVarChar,50),
            new SqlMetaData("ArticleTitle"      ,SqlDbType.NVarChar,200),
            new SqlMetaData("ArticleSubtitle"   ,SqlDbType.NVarChar,256),
            new SqlMetaData("CoverImage"        ,SqlDbType.NVarChar,256),
            new SqlMetaData("AllowComent"       ,SqlDbType.NVarChar,50),
            new SqlMetaData("Visits"            ,SqlDbType.Int),
            new SqlMetaData("ArticleStatus"     ,SqlDbType.NVarChar,50),
            new SqlMetaData("OverdueTime"       ,SqlDbType.DateTime),
            new SqlMetaData("HtmlContent"       ,SqlDbType.NVarChar,SqlMetaData.Max),// Must point the max length on type NVarChar
            new SqlMetaData("TextContent"       ,SqlDbType.NVarChar,SqlMetaData.Max),
            new SqlMetaData("VisitRights"       ,SqlDbType.NVarChar,256),
            new SqlMetaData("Keyword"           ,SqlDbType.NVarChar,100),
            new SqlMetaData("ArticleType"       ,SqlDbType.NVarChar,50),
            new SqlMetaData("ArticleTime"       ,SqlDbType.DateTime),
            new SqlMetaData("ArticleWriter"     ,SqlDbType.NVarChar,50),
            new SqlMetaData("ArticleSourceName" ,SqlDbType.NVarChar,50),
            new SqlMetaData("ArticleSource"     ,SqlDbType.NVarChar,256),
            new SqlMetaData("IsDeleted"         ,SqlDbType.Bit),
            new SqlMetaData("VersionNo"         ,SqlDbType.Int),
            new SqlMetaData("TransactionID"     ,SqlDbType.NVarChar,50),
            new SqlMetaData("CreatedBy"         ,SqlDbType.NVarChar,50),
            new SqlMetaData("CreatedTime"       ,SqlDbType.DateTime),
            new SqlMetaData("LastUpdatedBy"     ,SqlDbType.NVarChar,50),
            new SqlMetaData("LastUpdatedTime"   ,SqlDbType.DateTime),
            new SqlMetaData("ArticleVisit"      ,SqlDbType.Int),
            new SqlMetaData("IsAnswer"          ,SqlDbType.Int),
            new SqlMetaData("MappingID"         ,SqlDbType.NVarChar,50),
            new SqlMetaData("SvrID"             ,SqlDbType.NVarChar,50),
            new SqlMetaData("AppID"             ,SqlDbType.NVarChar,50),
            new SqlMetaData("ChannelID"         ,SqlDbType.NVarChar,50),
            new SqlMetaData("Summary"           ,SqlDbType.NVarChar,2000),
            new SqlMetaData("Images"            ,SqlDbType.NVarChar,2000),
            new SqlMetaData("Category"          ,SqlDbType.NVarChar,50),
            new SqlMetaData("OpenStyle"         ,SqlDbType.NVarChar,50),
            new SqlMetaData("Url"               ,SqlDbType.NVarChar,1000),
            new SqlMetaData("PublishUserName"   ,SqlDbType.NVarChar,50)
        };
    }
}
