using System;
using Tools.UI.ViewData;

namespace Cookbook.UI.ViewData.Recipe
{
    using Entity.Recipe;

    public class RecipeSummaryVD : ViewDataBase
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

        public RecipeSummaryVD(Recipe recipe)
        {
            Id = recipe.Id;
            Name = recipe.Name;
            PreparationTime = recipe.PreparationTime;
            CookingTime = recipe.CookingTime;
        }
    }
}
