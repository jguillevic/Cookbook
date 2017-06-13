using Cookbook.UWP.IngredientServiceReference;
using System;
using System.Collections.Generic;
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
    public sealed partial class AddOrUpdateIngredientPage : Page
    {
        private AddOrUpdateIngredientVM _vm;
        private bool _isAdding;

        public AddOrUpdateIngredientPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var ingredient = e.Parameter as Ingredient;

            if (ingredient != null)
            {
                _isAdding = false;
                _vm = new AddOrUpdateIngredientVM(ingredient);
            }
            else
            {
                _isAdding = true;
                ingredient = new Ingredient { Id = Guid.NewGuid() };
                _vm = new AddOrUpdateIngredientVM(ingredient);
            }

            xGrid.DataContext = _vm;
        }

        private async void xSave_Click(object sender, RoutedEventArgs e)
        {
            var client = new IngredientServiceClient();

            var ingredients = new List<Ingredient> { _vm.GetIngredient() };

            if (_isAdding)
            {
                await client.AddAsync(ingredients);
            }
            else
            {
                await client.UpdateAsync(ingredients);
            }

            Frame.GoBack();
        }

        private void xUndo_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
