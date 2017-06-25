using Cookbook.ServiceClient.Recipe;
using Cookbook.UI.ViewData.Recipe;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Tools.UI.ViewModel;

namespace Cookbook.UI.ViewModel.Recipe
{
    public class ListMeasuresVM : ViewModelBase
    {
        public ObservableCollection<MeasureSummaryVD> Measures { get; private set; }

        public ListMeasuresVM()
        {
            Measures = new ObservableCollection<MeasureSummaryVD>();
        }

        public async Task Populate()
        {
            var measures = await MeasureServiceClient.LoadAsync();
            measures.Clear();
            measures.ForEach(item => Measures.Add(new MeasureSummaryVD(item)));
        }
    }
}
