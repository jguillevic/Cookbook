using Cookbook.Entity.Recipe;
using System;

namespace Cookbook.Rule.Recipe
{
    public static class IngredientRule
    {
        public static Ingredient GetDefault()
        {
            var ingredient = new Ingredient();

            ingredient.Id = Guid.NewGuid();

            return ingredient;
        }
    }
}
