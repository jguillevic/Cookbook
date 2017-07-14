using Cookbook.ServiceClient.Recipe;
using Cookbook.UI.ViewData.Recipe;
using Cookbook.UI.ViewModel.Home;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Tools.UI.Command;
using Tools.UI.Common;
using Tools.UI.ViewModel;
using static Cookbook.Entity.Recipe.RecipeEntityDescriptions;

namespace Cookbook.UI.ViewModel.Recipe
{
    public class ListMeasuresVM : PageViewModel
    {
        public ObservableRangeCollection<MeasureSummaryVD> Measures { get; private set; }

        private int _selectedIndex;
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                if (_selectedIndex != value)
                {
                    _selectedIndex = value;
                    OnPropertyChanged("SelectedIndex");
                }
            }
        }

        public DelegateCommand AddCommand { get; set; }
        public DelegateCommand UpdateCommand { get; set; }
        public DelegateCommand RefreshCommand { get; set; }
        public DelegateCommand GoToHomeCommand { get; set; }

        public ListMeasuresVM()
        {
            Measures = new ObservableRangeCollection<MeasureSummaryVD>();

            AddCommand = new DelegateCommand(AddCommandExecute);
            UpdateCommand = new DelegateCommand(UpdateCommandExecute);
            RefreshCommand = new DelegateCommand(RefreshCommandExecute);
            GoToHomeCommand = new DelegateCommand(GoToHomeCommandExecute);
        }

        public override async Task PopulateAsync()
        {
            Measures.Clear();

            var measures = await MeasureServiceClient.LoadAsync(new List<string> { MeasureEntityDescription.Id, MeasureEntityDescription.Name });

            var measuresVD = new List<MeasureSummaryVD>(measures.Count);
            measures.ForEach(item => measuresVD.Add(new MeasureSummaryVD(item)));

            Measures.AddRange(measuresVD);
        }

        private async void AddCommandExecute(object obj)
        {
            await Setter.SetCurrentViewModelAsync(new AddOrUpdateMeasureVM());
        }

        private async void UpdateCommandExecute(object obj)
        {
            await Setter.SetCurrentViewModelAsync(new AddOrUpdateMeasureVM(Measures[SelectedIndex].Id));
        }

        private async void RefreshCommandExecute(object obj)
        {
            await PopulateAsync();
        }

        private async void GoToHomeCommandExecute(object obj)
        {
            await Setter.SetCurrentViewModelAsync(new HomeViewModel());
        }
    }
}
