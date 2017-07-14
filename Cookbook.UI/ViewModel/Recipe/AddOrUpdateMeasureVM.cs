using Cookbook.Entity.Recipe;
using Cookbook.Rule.Recipe;
using Cookbook.ServiceClient.Recipe;
using Cookbook.UI.ViewData.Recipe;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tools.UI.Command;
using Tools.UI.ViewModel;

namespace Cookbook.UI.ViewModel.Recipe
{
    public class AddOrUpdateMeasureVM : EntityViewModel<MeasureVD>
    {
        private Guid _measureId;

        private bool IsAdding
        {
            get
            {
                return _measureId == Guid.Empty;
            }
        }

        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand UndoCommand { get; set; }

        public AddOrUpdateMeasureVM() : this(Guid.Empty) { }

        public AddOrUpdateMeasureVM(Guid measureId) : base()
        {
            _measureId = measureId;

            SaveCommand = new DelegateCommand(SaveCommandExecute);
            UndoCommand = new DelegateCommand(UndoCommandExecute);
        }

        public async override Task PopulateAsync()
        {
            if (IsAdding)
            {
                Item.SetFromEntity(MeasureRule.GetDefault());
            }
            else
            {
                var filter = new MeasureFilter();
                filter.IdsToLoad.Add(_measureId);
                var measures = await MeasureServiceClient.LoadAsync(filter);
                Item.SetFromEntity(measures[0]);
            }
        }

        private async void SaveCommandExecute(object obj)
        {
            if (IsAdding)
                await MeasureServiceClient.AddAsync(new List<Measure> { Item.GetEntity() });
            else
                await MeasureServiceClient.UpdateAsync(new List<Measure> { Item.GetEntity() });

            await Setter.SetCurrentViewModelAsync(new ListMeasuresVM());
        }

        private async void UndoCommandExecute(object obj)
        {
            await Setter.SetCurrentViewModelAsync(new ListMeasuresVM());
        }
    }
}
