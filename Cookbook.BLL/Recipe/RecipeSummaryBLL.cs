using Cookbook.DAL.Recipe;
using Cookbook.Entity.Recipe;
using System.Collections.Generic;

namespace Cookbook.BLL.Recipe
{
    public class RecipeSummaryBLL
    {
        private RecipeSummaryDAL _recipeSummaryDAL;

        public RecipeSummaryBLL()
        {
            _recipeSummaryDAL = new RecipeSummaryDAL();
        }

        public List<RecipeSummary> Load()
        {
            return _recipeSummaryDAL.Load();
        }

        public List<RecipeSummary> Load(RecipeFilter filter)
        {
            return _recipeSummaryDAL.Load(filter);
        }
    }
}
