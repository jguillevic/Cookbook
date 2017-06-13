using System;

namespace Cookbook.Entity.Recipe
{
    public class RecipeFeature
    {
        public Guid RecipeId { get; set; }

        public Feature Feature { get; set; }
    }
}
