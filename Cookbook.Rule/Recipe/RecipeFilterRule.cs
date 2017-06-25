using Cookbook.Entity.Recipe;
using System;
using System.Collections.Generic;

namespace Cookbook.Rule.Recipe
{
    public class RecipeFilterRule
    {
        public static RecipeFilter GetDefaultRecipeFilter()
        {
            var filter = new RecipeFilter();

            return filter;
        }
    }
}
