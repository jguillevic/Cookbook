using System;

namespace Cookbook.Entity.Recipe
{
    public class RecipeFilter
    {
        public string Name { get; set; }

        public Cost Cost { get; set; }

        public Difficulty Difficulty { get; set; }

        public Feature Feature { get; set; }

        public RecipeKind RecipeKind { get; set; }

        public Season Season { get; set; }

        public int? PreparationTime { get; set; }

        public int? CookingTime { get; set; }
    }
}