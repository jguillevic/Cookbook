using Cookbook.DAL.Recipe;
using Cookbook.Entity.Recipe;
using System.Collections.Generic;

namespace Cookbook.BLL.Recipe
{
    public class IngredientBLL
    {
        private IngredientDAL _ingredientDAL;

        public IngredientBLL()
        {
            _ingredientDAL = new IngredientDAL();
        }

        public List<Ingredient> Load()
        {
            return _ingredientDAL.Load();
        }

        public void Add(IEnumerable<Ingredient> ingredients)
        {
            _ingredientDAL.Add(ingredients);
        }

        public void Update(IEnumerable<Ingredient> ingredients)
        {
            _ingredientDAL.Update(ingredients);
        }
    }
}
