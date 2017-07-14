using System.Threading.Tasks;
using Cookbook.Entity.Recipe;
using Tools.UI.DataProvider;
using Cookbook.ServiceClient.Recipe;

namespace Cookbook.UI.DataProvider.Recipe
{
    public sealed class CostDataProvider : DataProviderBase<Cost>
    {
        public CostDataProvider() : base() { }

        public async override Task PopulateAsync()
        {
            Items.Clear();
            Items.AddRange(await CostServiceClient.LoadAsync());
        }
    }
}
