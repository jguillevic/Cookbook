using System;

namespace Cookbook.Entity.Recipe
{
    public class RecipeIngredient
    {
        public Guid RecipeId { get; set; }

        public Guid IngredientId { get; set; }

        public Guid MeasureId { get; set; }

        public int Order { get; set; }

        public int Amount { get; set; }
    }
}