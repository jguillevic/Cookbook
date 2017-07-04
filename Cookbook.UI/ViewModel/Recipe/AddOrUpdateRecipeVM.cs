using Cookbook.Rule.Recipe;
using Cookbook.ServiceClient.Recipe;
using Cookbook.UI.ViewData.Recipe;
using System;
using System.Collections.Generic;
using Tools.UI.Command;
using Tools.UI.ViewModel;

namespace Cookbook.UI.ViewModel.Recipe
{
    using Cookbook.Entity.Recipe;

    public class AddOrUpdateRecipeVM : PageViewModel
    {
        private Guid _recipeId;

        private bool IsAdding
        {
            get
            {
                return _recipeId == Guid.Empty;
            }
        }

        public RecipeVD Recipe { get; set; }

        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand UndoCommand { get; set; }

        public AddOrUpdateRecipeVM() : this(Guid.Empty) { }

        public AddOrUpdateRecipeVM(Guid recipeId) : base()
        {
            _recipeId = recipeId;

            Recipe = new RecipeVD();

            SaveCommand = new DelegateCommand(SaveCommandExecute);
            UndoCommand = new DelegateCommand(UndoCommandExecute);
        }

        public async override void Populate()
        {
            if (IsAdding)
            {
                Recipe.SetFromEntity(RecipeRule.GetDefault());
            }
            else
            {
                //var recipes = await RecipeServiceClient.LoadAsync(filter);
                //Recipe.SetFromEntity(recipes[0]);
            }
        }

        private async void SaveCommandExecute(object obj)
        {
            if (IsAdding)
                await RecipeServiceClient.AddAsync(new List<Recipe> { Recipe.GetEntity() });
            else
                await RecipeServiceClient.UpdateAsync(new List<Recipe> { Recipe.GetEntity() });
        }

        private void UndoCommandExecute(object obj)
        {
            Setter.SetCurrentViewModel(new ListRecipesVM());
        }
    }
}
