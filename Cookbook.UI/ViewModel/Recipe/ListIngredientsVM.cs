using Cookbook.ServiceClient.Recipe;
using Cookbook.UI.ViewData.Recipe;
using Cookbook.UI.ViewModel.Home;
using System.Collections.ObjectModel;
using Tools.UI.Command;
using Tools.UI.ViewModel;

namespace Cookbook.UI.ViewModel.Recipe
{
    public class ListIngredientsVM : PageViewModel
    {
        public ObservableCollection<IngredientSummaryVD> Ingredients { get; private set; }

        public DelegateCommand AddCommand { get; set; }
        public DelegateCommand UpdateCommand { get; set; }
        public DelegateCommand RefreshCommand { get; set; }
        public DelegateCommand GoToHomeCommand { get; set; }

        public ListIngredientsVM() : base()
        {
            Ingredients = new ObservableCollection<IngredientSummaryVD>();

            AddCommand = new DelegateCommand(AddCommandExecute);
            UpdateCommand = new DelegateCommand(UpdateCommandExecute);
            RefreshCommand = new DelegateCommand(RefreshCommandExecute);
            GoToHomeCommand = new DelegateCommand(GoToHomeCommandExecute);
        }

        public override async void Populate()
        {
            Ingredients.Clear();
            var ingredients = await IngredientServiceClient.LoadAsync();         
            ingredients.ForEach(item => Ingredients.Add(new IngredientSummaryVD(item)));
        }

        private void AddCommandExecute(object obj)
        {

        }

        private void UpdateCommandExecute(object obj)
        {

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
