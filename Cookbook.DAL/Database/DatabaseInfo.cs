using System.Web.Configuration;

namespace Cookbook.DAL.Database
{
    public static class DatabaseInfo
    {
        public static string DefaultConnectionString;

        static DatabaseInfo()
        {
            DefaultConnectionString = WebConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString;
        }
    }
}