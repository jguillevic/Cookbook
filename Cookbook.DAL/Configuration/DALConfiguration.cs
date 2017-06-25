using Tools.Configuration.Configuration;

namespace Cookbook.DAL.Configuration
{
    public class DALConfiguration
    {
        public ConnectionStrings ConnectionStrings { get; set; }

        public static DALConfiguration Instance { get; private set; }

        static DALConfiguration()
        {
            Instance = ConfigurationHelper.Generate<DALConfiguration>("Configuration\\DALConfiguration.json");           
        }
    }

    public class ConnectionStrings
    {
        public string DefaultConnection { get; set; }
    }
}
