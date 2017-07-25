using Cookbook.ServiceClient.Recipe;
using Cookbook.UI.ViewData.Recipe;
using Cookbook.UI.ViewModel.Home;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tools.UI.Command;
using Tools.UI.Common;
using Tools.UI.ViewModel;
using static Cookbook.Entity.Recipe.RecipeEntityDescriptions;

namespace Cookbook.UI.ViewModel.Recipe
{
    public class ListMeasuresVM : ListViewModel<MeasureSummaryVD>
    {
        public DelegateCommand AddCommand { get; set; }
        public DelegateCommand UpdateCommand { get; set; }
        public DelegateCommand RefreshCommand { get; set; }
        public DelegateCommand GoToHomeCommand { get; set; }

        public ListMeasuresVM() : base()
        {
            AddCommand = new DelegateCommand(AddCommandExecute);
            UpdateCommand = new DelegateCommand(UpdateCommandExecute);
            RefreshCommand = new DelegateCommand(RefreshCommandExecute);
            GoToHomeCommand = new DelegateCommand(GoToHomeCommandExecute);
        }

        public override async Task PopulateAsync()
        {
            Items.Clear();

            var measures = await MeasureServiceClient.LoadAsync(new List<string> { MeasureEntityDescription.Id, MeasureEntityDescription.Name });

            var measuresVD = new List<MeasureSummaryVD>(measures.Count);
            measures.ForEach(item => measuresVD.Add(new MeasureSummaryVD(item)));

            Items.AddRange(measuresVD);
        }

        private async void AddCommandExecute(object obj)
        {
            await Setter.SetCurrentViewModelAsync(new AddOrUpdateMeasureVM());
        }

        private async void UpdateCommandExecute(object obj)
        {
            await Setter.SetCurrentViewModelAsync(new AddOrUpdateMeasureVM(Items[SelectedIndex].Id));
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
