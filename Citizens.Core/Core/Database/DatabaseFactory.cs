

namespace Citizens.Core
{
    using System.Data;
    using System.Data.SqlClient;

    public static class DatabaseFactory
    {
        public static IDbConnection GenerateDatabase(string connectString = null)
        {
#if DEBUG

            if (string.IsNullOrEmpty(connectString))
                connectString = GenerateMSSqlConnectionString("asfp-pbi.database.windows.net", "reporting_uat", "asfpd", "AsFp.Azure.2019");
#else
            if (string.IsNullOrEmpty(connectString))
                connectString = GenerateMSSqlConnectionString("asfp-pbi.database.windows.net", "reporting", "asfpd", "AsFp.Azure.2019");
#endif
            return new SqlConnection(connectString);

        }
        private static string GenerateMSSqlConnectionString(string server, string database, string userid, string password)
        {
            var builder = new SqlConnectionStringBuilder()
            {
                DataSource = server,
                InitialCatalog = database,
                UserID = userid,
                Password = password
            };
            return builder.ToString();
        }
    }
}
