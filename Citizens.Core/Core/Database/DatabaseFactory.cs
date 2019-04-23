

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
                connectString = GenerateMSSqlConnectionString("DESKTOP-CF53RUS", "smzy", "sa", "Window2008");
#else
            if (string.IsNullOrEmpty(connectString))
                connectString = GenerateMSSqlConnectionString("172.18.2.11", "smzy_main_20160225", "smzy", "Window2008");
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
