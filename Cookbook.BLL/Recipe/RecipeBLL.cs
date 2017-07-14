using Cookbook.DAL.Recipe;
using System.Collections.Generic;

namespace Cookbook.BLL.Recipe
{
    using Entity.Recipe;

    public class RecipeBLL
    {
        private RecipeDAL _recipeDAL;

        public RecipeBLL()
        {
            _recipeDAL = new RecipeDAL();
        }

        public List<Recipe> Load(RecipeFilter filter, List<string> fields)
        {
            return _recipeDAL.Load(filter, fields);
        }

        public void Add(IEnumerable<Recipe> recipes)
        {
            _recipeDAL.Add(recipes);
        }

        public void Update(IEnumerable<Recipe> recipes)
        {
            _recipeDAL.Update(recipes);
        }
    }
}
