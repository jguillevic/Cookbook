using Cookbook.Rule.Recipe;
using Cookbook.ServiceClient.Recipe;
using Cookbook.UI.DataProvider;
using Cookbook.UI.DataProvider.Recipe;
using Cookbook.UI.ViewData.Recipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tools.UI.Command;
using Tools.UI.ViewModel;
using static Cookbook.Entity.Recipe.RecipeEntityDescriptions;

namespace Cookbook.UI.ViewModel.Recipe
{
    using Cookbook.Entity.Recipe;
    using Tools.UI.Common;

    public class AddOrUpdateRecipeVM : EntityViewModel<RecipeVD>
    {
        private Guid _recipeId;
        private int _currentIngredientOrder;
        private int _currentInstructionOrder;
        private bool _disposed;

        private bool IsAdding
        {
            get
            {
                return _recipeId == Guid.Empty;
            }
        }

        private int _oldPersonNumber;
        private int _personNumber;
        public int PersonNumber
        {
            get { return _personNumber; }
            set
            {
                if (_personNumber != value)
                {
                    _personNumber = value;
                    OnPropertyChanged("PersonNumber");
                }
            }
        }

        public ObservableRangeCollection<CostVD> Costs { get; private set; }
        public ObservableRangeCollection<DifficultyVD> Difficulties { get; private set; }
        public ObservableRangeCollection<RecipeKindVD> RecipeKinds { get; private set; }
        public ObservableRangeCollection<SeasonVD> Seasons { get; private set; }
        public ObservableRangeCollection<FeatureVD> Features { get; private set; }
        public ObservableRangeCollection<MeasureSummaryVD> Measures { get; private set; }
        public ObservableRangeCollection<IngredientSummaryVD> Ingredients { get; private set; }

        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand UndoCommand { get; set; }
        public DelegateCommand AddIngredientCommand { get; set; }
        public DelegateCommand RemoveIngredientCommand { get; set; }
        public DelegateCommand AddInstructionCommand { get; set; }
        public DelegateCommand RemoveInstructionCommand { get; set; }
        public DelegateCommand DecreasePersonNumberCommand { get; set; }
        public DelegateCommand IncreasePersonNumberCommand { get; set; }

        public AddOrUpdateRecipeVM() : this(Guid.Empty) { }

        public AddOrUpdateRecipeVM(Guid recipeId) : base()
        {
            _recipeId = recipeId;
            _oldPersonNumber = 1;
            _disposed = false;

            PropertyChanged += AddOrUpdateRecipeVMPropertyChanged;

            Costs = new ObservableRangeCollection<CostVD>();
            Difficulties = new ObservableRangeCollection<DifficultyVD>();
            RecipeKinds = new ObservableRangeCollection<RecipeKindVD>();
            Seasons = new ObservableRangeCollection<SeasonVD>();
            Features = new ObservableRangeCollection<FeatureVD>();
            Measures = new ObservableRangeCollection<MeasureSummaryVD>();
            Ingredients = new ObservableRangeCollection<IngredientSummaryVD>();

            SaveCommand = new DelegateCommand(SaveCommandExecute);
            UndoCommand = new DelegateCommand(UndoCommandExecute);
            AddIngredientCommand = new DelegateCommand(AddIngredientCommandExecute);
            RemoveIngredientCommand = new DelegateCommand(RemoveIngredientCommandExecute);
            AddInstructionCommand = new DelegateCommand(AddInstructionCommandExecute);
            RemoveInstructionCommand = new DelegateCommand(RemoveInstructionCommandExecute);
            DecreasePersonNumberCommand = new DelegateCommand(DecreasePersonNumberCommandExecute);
            IncreasePersonNumberCommand = new DelegateCommand(IncreasePersonNumberCommandExecute);
        }

        private void AddOrUpdateRecipeVMPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "PersonNumber")
            {
                foreach (var ingredient in Item.Ingredients)
                    ingredient.Amount = Math.Round(ingredient.Amount * PersonNumber / _oldPersonNumber, 5);

                _oldPersonNumber = PersonNumber;
            }
        }

        public override void Initialize()
        {
            InitializeCosts();
            InitializeDifficulties();
            InitializeRecipeKinds();
            InitializeSeasons();
            InitializeFeatures();
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

        private void InitializeSeasons()
        {
            var seasons = ((SeasonDataProvider)DataProviderManager.GetDataProvider(DataProviderKeys.SeasonDataProviderKey)).Items;
            foreach (var season in seasons)
                Seasons.Add(new SeasonVD(season));
        }

        private void InitializeFeatures()
        {
            var features = ((FeatureDataProvider)DataProviderManager.GetDataProvider(DataProviderKeys.FeatureDataProviderKey)).Items;
            foreach (var feature in features)
                Features.Add(new FeatureVD(feature));
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

            var measures = await MeasureServiceClient.LoadAsync(new List<string> { MeasureEntityDescription.Id, MeasureEntityDescription.Name });
            measures.ForEach(item => Measures.Add(new MeasureSummaryVD(item)));

            var ingredients = await IngredientServiceClient.LoadAsync(new List<string> { IngredientEntityDescription.Id, IngredientEntityDescription.Name });
            ingredients.ForEach(item => Ingredients.Add(new IngredientSummaryVD(item)));

            _currentIngredientOrder = Item.Ingredients.Count > 0 ? Item.Ingredients.Max(item => item.Order) : 0;
            _currentInstructionOrder = Item.Instructions.Count > 0 ? Item.Instructions.Max(item => item.Order) : 0;

            PersonNumber = 2;
        }

        public override void Dispose()
        {
            if (!_disposed)
            {
                PropertyChanged -= AddOrUpdateRecipeVMPropertyChanged;

                base.Dispose();

                _disposed = true;
            }
        }

        private async void SaveCommandExecute(object obj)
        {
            if (IsAdding)
                await RecipeServiceClient.AddAsync(new List<Recipe> { Item.GetEntity(PersonNumber) });
            else
                await RecipeServiceClient.UpdateAsync(new List<Recipe> { Item.GetEntity(PersonNumber) });

            await Setter.SetCurrentViewModelAsync(new ListRecipesVM());
        }

        private async void UndoCommandExecute(object obj)
        {
            await Setter.SetCurrentViewModelAsync(new ListRecipesVM());
        }

        private void AddIngredientCommandExecute(object obj)
        {
            var ingredient = new RecipeIngredientVD();
            ingredient.RecipeId = Item.Id;
            ingredient.Order = ++_currentIngredientOrder;

            Item.Ingredients.Add(ingredient);
        }

        private void RemoveIngredientCommandExecute(object obj)
        {
            Item.Ingredients.RemoveAt(Item.Ingredients.Count - 1);
        }

        private void AddInstructionCommandExecute(object obj)
        {
            var instruction = new RecipeInstructionVD();
            instruction.RecipeId = Item.Id;
            instruction.Order = ++_currentInstructionOrder;

            Item.Instructions.Add(instruction);
        }

        private void RemoveInstructionCommandExecute(object obj)
        {
            Item.Instructions.RemoveAt(Item.Instructions.Count - 1);
        }

        private void DecreasePersonNumberCommandExecute(object obj)
        {
            if (PersonNumber > 1)
                PersonNumber--;
        }

        private void IncreasePersonNumberCommandExecute(object obj)
        {
            PersonNumber++;
        }
    }
}
