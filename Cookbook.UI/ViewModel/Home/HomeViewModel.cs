using Cookbook.UI.ViewModel.Recipe;
using Tools.UI.Command;
using Tools.UI.ViewModel;

namespace Cookbook.UI.ViewModel.Home
{
    public class HomeViewModel : PageViewModel
    {
        public DelegateCommand GoToListIngredientsCommand { get; set; }
        public DelegateCommand GoToListMeasuresCommand { get; set; }
        public DelegateCommand GoToListRecipesCommand { get; set; }

        public HomeViewModel() : base()
        {
            GoToListIngredientsCommand = new DelegateCommand(GoToListIngredientsCommandExecute);
            GoToListMeasuresCommand = new DelegateCommand(GoToListMeasuresCommandExecute);
            GoToListRecipesCommand = new DelegateCommand(GoToListRecipesCommandExecute);
        }

        private async void GoToListIngredientsCommandExecute(object obj)
        {
            await Setter.SetCurrentViewModelAsync(new ListIngredientsVM());
        }

        private async void GoToListMeasuresCommandExecute(object obj)
        {
            await Setter.SetCurrentViewModelAsync(new ListMeasuresVM());
        }

        private async void GoToListRecipesCommandExecute(object obj)
        {
            await Setter.SetCurrentViewModelAsync(new ListRecipesVM());
        }
    }
}
