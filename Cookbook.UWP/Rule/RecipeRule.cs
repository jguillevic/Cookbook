using Cookbook.UWP.RecipeServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookbook.UWP.Rule
{
    public static class RecipeRule
    {
        public static RecipeServiceReference.Recipe GetInitializedRecipe()
        {
            var recipe = new RecipeServiceReference.Recipe();

            recipe.Id = Guid.NewGuid();

            recipe.Instructions = new List<RecipeInstruction>();
            recipe.Ingredients = new List<RecipeIngredient>();

            return recipe;
        }
    }
}
