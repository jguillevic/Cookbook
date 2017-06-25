using Tools.Configuration.Configuration;

namespace Cookbook.Service.Configuration
{
    public class ServiceConfiguration
    {
        public string BaseUrl { get; set; }
        public string RecipeRoute { get; set; }
        public string CostRoute { get; set; }
        public string DifficultyRoute { get; set; }
        public string RecipeKindRoute { get; set; }
        public string SeasonRoute { get; set; }
        public string FeatureRoute { get; set; }
        public string IngredientRoute { get; set; }
        public string MeasureRoute { get; set; }

        public static ServiceConfiguration Instance { get; private set; }

        static ServiceConfiguration()
        {
            Instance = ConfigurationHelper.Generate<ServiceConfiguration>("Configuration\\ServiceConfiguration.json");
        }
    }
}
