using Cookbook.Entity.Recipe;
using System;
using Tools.UI.ViewData;

namespace Cookbook.UI.ViewData.Recipe
{
    public class IngredientVD : ViewDataBase
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

        private string _code;
        public string Code
        {
            get { return _code; }
            set
            {
                if (_code != value)
                {
                    _code = value;
                    OnPropertyChanged("Code");
                }
            }
        }

        private Guid _ingredientKindId;
        public Guid IngredientKindId
        {
            get { return _ingredientKindId; }
            set
            {
                if (_ingredientKindId != value)
                {
                    _ingredientKindId = value;
                    OnPropertyChanged("IngredientKindId");
                }
            }
        }

        private decimal _calories;
        public decimal Calories
        {
            get { return _calories; }
            set
            {
                if (_calories != value)
                {
                    _calories = value;
                    OnPropertyChanged("Calories");
                }
            }
        }

        private decimal _protein;
        public decimal Protein
        {
            get { return _protein; }
            set
            {
                if (_protein != value)
                {
                    _protein = value;
                    OnPropertyChanged("Protein");
                }
            }
        }

        private decimal _carbohydrate;
        public decimal Carbohydrate
        {
            get { return _carbohydrate; }
            set
            {
                if (_carbohydrate != value)
                {
                    _carbohydrate = value;
                    OnPropertyChanged("Carbohydrate");
                }
            }
        }

        private decimal _lipid;
        public decimal Lipid
        {
            get { return _lipid; }
            set
            {
                if (_lipid != value)
                {
                    _lipid = value;
                    OnPropertyChanged("Lipid");
                }
            }
        }

        private decimal _water;
        public decimal Water
        {
            get { return _water; }
            set
            {
                if (_water != value)
                {
                    _water = value;
                    OnPropertyChanged("Water");
                }
            }
        }

        private decimal _fiber;
        public decimal Fiber
        {
            get { return _fiber; }
            set
            {
                if (_fiber != value)
                {
                    _fiber = value;
                    OnPropertyChanged("Fiber");
                }
            }
        }

        public IngredientVD() : base() { }

        public IngredientVD(Ingredient ingredient) : this()
        {
            SetFromEntity(ingredient);
        }

        public void SetFromEntity(Ingredient ingredient)
        {
            Id = ingredient.Id;
            Name = ingredient.Name;
            Code = ingredient.Code;
            IngredientKindId = ingredient.IngredientKindId;
            Calories = ingredient.Calories;
            Protein = ingredient.Protein;
            Carbohydrate = ingredient.Carbohydrate;
            Lipid = ingredient.Lipid;
            Water = ingredient.Water;
            Fiber = ingredient.Fiber;
        }

        public Ingredient GetEntity()
        {
            var ingredient = new Ingredient();

            ingredient.Id = Id;
            ingredient.Name = Name;
            ingredient.Code = Code;
            ingredient.IngredientKindId = IngredientKindId;
            ingredient.Calories = Calories;
            ingredient.Protein = Protein;
            ingredient.Carbohydrate = Carbohydrate;
            ingredient.Lipid = Lipid;
            ingredient.Water = Water;
            ingredient.Fiber = Fiber;

            return ingredient;
        }
    }
}
