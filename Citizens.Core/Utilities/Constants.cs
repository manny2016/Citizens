

namespace Citizens.Core
{
    using Citizens.Core.Models;
    using Microsoft.SqlServer.Server;
    using System.Data;
    using System.Data.SqlTypes;
    using System.Collections.Generic;
    public static class Constants
    {
        public static readonly SqlMetaData[] WebArticleStructure = new SqlMetaData[]
        {

            new SqlMetaData("ArticleID"         ,SqlDbType.VarChar,50),
            new SqlMetaData("ArticleTitle"      ,SqlDbType.VarChar,200),
            new SqlMetaData("ArticleSubtitle"   ,SqlDbType.VarChar,256),
            new SqlMetaData("CoverImage"        ,SqlDbType.VarChar,256),
            new SqlMetaData("AllowComent"       ,SqlDbType.VarChar,50),
            new SqlMetaData("Visits"            ,SqlDbType.Int),
            new SqlMetaData("ArticleStatus"     ,SqlDbType.VarChar,50),
            new SqlMetaData("OverdueTime"       ,SqlDbType.DateTime),
            new SqlMetaData("HtmlContent"       ,SqlDbType.VarChar,SqlMetaData.Max),
            new SqlMetaData("TextContent"       ,SqlDbType.VarChar),
            new SqlMetaData("VisitRights"       ,SqlDbType.VarChar,256),
            new SqlMetaData("Keyword"           ,SqlDbType.VarChar,100),
            new SqlMetaData("ArticleType"       ,SqlDbType.VarChar,50),
            new SqlMetaData("ArticleTime"       ,SqlDbType.DateTime),
            new SqlMetaData("ArticleWriter"     ,SqlDbType.VarChar,50),
            new SqlMetaData("ArticleSourceName" ,SqlDbType.VarChar,50),
            new SqlMetaData("ArticleSource"     ,SqlDbType.VarChar,256),
            new SqlMetaData("IsDeleted"         ,SqlDbType.Bit),
            new SqlMetaData("VersionNo"         ,SqlDbType.Int),
            new SqlMetaData("TransactionID"     ,SqlDbType.VarChar,50),
            new SqlMetaData("CreatedBy"         ,SqlDbType.VarChar,50),
            new SqlMetaData("CreatedTime"       ,SqlDbType.DateTime),
            new SqlMetaData("LastUpdatedBy"     ,SqlDbType.VarChar,50),
            new SqlMetaData("LastUpdatedTime"   ,SqlDbType.DateTime),
            new SqlMetaData("ArticleVisit"      ,SqlDbType.Int),
            new SqlMetaData("IsAnswer"          ,SqlDbType.Int),
            new SqlMetaData("MappingID"         ,SqlDbType.VarChar,50),
            new SqlMetaData("SvrID"             ,SqlDbType.VarChar,50),
            new SqlMetaData("AppID"             ,SqlDbType.VarChar,50),
            new SqlMetaData("ChannelID"         ,SqlDbType.VarChar,50),
            new SqlMetaData("Summary"           ,SqlDbType.VarChar,2000),
            new SqlMetaData("Images"            ,SqlDbType.VarChar,2000),
            new SqlMetaData("Category"          ,SqlDbType.VarChar,50),
            new SqlMetaData("OpenStyle"         ,SqlDbType.VarChar,50),
            new SqlMetaData("Url"               ,SqlDbType.VarChar,1000),
            new SqlMetaData("PublishUserName"   ,SqlDbType.VarChar,50)
        };
    }
}
