using Cookbook.Entity.Recipe;
using Cookbook.ServiceClient.Recipe;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace Cookbook.UI.View.Recipe
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class ListMeasuresPage : Page
    {
        public ListMeasuresPage()
        {
            this.InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            await LoadAsync();
        }

        private async Task LoadAsync()
        {
            var measures = new ObservableCollection<Measure>(await MeasureServiceClient.LoadAsync());

            xIngredientsListView.ItemsSource = measures;

            if (measures.Count > 0)
            {
                xIngredientsListView.SelectedIndex = 0;
            }
        }
    }
}
