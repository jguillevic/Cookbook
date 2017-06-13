using Cookbook.UWP.Common;
using Cookbook.UWP.RecipeServiceReference;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

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

        private Season _season;
        public Season Season
        {
            get { return _season; }
            set
            {
                if (_season != value)
                {
                    _season = value;
                    OnPropertyChanged("IsWinter");
                    OnPropertyChanged("IsSpring");
                    OnPropertyChanged("IsSummer");
                    OnPropertyChanged("IsAutumn");
                }
            }
        }
        
        public bool IsWinter
        {
            get { return Season.HasFlag(Season.Winter); }
            set
            {
                if (value)
                {
                    Season |= Season.Winter;
                }
                else
                {
                    Season ^= Season.Winter;
                }
            }
        }

        public bool IsSpring
        {
            get { return Season.HasFlag(Season.Spring); }
            set
            {
                if (value)
                {
                    Season |= Season.Spring;
                }
                else
                {
                    Season ^= Season.Spring;
                }
            }
        }

        public bool IsSummer
        {
            get { return Season.HasFlag(Season.Summer); }
            set
            {
                if (value)
                {
                    Season |= Season.Summer;
                }
                else
                {
                    Season ^= Season.Summer;
                }
            }
        }

        public bool IsAutumn
        {
            get { return Season.HasFlag(Season.Autumn); }
            set
            {
                if (value)
                {
                    Season |= Season.Autumn;
                }
                else
                {
                    Season ^= Season.Autumn;
                }
            }
        }

        private Feature _feature;
        public Feature Feature
        {
            get { return _feature; }
            set
            {
                if (_feature != value)
                {
                    _feature = value;
                    OnPropertyChanged("IsVegetarian");
                    OnPropertyChanged("IsFestive");
                }
            }
        }

        public bool IsVegetarian
        {
            get { return Feature.HasFlag(Feature.Vegetarian); }
            set
            {
                if (value)
                {
                    Feature |= Feature.Vegetarian;
                }
                else
                {
                    Feature ^= Feature.Vegetarian;
                }
            }
        }

        public bool IsFestive
        {
            get { return Feature.HasFlag(Feature.Festive); }
            set
            {
                if (value)
                {
                    Feature |= Feature.Festive;
                }
                else
                {
                    Feature ^= Feature.Festive;
                }
            }
        }

        private VisualCost _cost;
        public VisualCost Cost
        {
            get { return _cost; }
            set
            {
                if (_cost != value)
                {
                    _cost = value;
                    OnPropertyChanged("Cost");
                }
            }
        }

        private VisualDifficulty _difficulty;
        public VisualDifficulty Difficulty
        {
            get { return _difficulty; }
            set
            {
                if (_difficulty != value)
                {
                    _difficulty = value;
                    OnPropertyChanged("Difficulty");
                }
            }
        }

        private VisualRecipeKind _recipeKind;
        public VisualRecipeKind RecipeKind
        {
            get { return _recipeKind; }
            set
            {
                if (_recipeKind != value)
                {
                    _recipeKind = value;
                    OnPropertyChanged("RecipeKind");
                }
            }
        }

        public ObservableCollection<RecipeInstruction> Instructions { get; set; }

        public ObservableCollection<RecipeIngredient> Ingredients { get; set; }

        public int InstructionCurrentOrder { get; set; }

        public int IngredientCurrentOrder { get; set; }

        public ObservableCollection<VisualCost> Costs { get; set; }

        public ObservableCollection<VisualDifficulty> Difficulties { get; set; }

        public ObservableCollection<VisualRecipeKind> RecipeKinds { get; set; }

        public AddOrUpdateRecipeVM(RecipeServiceReference.Recipe recipe)
        {
            Costs = new ObservableCollection<VisualCost>(CostDataProvider.Costs);
            Difficulties = new ObservableCollection<VisualDifficulty>(DifficultyDataProvider.Difficulties);
            RecipeKinds = new ObservableCollection<VisualRecipeKind>(RecipeKindDataProvider.RecipeKinds);

            Id = recipe.Id;
            Name = recipe.Name;
            Description = recipe.Description;
            Cost = CostDataProvider.GetVisualCost(recipe.Cost);
            Difficulty = DifficultyDataProvider.GetVisualDifficulty(recipe.Difficulty);
            RecipeKind = RecipeKindDataProvider.GetVisualRecipeKind(recipe.RecipeKind);
            Season = recipe.Season;
            Feature = Feature;
            PreparationTime = recipe.PreparationTime;
            CookingTime = recipe.CookingTime;

            Instructions = new ObservableCollection<RecipeInstruction>(recipe.Instructions);
            if (recipe.Instructions.Count > 0)
            {
                InstructionCurrentOrder = recipe.Instructions.Max(item => item.Order);
            }
            else
            {
                InstructionCurrentOrder = 1;
            }

            Ingredients = new ObservableCollection<RecipeIngredient>(recipe.Ingredients);
            if (recipe.Ingredients.Count > 0)
            {
                IngredientCurrentOrder = recipe.Ingredients.Max(item => item.Order);
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
                , Cost = Cost.Cost
                , Difficulty = Difficulty.Difficulty
                , RecipeKind = RecipeKind.RecipeKind
                , CookingTime = CookingTime
                , PreparationTime = PreparationTime
                , Feature = Feature
                , Season = Season
                , Instructions = new List<RecipeInstruction>(Instructions)
                , Ingredients = new List<RecipeIngredient>(Ingredients)
            };
        }
    }
}
