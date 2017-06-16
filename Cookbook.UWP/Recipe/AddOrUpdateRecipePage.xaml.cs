using Cookbook.Rule.Recipe;
using Cookbook.UWP.RecipeServiceReference;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, voir la page http://go.microsoft.com/fwlink/?LinkId=234238

namespace Cookbook.UWP.Recipe
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class AddOrUpdateRecipePage : Page
    {
        private bool _isAdding;
        private AddOrUpdateRecipeVM _vm;

        public AddOrUpdateRecipePage()
        {
            this.InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var recipe = e.Parameter as RecipeServiceReference.Recipe;

            if (recipe != null)
            {
                _isAdding = false;
                _vm = new AddOrUpdateRecipeVM(recipe);
            }
            else
            {
                _isAdding = true;
                //recipe = RecipeRule.GetInitializedRecipe();
                recipe = new Recipe {
                    Id = Guid.NewGuid()
                    , FeatureIds = new List<Guid>()
                    , SeasonIds = new List<Guid>()
                    , Instructions = new List<RecipeInstruction>()
                    , Ingredients = new List<RecipeIngredient>()
                };
                _vm = new AddOrUpdateRecipeVM(recipe);
            }

            await _vm.Populate();

            xGrid.DataContext = _vm;
        }

        private async void xSave_Click(object sender, RoutedEventArgs e)
        {
            var client = new RecipeServiceClient();

            var recipes = new List<RecipeServiceReference.Recipe> { _vm.GetRecipe() };

            if (_isAdding)
            {
                await client.AddAsync(recipes);
            }
            else
            {
                await client.UpdateAsync(recipes);
            }

            Frame.GoBack();
        }

        private void xUndo_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void xAddButton_Click(object sender, RoutedEventArgs e)
        {
            _vm.Instructions.Add(new RecipeInstruction { RecipeId = _vm.Id, Order = _vm.InstructionCurrentOrder++ });
        }

        private void xDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            _vm.Instructions.RemoveAt(_vm.Instructions.Count - 1);
        }
    }
}
