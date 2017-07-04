using Cookbook.Entity.Recipe;
using Cookbook.Rule.Recipe;
using Cookbook.ServiceClient.Recipe;
using Cookbook.UI.ViewData.Recipe;
using System;
using System.Collections.Generic;
using Tools.UI.Command;
using Tools.UI.ViewModel;

namespace Cookbook.UI.ViewModel.Recipe
{
    public class AddOrUpdateMeasureVM : PageViewModel
    {
        private Guid _measureId;

        private bool IsAdding
        {
            get
            {
                return _measureId == Guid.Empty;
            }
        }

        public MeasureVD Measure { get; set; }

        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand UndoCommand { get; set; }

        public AddOrUpdateMeasureVM() : this(Guid.Empty) { }

        public AddOrUpdateMeasureVM(Guid recipeId) : base()
        {
            _measureId = recipeId;

            Measure = new MeasureVD();

            SaveCommand = new DelegateCommand(SaveCommandExecute);
            UndoCommand = new DelegateCommand(UndoCommandExecute);
        }

        public async override void Populate()
        {
            if (IsAdding)
            {
                Measure.SetFromEntity(MeasureRule.GetDefault());
            }
            else
            {
                var measures = await MeasureServiceClient.LoadAsync(new List<Guid> { _measureId });
                Measure.SetFromEntity(measures[0]);
            }
        }

        private async void SaveCommandExecute(object obj)
        {
            if (IsAdding)
                await MeasureServiceClient.AddAsync(new List<Measure> { Measure.GetEntity() });
            else
                await MeasureServiceClient.UpdateAsync(new List<Measure> { Measure.GetEntity() });

            Setter.SetCurrentViewModel(new ListMeasuresVM());
        }

        private void UndoCommandExecute(object obj)
        {
            Setter.SetCurrentViewModel(new ListMeasuresVM());
        }
    }
}
