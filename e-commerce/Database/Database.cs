using System.Configuration;
using System.Data.SqlClient;

namespace e_commerce.Database
{
    //class for handlling database connection
    internal class Database
    {
        //singleton instance
        private static SqlConnection? conn = null;

        private Database()
        {

        }

        //returns singleton instance of SqlConnection
        public static SqlConnection GetConnection()
        {
            if (conn == null)
            {
                SqlConnectionStringBuilder consStringBuilder = new SqlConnectionStringBuilder();
                consStringBuilder.UserID = readConfig("Name");
                consStringBuilder.Password = readConfig("Password");
                consStringBuilder.InitialCatalog = readConfig("Database");
                consStringBuilder.DataSource = readConfig("DataSource");
                consStringBuilder.TrustServerCertificate = true;
                consStringBuilder.ConnectTimeout = 30;
                conn = new SqlConnection(consStringBuilder.ConnectionString);
                conn.Open();
            }
            return conn;
        }

        //close SqlConnection
        public static void CloseConnection()
        {
            if (conn != null)
            {
                conn.Close();
                conn.Dispose();
            }
        }

        //returns string from App.config
        //input string key of config value
        private static string readConfig(string key)
        {
            var appConfig = ConfigurationManager.AppSettings;
            return appConfig[key] ?? "not found";
        }
    }
}
