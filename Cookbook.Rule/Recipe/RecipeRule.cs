using System;
using System.Collections.Generic;

namespace Cookbook.Rule.Recipe
{
    using Entity.Recipe;

    public static class RecipeRule
    {
        public static Recipe GetInitializedRecipe()
        {
            var recipe = new Recipe();

            recipe.Id = Guid.NewGuid();

            recipe.Instructions = new List<RecipeInstruction>();
            recipe.Ingredients = new List<RecipeIngredient>();

            return recipe;
        }
    }
}
