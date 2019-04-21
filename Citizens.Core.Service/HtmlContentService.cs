
namespace Citizens.Core.Service
{
    using Citizens.Core.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Dapper;
    using System.Data.SqlClient;
    using System.Data;

    public class HtmlContentService : IProcessingResultService<WebArticle>
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(HtmlContentService));

        public void Dispose()
        {

        }

        public void Save(IEnumerable<WebArticle> results)
        {
            
            var dt = DateTime.Now;
            results = results.GroupBy(o => o.ArticleID).Select((o =>
            {
                return o.FirstOrDefault();
            }));
            using (var database = DatabaseFactory.GenerateDatabase())
            {
                try
                {
                    var transactionId = Guid.NewGuid().ToString().ToUpper();
                    if (database.State != ConnectionState.Open)
                        database.Open();
                    var command = database.CreateCommand();
                    command.CommandText = "[dbo].[spImportArticle]";
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 60 * 60;
                    command.Parameters.Add(new SqlParameter()
                    {
                        SqlDbType = SqlDbType.Structured,
                        TypeName = "[dbo].[ImportArticleStructure]",
                        Value = results.Select(o => o.ConvertToImportArticleDataRecord(transactionId)),
                        ParameterName = "@articles"
                    });
                    command.ExecuteNonQuery();
                    Logger.InfoFormat("Import {0} from . spend {1} sec.", results.Count(), DateTime.Now.Subtract(dt).TotalSeconds);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex.Message, ex);
                }
                finally
                {
                    if (database.State != ConnectionState.Closed)
                        database.Close();
                }
            }
        }
    }
}
