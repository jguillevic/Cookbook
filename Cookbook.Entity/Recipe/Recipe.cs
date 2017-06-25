using System;
using System.Collections.Generic;

namespace Cookbook.Entity.Recipe
{
    public class Recipe
    {
        public Recipe()
        {
            Instructions = new List<RecipeInstruction>();
            Ingredients = new List<RecipeIngredient>();
            SeasonIds = new List<Guid>();
            FeatureIds = new List<Guid>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<RecipeInstruction> Instructions { get; set; }
        public int PreparationTime { get; set; }
        public int CookingTime { get; set; }
        public List<Guid> SeasonIds { get; set; }
        public Guid CostId { get; set; }
        public Guid DifficultyId { get; set; }
        public Guid RecipeKindId { get; set; }
        public List<Guid> FeatureIds { get; set; }
        public List<RecipeIngredient> Ingredients { get; set; }
        public string ExternalUrl { get; set; }
        public Guid? UserId { get; set; }
    }
}