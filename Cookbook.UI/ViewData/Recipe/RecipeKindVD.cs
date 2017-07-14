using Cookbook.Entity.Recipe;
using System;
using Tools.UI.ViewData;

namespace Cookbook.UI.ViewData.Recipe
{
    public class RecipeKindVD : ViewDataBase
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

        public RecipeKindVD() : base() { }

        public RecipeKindVD(RecipeKind recipeKind) : this()
        {
            SetFromEntity(recipeKind);
        }

        public void SetFromEntity(RecipeKind recipeKind)
        {
            Id = recipeKind.Id;
            Name = recipeKind.Name;
            Code = recipeKind.Code;
        }

        public RecipeKind GetEntity()
        {
            var recipeKind = new RecipeKind();

            recipeKind.Id = Id;
            recipeKind.Name = Name;
            recipeKind.Code = Code;

            return recipeKind;
        }
    }
}
