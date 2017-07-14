using System.Threading.Tasks;
using Cookbook.Entity.Recipe;
using Tools.UI.DataProvider;
using Cookbook.ServiceClient.Recipe;

namespace Cookbook.UI.DataProvider.Recipe
{
    public sealed class SeasonDataProvider : DataProviderBase<Season>
    {
        public SeasonDataProvider() : base() { }

        public async override Task PopulateAsync()
        {
            Items.Clear();
            Items.AddRange(await SeasonServiceClient.LoadAsync());
        }
    }
}
