using Cookbook.Entity.Recipe;
using System;
using Tools.UI.ViewData;

namespace Cookbook.UI.ViewData.Recipe
{
    public class RecipeIngredientVD : ViewDataBase
    {
        public Guid RecipeId { get; set; }

        public int Order { get; set; }

        private Guid _ingredientId;
        public Guid IngredientId
        {
            get { return _ingredientId; }
            set
            {
                if (_ingredientId != value)
                {
                    _ingredientId = value;
                    OnPropertyChanged("IngredientId");
                }
            }
        }

        private Guid _measureId;
        public Guid MeasureId
        {
            get { return _measureId; }
            set
            {
                if (_measureId != value)
                {
                    _measureId = value;
                    OnPropertyChanged("MeasureId");
                }
            }
        }

        private decimal _amount;
        public decimal Amount
        {
            get { return _amount; }
            set
            {
                if (_amount != value)
                {
                    _amount = value;
                    OnPropertyChanged("Amount");
                }
            }
        }

        public RecipeIngredientVD() : base() { }

        public RecipeIngredientVD(RecipeIngredient recipeIngredient)
        {
            SetFromEntity(recipeIngredient);
        }

        public void SetFromEntity(RecipeIngredient recipeIngredient)
        {
            RecipeId = recipeIngredient.RecipeId;
            Order = recipeIngredient.Order;
            IngredientId = recipeIngredient.IngredientId;
            MeasureId = recipeIngredient.MeasureId;
            Amount = recipeIngredient.Amount;
        }

        public RecipeIngredient GetEntity(int personNumber)
        {
            var recipeIngredient = new RecipeIngredient();

            recipeIngredient.RecipeId = RecipeId;
            recipeIngredient.Order = Order;
            recipeIngredient.IngredientId = IngredientId;
            recipeIngredient.MeasureId = MeasureId;
            recipeIngredient.Amount = Amount / personNumber;

            return recipeIngredient;
        }
    }
}
