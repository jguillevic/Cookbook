using Cookbook.Rule.Recipe;
using Cookbook.ServiceClient.Recipe;
using Cookbook.UI.ViewData.Recipe;
using Cookbook.UI.ViewModel.Home;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tools.UI.Command;
using Tools.UI.ViewModel;
using static Cookbook.Entity.Recipe.RecipeEntityDescriptions;

namespace Cookbook.UI.ViewModel.Recipe
{
    public class ListRecipesVM : ListViewModel<RecipeSummaryVD>
    {
        public RecipeFilterVD Filter { get; private set; }

        public DelegateCommand AddCommand { get; set; }
        public DelegateCommand UpdateCommand { get; set; }
        public DelegateCommand RefreshCommand { get; set; }
        public DelegateCommand GoToRecipeCrawlerCommand { get; set; }
        public DelegateCommand GoToHomeCommand { get; set; }

        public ListRecipesVM()
        {
            AddCommand = new DelegateCommand(AddCommandExecute);
            UpdateCommand = new DelegateCommand(UpdateCommandExecute);
            RefreshCommand = new DelegateCommand(RefreshCommandExecute);
            GoToRecipeCrawlerCommand = new DelegateCommand(GoToRecipeCrawlerCommandExecute);
            GoToHomeCommand = new DelegateCommand(GoToHomeCommandExecute);
        }

        public override void Initialize()
        {
            var filter = RecipeFilterRule.GetDefault();
            Filter = new RecipeFilterVD(filter);
        }

        public override async Task PopulateAsync()
        {
            Items.Clear();

            var recipes 
                = await RecipeServiceClient.LoadAsync(
                    Filter.GetEntity()
                    , new List<string> {
                        RecipeEntityDescription.Id
                        , RecipeEntityDescription.Name
                        , RecipeEntityDescription.Description
                        , RecipeEntityDescription.CookingTime
                        , RecipeEntityDescription.PreparationTime });

            var recipesVD = new List<RecipeSummaryVD>(recipes.Count);
            recipes.ForEach(item => recipesVD.Add(new RecipeSummaryVD(item)));

            Items.AddRange(recipesVD);
        }

        private async void AddCommandExecute(object obj)
        {
            await Setter.SetCurrentViewModelAsync(new AddOrUpdateRecipeVM());
        }

        private async void UpdateCommandExecute(object obj)
        {
            await Setter.SetCurrentViewModelAsync(new AddOrUpdateRecipeVM(Items[SelectedIndex].Id));
        }

        private async void RefreshCommandExecute(object obj)
        {
            await PopulateAsync();
        }

        private async void GoToRecipeCrawlerCommandExecute(object obj)
        {
            await Setter.SetCurrentViewModelAsync(new RecipeCrawlerVM());
        }

        private async void GoToHomeCommandExecute(object obj)
        {
            await Setter.SetCurrentViewModelAsync(new HomeViewModel());
        }
    }
}
