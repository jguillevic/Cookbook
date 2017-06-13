using Cookbook.UWP.Engine;
using Cookbook.UWP.RecipeServiceReference;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
    public sealed partial class ListRecipesPage : Page
    {
        public ListRecipesPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            await Load();
        }

        private async Task Load()
        {
            var client = new RecipeServiceClient();

            var recipes = new ObservableCollection<RecipeServiceReference.Recipe>(await client.LoadAsync(new RecipeFilter()));

            xRecipesListView.ItemsSource = recipes;

            if (recipes.Count > 0)
            {
                xRecipesListView.SelectedIndex = 0;
            }
        }

        private void xAdd_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddOrUpdateRecipePage));
        }

        private void xEdit_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddOrUpdateRecipePage), xRecipesListView.SelectedItem);
        }

        private async void xRefresh_Click(object sender, RoutedEventArgs e)
        {
            await Load();
        }

        private void xHome_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private async void xCrawl_Click(object sender, RoutedEventArgs e)
        {
            var recipes = await RecipeCrawler.CrawlRecipes(20);

            var client = new RecipeServiceClient();

            await client.AddAsync(recipes);
        }
    }
}
