using System;
using System.Collections.ObjectModel;
using Tools.UI.ViewData;

namespace Cookbook.UI.ViewData.Recipe
{
    using Entity.Recipe;

    public class RecipeVD : ViewDataBase
    {
        public Guid Id { get; private set; }

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

        private int _preparationTime;
        public int PreparationTime
        {
            get { return _preparationTime; }
            set
            {
                if (_preparationTime != value)
                {
                    _preparationTime = value;
                    OnPropertyChanged("PreprationTime");
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

        private string _externalUrl;
        public string ExternalUrl
        {
            get { return _externalUrl; }
            set
            {
                if (_externalUrl != value)
                {
                    _externalUrl = value;
                    OnPropertyChanged("ExternalUrl");
                }
            }
        }

        private Guid? _userId;
        public Guid? UserId
        {
            get { return _userId; }
            set
            {
                if (_userId != value)
                {
                    _userId = value;
                    OnPropertyChanged("UserId");
                }
            }
        }

        public ObservableCollection<Guid> SeasonIds { get; private set; }

        public ObservableCollection<Guid> FeatureIds { get; private set; }

        public ObservableCollection<RecipeInstructionVD> Instructions { get; private set; }

        public ObservableCollection<RecipeIngredientVD> Ingredients { get; private set; }

        public RecipeVD() : base()
        {
            SeasonIds = new ObservableCollection<Guid>();
            FeatureIds = new ObservableCollection<Guid>();
            Instructions = new ObservableCollection<RecipeInstructionVD>();
            Ingredients = new ObservableCollection<RecipeIngredientVD>();
        }

        public RecipeVD(Recipe recipe) : this()
        {
            SetFromEntity(recipe);
        }

        public void SetFromEntity(Recipe recipe)
        {
            Id = recipe.Id;
            Name = recipe.Name;
            PreparationTime = recipe.PreparationTime;
            CookingTime = recipe.CookingTime;
            Description = recipe.Description;
            CostId = recipe.CostId;
            DifficultyId = recipe.DifficultyId;
            RecipeKindId = recipe.RecipeKindId;
            UserId = recipe.UserId;
            ExternalUrl = recipe.ExternalUrl;
            recipe.SeasonIds.ForEach(item => SeasonIds.Add(item));
            recipe.FeatureIds.ForEach(item => FeatureIds.Add(item));
            recipe.Instructions.ForEach(item => Instructions.Add(new RecipeInstructionVD(item)));
            recipe.Ingredients.ForEach(item => Ingredients.Add(new RecipeIngredientVD(item)));
        }

        public Recipe GetEntity()
        {
            var recipe = new Recipe();

            recipe.Id = Id;
            recipe.Name = Name;
            recipe.PreparationTime = PreparationTime;
            recipe.CookingTime = CookingTime;
            recipe.Description = Description;
            recipe.CostId = CostId;
            recipe.DifficultyId = DifficultyId;
            recipe.RecipeKindId = RecipeKindId;
            recipe.UserId = UserId;
            recipe.ExternalUrl = ExternalUrl;
            recipe.SeasonIds.AddRange(SeasonIds);
            recipe.FeatureIds.AddRange(FeatureIds);

            foreach (var instruction in Instructions)
                recipe.Instructions.Add(instruction.GetEntity());

            foreach (var ingredient in Ingredients)
                recipe.Ingredients.Add(ingredient.GetEntity());

            return recipe;
        }
    }
}
