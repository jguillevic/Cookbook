using Cookbook.UWP.DifficultyServiceReference;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cookbook.UWP.Recipe
{
    public class DifficultyDataProvider
    {
        private static bool _isComplete;

        public static List<Difficulty> Difficulties { get; set; }

        static DifficultyDataProvider()
        {
            _isComplete = false;
            Difficulties = new List<Difficulty>();
        }

        public async static Task Populate()
        {
            if (!_isComplete)
            {
                var client = new DifficultyServiceClient();
                Difficulties.AddRange(await client.LoadAsync());
                _isComplete = true;
            }
        }
    }
}
