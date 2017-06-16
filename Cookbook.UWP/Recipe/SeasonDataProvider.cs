using Cookbook.UWP.SeasonServiceReference;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cookbook.UWP.Recipe
{
    public static class SeasonDataProvider
    {
        private static bool _isComplete;

        public static List<Season> Seasons { get; set; }

        static SeasonDataProvider()
        {
            _isComplete = false;
            Seasons = new List<Season>();
        }

        public async static Task Populate()
        {
            if (!_isComplete)
            {
                var client = new SeasonServiceClient();
                Seasons.AddRange(await client.LoadAsync());
                _isComplete = true;
            }
        }
    }
}
