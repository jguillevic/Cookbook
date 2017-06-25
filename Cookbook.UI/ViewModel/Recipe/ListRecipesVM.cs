using Cookbook.Rule.Recipe;
using Cookbook.ServiceClient.Recipe;
using Cookbook.UI.ViewData.Recipe;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Tools.UI.ViewModel;

namespace Cookbook.UI.ViewModel.Recipe
{
    public class ListRecipesVM : ViewModelBase
    {
        public ObservableCollection<RecipeSummaryVD> Recipes { get; private set; }

        public RecipeFilterVD Filter { get; private set; }

        public ListRecipesVM()
        {
            Recipes = new ObservableCollection<RecipeSummaryVD>();
        }

        public void Initialize()
        {
            var filter = RecipeFilterRule.GetDefaultRecipeFilter();
            Filter = new RecipeFilterVD(filter);
        }

        public async Task Populate()
        {
            var recipes = await RecipeServiceClient.LoadAsync(Filter.GetEntity());
            Recipes.Clear();
            recipes.ForEach(item => Recipes.Add(new RecipeSummaryVD(item)));  
        }
    }
}
