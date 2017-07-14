using Cookbook.UI.DataProvider;
using Cookbook.UI.DataProvider.Recipe;
using Tools.UI.ViewModel;

namespace Cookbook.UI.ViewModel
{
    public sealed class CookbookAppVM : ApplicationViewModel
    {
        public CookbookAppVM(): base() { }

        public override void Initialize()
        {
            base.Initialize();

            DataProviderManager.AddDataProvider(DataProviderKeys.CostDataProviderKey, new CostDataProvider());
            DataProviderManager.AddDataProvider(DataProviderKeys.DifficultyDataProviderKey, new DifficultyDataProvider());
            DataProviderManager.AddDataProvider(DataProviderKeys.FeatureDataProviderKey, new FeatureDataProvider());
            DataProviderManager.AddDataProvider(DataProviderKeys.RecipeKindDataProviderKey, new RecipeKindDataProvider());
            DataProviderManager.AddDataProvider(DataProviderKeys.SeasonDataProviderKey, new SeasonDataProvider());
            DataProviderManager.AddDataProvider(DataProviderKeys.IngredientKindDataProviderKey, new IngredientKindDataProvider());
        }
    }
}
