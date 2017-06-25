using Microsoft.Extensions.Configuration;

namespace Tools.Configuration.Configuration
{
    public class ConfigurationHelper
    {
        public static T Generate<T>(string path)
            where T : new()
        {
            var builder = new ConfigurationBuilder().AddJsonFile(path, optional: false, reloadOnChange: true);
            var configuration = builder.Build();
            var config = new T();
            configuration.Bind(config);

            return config;
        }
    }
}
