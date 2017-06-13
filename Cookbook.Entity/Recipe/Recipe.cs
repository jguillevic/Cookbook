using System;
using System.Collections.Generic;

namespace Cookbook.Entity.Recipe
{
    public class Recipe
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<RecipeInstruction> Instructions { get; set; }

        public int PreparationTime { get; set; }

        public int CookingTime { get; set; }

        public Season Season { get; set; }

        public Cost Cost { get; set; }

        public Difficulty Difficulty { get; set; }

        public RecipeKind RecipeKind { get; set; }

        public Feature Feature { get; set; }

        public List<RecipeIngredient> Ingredients { get; set; }

        public string ExternalUrl { get; set; }

        public Guid? UserId { get; set; }
    }
}