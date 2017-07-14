using Cookbook.ServiceClient.Recipe;
using Cookbook.UI.ViewData.Recipe;
using Cookbook.UI.ViewModel.Home;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Tools.UI.Command;
using Tools.UI.Common;
using Tools.UI.ViewModel;
using static Cookbook.Entity.Recipe.RecipeEntityDescriptions;

namespace Cookbook.UI.ViewModel.Recipe
{
    public class ListIngredientsVM : PageViewModel
    {
        public ObservableRangeCollection<IngredientSummaryVD> Ingredients { get; private set; }

        private int _selectedIndex;
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                if (_selectedIndex != value)
                {
                    _selectedIndex = value;
                    OnPropertyChanged("SelectedIndex");
                }
            }
        }

        public DelegateCommand AddCommand { get; set; }
        public DelegateCommand UpdateCommand { get; set; }
        public DelegateCommand RefreshCommand { get; set; }
        public DelegateCommand GoToHomeCommand { get; set; }

        public ListIngredientsVM() : base()
        {
            Ingredients = new ObservableRangeCollection<IngredientSummaryVD>();

            AddCommand = new DelegateCommand(AddCommandExecute);
            UpdateCommand = new DelegateCommand(UpdateCommandExecute);
            RefreshCommand = new DelegateCommand(RefreshCommandExecute);
            GoToHomeCommand = new DelegateCommand(GoToHomeCommandExecute);
        }

        public override async Task PopulateAsync()
        {
            Ingredients.Clear();

            var ingredients = await IngredientServiceClient.LoadAsync(new List<string> { IngredientEntityDescription.Id, IngredientEntityDescription.Name });

            var ingredientsVD = new List<IngredientSummaryVD>(ingredients.Count);
            ingredients.ForEach(item => ingredientsVD.Add(new IngredientSummaryVD(item)));

            Ingredients.AddRange(ingredientsVD);
        }

        private async void AddCommandExecute(object obj)
        {
            await Setter.SetCurrentViewModelAsync(new AddOrUpdateIngredientVM());
        }

        private async void UpdateCommandExecute(object obj)
        {
            await Setter.SetCurrentViewModelAsync(new AddOrUpdateIngredientVM(Ingredients[SelectedIndex].Id));
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
