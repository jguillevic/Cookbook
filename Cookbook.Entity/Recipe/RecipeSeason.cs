using System;

namespace Cookbook.Entity.Recipe
{
    public class RecipeSeason
    {
        public Guid RecipeId { get; set; }
        public Guid SeasonId { get; set; }
    }
}
