using Cookbook.Rule.Recipe;
using Cookbook.UI.ViewData.Recipe;
using System;
using Tools.UI.Command;
using Tools.UI.ViewModel;

namespace Cookbook.UI.ViewModel.Recipe
{
    public class AddOrUpdateIngredientVM : PageViewModel
    {
        private Guid _ingredientId;

        private bool IsAdding
        {
            get
            {
                return _ingredientId == Guid.Empty;
            }
        }

        public IngredientVD Ingredient { get; set; }

        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand UndoCommand { get; set; }

        public AddOrUpdateIngredientVM() : this(Guid.Empty) { }

        private AddOrUpdateIngredientVM(Guid ingredientId) : base()
        {
            _ingredientId = ingredientId;

            Ingredient = new IngredientVD();

            SaveCommand = new DelegateCommand(SaveCommandExecute);
            UndoCommand = new DelegateCommand(UndoCommandExecute);
        }

        public override void Populate()
        {
            if (IsAdding)
            {
                Ingredient.SetFromEntity(IngredientRule.GetDefault());
            }
            else
            {
                //var filter = new MeasureFilter();
                //filter.MeasureIds.Add(_measureId);
                //var measures = await MeasureServiceClient.LoadAsync(filter);
                //Measure.SetFromEntity(measures[0]);
            }
        }

        private void SaveCommandExecute(object obj)
        {
            //if (IsAdding)
            //    await MeasureServiceClient.AddAsync(new List<Measure> { Measure.GetEntity() });
            //else
            //    await MeasureServiceClient.UpdateAsync(new List<Measure> { Measure.GetEntity() });
        }

        private void UndoCommandExecute(object obj)
        {
            Setter.SetCurrentViewModel(new ListIngredientsVM());
        }
    }
}
