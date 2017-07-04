using Cookbook.Rule.Recipe;
using Cookbook.ServiceClient.Recipe;
using Cookbook.UI.ViewData.Recipe;
using Cookbook.UI.ViewModel.Home;
using System.Collections.ObjectModel;
using Tools.UI.Command;
using Tools.UI.ViewModel;

namespace Cookbook.UI.ViewModel.Recipe
{
    public class ListRecipesVM : PageViewModel
    {
        public ObservableCollection<RecipeSummaryVD> Recipes { get; private set; }

        public RecipeFilterVD Filter { get; private set; }

        public DelegateCommand AddCommand { get; set; }
        public DelegateCommand UpdateCommand { get; set; }
        public DelegateCommand RefreshCommand { get; set; }
        public DelegateCommand GoToHomeCommand { get; set; }

        public ListRecipesVM()
        {
            Recipes = new ObservableCollection<RecipeSummaryVD>();

            AddCommand = new DelegateCommand(AddCommandExecute);
            UpdateCommand = new DelegateCommand(UpdateCommandExecute);
            RefreshCommand = new DelegateCommand(RefreshCommandExecute);
            GoToHomeCommand = new DelegateCommand(GoToHomeCommandExecute);
        }

        public override void Initialize()
        {
            var filter = RecipeFilterRule.GetDefault();
            Filter = new RecipeFilterVD(filter);
        }

        public override async void Populate()
        {
            Recipes.Clear();
            var recipes = await RecipeServiceClient.LoadAsync(Filter.GetEntity());
            recipes.ForEach(item => Recipes.Add(new RecipeSummaryVD(item)));  
        }

        private void AddCommandExecute(object obj)
        {
            Setter.SetCurrentViewModel(new AddOrUpdateRecipeVM());
        }

        private void UpdateCommandExecute(object obj)
        {
            Setter.SetCurrentViewModel(new AddOrUpdateRecipeVM(Recipes[0].Id));
        }

        private void RefreshCommandExecute(object obj)
        {
            Populate();
        }

        private void GoToHomeCommandExecute(object obj)
        {
            Setter.SetCurrentViewModel(new HomeViewModel());
        }
    }
}
