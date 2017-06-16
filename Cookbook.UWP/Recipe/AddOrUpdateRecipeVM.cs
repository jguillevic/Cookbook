using Cookbook.UWP.RecipeServiceReference;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Tools.UI.ViewModel;

namespace Cookbook.UWP.Recipe
{
    public class AddOrUpdateRecipeVM : ViewModelBase
    {
        private Guid _id;
        public Guid Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        private int _preparationTime;
        public int PreparationTime
        {
            get { return _preparationTime; }
            set
            {
                if (_preparationTime != value)
                {
                    _preparationTime = value;
                    OnPropertyChanged("PreparationTime");
                }
            }
        }

        private int _cookingTime;
        public int CookingTime
        {
            get { return _cookingTime; }
            set
            {
                if (_cookingTime != value)
                {
                    _cookingTime = value;
                    OnPropertyChanged("CookingTime");
                }
            }
        }

        private Guid _costId;
        public Guid CostId
        {
            get { return _costId; }
            set
            {
                if (_costId != value)
                {
                    _costId = value;
                    OnPropertyChanged("CostId");
                }
            }
        }

        private Guid _difficultyId;
        public Guid DifficultyId
        {
            get { return _difficultyId; }
            set
            {
                if (_difficultyId != value)
                {
                    _difficultyId = value;
                    OnPropertyChanged("DifficultyId");
                }
            }
        }

        private Guid _recipeKindId;
        public Guid RecipeKindId
        {
            get { return _recipeKindId; }
            set
            {
                if (_recipeKindId != value)
                {
                    _recipeKindId = value;
                    OnPropertyChanged("RecipeKindId");
                }
            }
        }

        public ObservableCollection<Guid> FeatureIds { get; set; }

        public ObservableCollection<Guid> SeasonIds { get; set; }

        public ObservableCollection<RecipeInstruction> Instructions { get; set; }

        public ObservableCollection<RecipeIngredient> Ingredients { get; set; }

        public int InstructionCurrentOrder { get; set; }

        public int IngredientCurrentOrder { get; set; }

        public AddOrUpdateRecipeVM(RecipeServiceReference.Recipe recipe)
        {
            Id = recipe.Id;
            Name = recipe.Name;
            Description = recipe.Description;
            CostId = recipe.CostId;
            DifficultyId = recipe.DifficultyId;
            RecipeKindId = recipe.RecipeKindId;
            SeasonIds = new ObservableCollection<Guid>(recipe.SeasonIds);
            FeatureIds = new ObservableCollection<Guid>(recipe.FeatureIds);
            PreparationTime = recipe.PreparationTime;
            CookingTime = recipe.CookingTime;

            Instructions = new ObservableCollection<RecipeInstruction>(recipe.Instructions);
            if (recipe.Instructions.Count > 0)
            {
                InstructionCurrentOrder = recipe.Instructions.Max(item => item.Order) + 1;
            }
            else
            {
                InstructionCurrentOrder = 1;
            }

            Ingredients = new ObservableCollection<RecipeIngredient>(recipe.Ingredients);
            if (recipe.Ingredients.Count > 0)
            {
                IngredientCurrentOrder = recipe.Ingredients.Max(item => item.Order) + 1;
            }
            else
            {
                IngredientCurrentOrder = 1;
            }
        }

        public RecipeServiceReference.Recipe GetRecipe()
        {
            return new RecipeServiceReference.Recipe {
                Id = Id
                , Name = Name
                , Description = Description
                , CostId = CostId
                , DifficultyId = DifficultyId
                , RecipeKindId = RecipeKindId
                , CookingTime = CookingTime
                , PreparationTime = PreparationTime
                , FeatureIds = new List<Guid>(FeatureIds)
                , SeasonIds = new List<Guid>(SeasonIds)
                , Instructions = new List<RecipeInstruction>(Instructions)
                , Ingredients = new List<RecipeIngredient>(Ingredients)
            };
        }

        public async Task Populate()
        {
            await CostDataProvider.Populate();
            await DifficultyDataProvider.Populate();
            await RecipeKindDataProvider.Populate();
            await FeatureDataProvider.Populate();
            await SeasonDataProvider.Populate();
        }
    }
}
