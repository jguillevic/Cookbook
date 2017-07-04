using Cookbook.Entity.Recipe;
using Tools.UI.ViewData;

namespace Cookbook.UI.ViewData.Recipe
{
    public class IngredientVD : ViewDataBase
    {
        public IngredientVD() : base() { }

        public IngredientVD(Ingredient ingredient) : this()
        {
            SetFromEntity(ingredient);
        }

        public void SetFromEntity(Ingredient ingredient)
        {

        }

        public Ingredient GetEntity()
        {
            var ingredient = new Ingredient();

            return ingredient;
        }
    }
}
