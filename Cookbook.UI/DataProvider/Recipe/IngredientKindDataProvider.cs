using Cookbook.Entity.Recipe;
using Cookbook.ServiceClient.Recipe;
using System.Threading.Tasks;
using Tools.UI.DataProvider;

namespace Cookbook.UI.DataProvider.Recipe
{
    public sealed class IngredientKindDataProvider : DataProviderBase<IngredientKind>
    {
        public IngredientKindDataProvider() : base() { }

        public async override Task PopulateAsync()
        {
            Items.Clear();
            Items.AddRange(await IngredientKindServiceClient.LoadAsync());
        }
    }
}
