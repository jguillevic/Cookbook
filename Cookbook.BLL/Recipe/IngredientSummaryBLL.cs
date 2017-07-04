using Cookbook.DAL.Recipe;
using Cookbook.Entity.Recipe;
using System.Collections.Generic;

namespace Cookbook.BLL.Recipe
{
    public class IngredientSummaryBLL
    {
        private IngredientSummaryDAL _ingredientSummaryDAL;

        public IngredientSummaryBLL()
        {
            _ingredientSummaryDAL = new IngredientSummaryDAL();
        }

        public List<IngredientSummary> Load()
        {
            return _ingredientSummaryDAL.Load();
        }

        public List<IngredientSummary> Load(IngredientFilter filter)
        {
            return _ingredientSummaryDAL.Load(filter);
        }
    }
}
