using System;
using System.Collections.Generic;

namespace Cookbook.Entity.Recipe
{
    public class RecipeFilter
    {
        public string Name { get; set; }
        public List<Guid> CostIds { get; set; }
        public List<Guid> DifficultyIds { get; set; }
        public List<Guid> FeatureIds { get; set; }
        public List<Guid> RecipeKindIds { get; set; }
        public List<Guid> SeasonIds { get; set; }
        public int? PreparationTime { get; set; }
        public int? CookingTime { get; set; }
        public List<Guid> IdsToLoad { get; set; }

        public RecipeFilter()
        {
            CostIds = new List<Guid>();
            DifficultyIds = new List<Guid>();
            RecipeKindIds = new List<Guid>();
            SeasonIds = new List<Guid>();
            FeatureIds = new List<Guid>();
            IdsToLoad = new List<Guid>();
        }
    }
}