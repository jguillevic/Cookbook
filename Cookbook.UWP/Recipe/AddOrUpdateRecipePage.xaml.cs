using Cookbook.UWP.RecipeServiceReference;
using Cookbook.UWP.Rule;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
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

        protected override void OnNavigatedTo(NavigationEventArgs e)
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
                recipe = RecipeRule.GetInitializedRecipe();
                _vm = new AddOrUpdateRecipeVM(recipe);
            }

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
            _vm.Instructions.Add(new RecipeInstruction { RecipeId = _vm.Id, Order = _vm.IngredientCurrentOrder++ });
        }

        private void xDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (xInstructListView.SelectedItem != null)
            {
                _vm.Instructions.Remove((RecipeInstruction)xInstructListView.SelectedItem);
            }
        }
    }
}
