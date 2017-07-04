using System;
using System.Collections.Generic;

namespace Cookbook.Entity.Recipe
{
    public class IngredientFilter
    {
        public List<Guid> IdsToLoad { get; set; }

        public IngredientFilter()
        {
            IdsToLoad = new List<Guid>();
        }
    }
}
