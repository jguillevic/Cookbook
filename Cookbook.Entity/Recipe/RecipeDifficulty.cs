using System;

namespace Cookbook.Entity.Recipe
{
    public class RecipeDifficulty
    {
        public Guid RecipeId { get; set; }

        public Difficulty Difficulty { get; set; }
    }
}
