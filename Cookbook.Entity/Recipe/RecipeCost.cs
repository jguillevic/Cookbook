using System;

namespace Cookbook.Entity.Recipe
{
    public class RecipeCost
    {
        public Guid RecipeId { get; set; }

        public Cost Cost { get; set; }
    }
}
