using System.Data.Common;
using System.Data.SqlClient;
using Tools.DAL.Database;

namespace Cookbook.DAL.Database
{
    public class SqlConnectionProvider : IDbConnectionProvider
    {
        public DbConnection GetOpenedDbConnection()
        {
            var connect = new SqlConnection(DatabaseInfo.DefaultConnectionString);

            connect.Open();

            return connect;
        }
    }
}
