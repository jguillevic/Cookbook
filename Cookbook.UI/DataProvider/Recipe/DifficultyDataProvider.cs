using Cookbook.Entity.Recipe;
using Cookbook.ServiceClient.Recipe;
using System.Threading.Tasks;
using Tools.UI.DataProvider;

namespace Cookbook.UI.DataProvider.Recipe
{
    public sealed class DifficultyDataProvider : DataProviderBase<Difficulty>
    {
        public DifficultyDataProvider() : base() { }

        public async override Task PopulateAsync()
        {
            Items.Clear();
            Items.AddRange(await DifficultyServiceClient.LoadAsync());
        }
    }
}
