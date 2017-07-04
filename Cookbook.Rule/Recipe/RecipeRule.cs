using System;

namespace Cookbook.Rule.Recipe
{
    using Entity.Recipe;

    public static class RecipeRule
    {
        public static Recipe GetDefault()
        {
            var recipe = new Recipe();

            recipe.Id = Guid.NewGuid();

            return recipe;
        }
    }
}
