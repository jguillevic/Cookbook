using Cookbook.DAL.Recipe;
using Cookbook.Entity.Recipe;
using System.Collections.Generic;

namespace Cookbook.BLL.Recipe
{
    public class IngredientKindBLL
    {
        private IngredientKindDAL _ingredientKindDAL;

        public IngredientKindBLL()
        {
            _ingredientKindDAL = new IngredientKindDAL();
        }

        public List<IngredientKind> Load(List<string> fields)
        {
            return _ingredientKindDAL.Load(fields);
        }
    }
}
