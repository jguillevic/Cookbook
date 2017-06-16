using Cookbook.UWP.CostServiceReference;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cookbook.UWP.Recipe
{
    public static class CostDataProvider
    {
        private static bool _isComplete;

        public static List<Cost> Costs { get; set; }

        static CostDataProvider()
        {
            _isComplete = false;
            Costs = new List<Cost>();
        }

        public async static Task Populate()
        {
            if (!_isComplete)
            {
                var client = new CostServiceClient();
                Costs.AddRange(await client.LoadAsync());
                _isComplete = true;
            }
        }
    }
}
