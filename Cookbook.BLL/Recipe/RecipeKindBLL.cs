using Cookbook.DAL.Recipe;
using Cookbook.Entity.Recipe;
using System.Collections.Generic;

namespace Cookbook.BLL.Recipe
{
    public class RecipeKindBLL
    {
        private RecipeKindDAL _recipeKindDAL;

        public RecipeKindBLL()
        {
            _recipeKindDAL = new RecipeKindDAL();
        }

        public List<RecipeKind> Load(List<string> fields)
        {
            return _recipeKindDAL.Load(fields);
        }
    }
}
