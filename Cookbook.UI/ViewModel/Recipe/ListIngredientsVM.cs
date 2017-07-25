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
    public class ListIngredientsVM : ListViewModel<IngredientSummaryVD>
    {
        public DelegateCommand AddCommand { get; set; }
        public DelegateCommand UpdateCommand { get; set; }
        public DelegateCommand RefreshCommand { get; set; }
        public DelegateCommand GoToHomeCommand { get; set; }

        public ListIngredientsVM() : base()
        {
            AddCommand = new DelegateCommand(AddCommandExecute);
            UpdateCommand = new DelegateCommand(UpdateCommandExecute);
            RefreshCommand = new DelegateCommand(RefreshCommandExecute);
            GoToHomeCommand = new DelegateCommand(GoToHomeCommandExecute);
        }

        public override async Task PopulateAsync()
        {
            Items.Clear();

            var ingredients = await IngredientServiceClient.LoadAsync(new List<string> { IngredientEntityDescription.Id, IngredientEntityDescription.Name });

            var ingredientsVD = new List<IngredientSummaryVD>(ingredients.Count);
            ingredients.ForEach(item => ingredientsVD.Add(new IngredientSummaryVD(item)));

            Items.AddRange(ingredientsVD);
        }

        private async void AddCommandExecute(object obj)
        {
            await Setter.SetCurrentViewModelAsync(new AddOrUpdateIngredientVM());
        }

        private async void UpdateCommandExecute(object obj)
        {
            await Setter.SetCurrentViewModelAsync(new AddOrUpdateIngredientVM(Items[SelectedIndex].Id));
        }

        private async void RefreshCommandExecute(object obj)
        {
            await PopulateAsync();
        }

        private async void GoToHomeCommandExecute(object obj)
        {
            await Setter.SetCurrentViewModelAsync(new HomeViewModel());
        }
    }
}
