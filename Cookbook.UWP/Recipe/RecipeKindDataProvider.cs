using Cookbook.UWP.RecipeKindServiceReference;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cookbook.UWP.Recipe
{
    public static class RecipeKindDataProvider
    {
        private static bool _isComplete;

        public static List<RecipeKind> RecipeKinds { get; set; }

        static RecipeKindDataProvider()
        {
            _isComplete = false;
            RecipeKinds = new List<RecipeKind>();
        }

        public async static Task Populate()
        {
            if (!_isComplete)
            {
                var client = new RecipeKindServiceClient();
                RecipeKinds.AddRange(await client.LoadAsync());
                _isComplete = true;
            }
        }
    }
}
