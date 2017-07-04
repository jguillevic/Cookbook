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

        private void GoToListIngredientsCommandExecute(object obj)
        {
            Setter.SetCurrentViewModel(new ListIngredientsVM());
        }

        private void GoToListMeasuresCommandExecute(object obj)
        {
            Setter.SetCurrentViewModel(new ListMeasuresVM());
        }

        private void GoToListRecipesCommandExecute(object obj)
        {
            Setter.SetCurrentViewModel(new ListRecipesVM());
        }
    }
}
