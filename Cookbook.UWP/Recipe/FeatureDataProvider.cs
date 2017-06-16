using Cookbook.UWP.FeatureServiceReference;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cookbook.UWP.Recipe
{
    public class FeatureDataProvider
    {
        private static bool _isComplete;

        public static List<Feature> Features { get; set; }

        static FeatureDataProvider()
        {
            _isComplete = false;
            Features = new List<Feature>();
        }

        public async static Task Populate()
        {
            if (!_isComplete)
            {
                var client = new FeatureServiceClient();
                Features.AddRange(await client.LoadAsync());
                _isComplete = true;
            }
        }
    }
}
