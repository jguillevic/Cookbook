using System;

namespace Cookbook.Entity.Recipe
{
    public class RecipeSeason
    {
        public Guid RecipeId { get; set; }

        public Season Season { get; set; }
    }
}
