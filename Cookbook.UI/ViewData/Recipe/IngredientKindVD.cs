using Cookbook.Entity.Recipe;
using System;
using Tools.UI.ViewData;

namespace Cookbook.UI.ViewData.Recipe
{
    public class IngredientKindVD : ViewDataBase
    {
        public Guid Id { get; set; }

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

        public IngredientKindVD() : base() { }

        public IngredientKindVD(IngredientKind ingredientKind) : this()
        {
            SetFromEntity(ingredientKind);
        }

        public void SetFromEntity(IngredientKind ingredientKind)
        {
            Id = ingredientKind.Id;
            Name = ingredientKind.Name;
            Code = ingredientKind.Code;
        }

        public IngredientKind GetEntity()
        {
            var ingredientKind = new IngredientKind();

            ingredientKind.Id = Id;
            ingredientKind.Name = Name;
            ingredientKind.Code = Code;

            return ingredientKind;
        }
    }
}
