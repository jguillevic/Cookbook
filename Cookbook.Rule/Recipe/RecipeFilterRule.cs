using Cookbook.Entity.Recipe;

namespace Cookbook.Rule.Recipe
{
    public static class RecipeFilterRule
    {
        public static RecipeFilter GetDefault()
        {
            var filter = new RecipeFilter();

            return filter;
        }
    }
}
