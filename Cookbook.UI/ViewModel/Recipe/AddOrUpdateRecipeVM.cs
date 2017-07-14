using Cookbook.Rule.Recipe;
using Cookbook.ServiceClient.Recipe;
using Cookbook.UI.DataProvider;
using Cookbook.UI.DataProvider.Recipe;
using Cookbook.UI.ViewData.Recipe;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tools.UI.Command;
using Tools.UI.ViewModel;

namespace Cookbook.UI.ViewModel.Recipe
{
    using Cookbook.Entity.Recipe;

    public class AddOrUpdateRecipeVM : EntityViewModel<RecipeVD>
    {
        private Guid _recipeId;

        private bool IsAdding
        {
            get
            {
                return _recipeId == Guid.Empty;
            }
        }

        public List<CostVD> Costs { get; private set; }
        public List<DifficultyVD> Difficulties { get; private set; }
        public List<RecipeKindVD> RecipeKinds { get; private set; }

        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand UndoCommand { get; set; }

        public AddOrUpdateRecipeVM() : this(Guid.Empty) { }

        public AddOrUpdateRecipeVM(Guid recipeId) : base()
        {
            _recipeId = recipeId;

            Costs = new List<CostVD>();
            Difficulties = new List<DifficultyVD>();
            RecipeKinds = new List<RecipeKindVD>();

            SaveCommand = new DelegateCommand(SaveCommandExecute);
            UndoCommand = new DelegateCommand(UndoCommandExecute);
        }

        public override void Initialize()
        {
            InitializeCosts();
            InitializeDifficulties();
            InitializeRecipeKinds();
        }

        private void InitializeCosts()
        {
            var costs = ((CostDataProvider)DataProviderManager.GetDataProvider(DataProviderKeys.CostDataProviderKey)).Items;
            foreach (var cost in costs)
                Costs.Add(new CostVD(cost));
        }

        private void InitializeDifficulties()
        {
            var difficulties = ((DifficultyDataProvider)DataProviderManager.GetDataProvider(DataProviderKeys.DifficultyDataProviderKey)).Items;
            foreach (var difficulty in difficulties)
                Difficulties.Add(new DifficultyVD(difficulty));
        }

        private void InitializeRecipeKinds()
        {
            var recipeKinds = ((RecipeKindDataProvider)DataProviderManager.GetDataProvider(DataProviderKeys.RecipeKindDataProviderKey)).Items;
            foreach (var recipeKind in recipeKinds)
                RecipeKinds.Add(new RecipeKindVD(recipeKind));
        }

        public async override Task PopulateAsync()
        {
            if (IsAdding)
            {
                Item.SetFromEntity(RecipeRule.GetDefault());
            }
            else
            {
                var filter = new RecipeFilter();
                filter.IdsToLoad.Add(_recipeId);
                var recipes = await RecipeServiceClient.LoadAsync(filter);
                Item.SetFromEntity(recipes[0]);
            }
        }

        private async void SaveCommandExecute(object obj)
        {
            if (IsAdding)
                await RecipeServiceClient.AddAsync(new List<Recipe> { Item.GetEntity() });
            else
                await RecipeServiceClient.UpdateAsync(new List<Recipe> { Item.GetEntity() });

            await Setter.SetCurrentViewModelAsync(new ListRecipesVM());
        }

        private async void UndoCommandExecute(object obj)
        {
            await Setter.SetCurrentViewModelAsync(new ListRecipesVM());
        }
    }
}
