using Cookbook.DAL.Configuration;

namespace Cookbook.DAL.Database
{
    public static class DatabaseInfo
    {
        public static string DefaultConnectionString;

        static DatabaseInfo()
        {
            DefaultConnectionString = DALConfiguration.Instance.ConnectionStrings.DefaultConnection;
        }
    }
}