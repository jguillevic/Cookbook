using Cookbook.UWP.IngredientServiceReference;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, voir la page http://go.microsoft.com/fwlink/?LinkId=234238

namespace Cookbook.UWP.Recipe
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class ListIngredientsPage : Page
    {
        public ListIngredientsPage()
        {
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            await Load();
        }

        private async Task Load()
        {
            var client = new IngredientServiceClient();

            var ingredients = new ObservableCollection<Ingredient>(await client.LoadAsync());

            xIngredientsListView.ItemsSource = ingredients;

            if (ingredients.Count > 0)
            {
                xIngredientsListView.SelectedIndex = 0;
            }
        }

        private void xHome_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private async void xRefresh_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await Load();
        }

        private void xAdd_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddOrUpdateIngredientPage));
        }

        private void xEdit_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddOrUpdateIngredientPage), xIngredientsListView.SelectedItem);
        }
    }
}
