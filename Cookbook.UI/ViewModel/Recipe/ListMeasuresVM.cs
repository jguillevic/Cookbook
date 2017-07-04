using Cookbook.ServiceClient.Recipe;
using Cookbook.UI.ViewData.Recipe;
using Cookbook.UI.ViewModel.Home;
using System.Collections.ObjectModel;
using Tools.UI.Command;
using Tools.UI.ViewModel;

namespace Cookbook.UI.ViewModel.Recipe
{
    public class ListMeasuresVM : PageViewModel
    {
        public ObservableCollection<MeasureSummaryVD> Measures { get; private set; }

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
            Measures = new ObservableCollection<MeasureSummaryVD>();

            AddCommand = new DelegateCommand(AddCommandExecute);
            UpdateCommand = new DelegateCommand(UpdateCommandExecute);
            RefreshCommand = new DelegateCommand(RefreshCommandExecute);
            GoToHomeCommand = new DelegateCommand(GoToHomeCommandExecute);
        }

        public override async void Populate()
        {
            Measures.Clear();
            var measures = await MeasureServiceClient.LoadAsync();         
            measures.ForEach(item => Measures.Add(new MeasureSummaryVD(item)));
        }

        private void AddCommandExecute(object obj)
        {
            Setter.SetCurrentViewModel(new AddOrUpdateMeasureVM());
        }

        private void UpdateCommandExecute(object obj)
        {
            Setter.SetCurrentViewModel(new AddOrUpdateMeasureVM(Measures[SelectedIndex].Id));
        }

        private void RefreshCommandExecute(object obj)
        {
            Populate();
        }

        private void GoToHomeCommandExecute(object obj)
        {
            Setter.SetCurrentViewModel(new HomeViewModel());
        }
    }
}
