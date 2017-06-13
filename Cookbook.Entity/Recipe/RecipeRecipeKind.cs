using System;

namespace Cookbook.Entity.Recipe
{
    public class RecipeRecipeKind
    {
        public Guid RecipeId { get; set; }

        public RecipeKind RecipeKind { get; set; }
    }
}
