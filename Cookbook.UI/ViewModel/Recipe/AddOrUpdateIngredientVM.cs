using Cookbook.Entity.Recipe;
using Cookbook.Rule.Recipe;
using Cookbook.ServiceClient.Recipe;
using Cookbook.UI.DataProvider;
using Cookbook.UI.DataProvider.Recipe;
using Cookbook.UI.ViewData.Recipe;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tools.UI.Command;
using Tools.UI.Common;
using Tools.UI.ViewModel;

namespace Cookbook.UI.ViewModel.Recipe
{
    public class AddOrUpdateIngredientVM : EntityViewModel<IngredientVD>
    {
        private Guid _ingredientId;

        private bool IsAdding
        {
            get
            {
                return _ingredientId == Guid.Empty;
            }
        }

        public ObservableRangeCollection<IngredientKindVD> IngredientKinds { get; private set; }

        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand UndoCommand { get; set; }

        public AddOrUpdateIngredientVM() : this(Guid.Empty) { }

        public AddOrUpdateIngredientVM(Guid ingredientId) : base()
        {
            _ingredientId = ingredientId;

            IngredientKinds = new ObservableRangeCollection<IngredientKindVD>();

            SaveCommand = new DelegateCommand(SaveCommandExecute);
            UndoCommand = new DelegateCommand(UndoCommandExecute);
        }

        public override void Initialize()
        {
            InitializeIngredientKinds();
        }

        private void InitializeIngredientKinds()
        {
            IngredientKinds.Clear();

            var ingredientKinds = ((IngredientKindDataProvider)DataProviderManager.GetDataProvider(DataProviderKeys.IngredientKindDataProviderKey)).Items;
            foreach (var ingredientKind in ingredientKinds)
                IngredientKinds.Add(new IngredientKindVD(ingredientKind));
        }

        public async override Task PopulateAsync()
        {
            if (IsAdding)
            {
                Item.SetFromEntity(IngredientRule.GetDefault());
            }
            else
            {
                var filter = new IngredientFilter();
                filter.IdsToLoad.Add(_ingredientId);
                var ingredients = await IngredientServiceClient.LoadAsync(filter);
                Item.SetFromEntity(ingredients[0]);
            }
        }

        private async void SaveCommandExecute(object obj)
        {
            if (IsAdding)
                await IngredientServiceClient.AddAsync(new List<Ingredient> { Item.GetEntity() });
            else
                await IngredientServiceClient.UpdateAsync(new List<Ingredient> { Item.GetEntity() });

            await Setter.SetCurrentViewModelAsync(new ListIngredientsVM());
        }

        private async void UndoCommandExecute(object obj)
        {
            await Setter.SetCurrentViewModelAsync(new ListIngredientsVM());
        }
    }
}
