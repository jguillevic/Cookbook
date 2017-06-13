using Cookbook.DAL.Recipe;
using System.Collections.Generic;

namespace Cookbook.Service.Recipe
{
    using Entity.Recipe;

    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "RecipeService" à la fois dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez RecipeService.svc ou RecipeService.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class RecipeService : IRecipeService
    {
        private RecipeDAL _recipeDAL;

        public RecipeService()
        {
            _recipeDAL = new RecipeDAL();
        }

        public void Add(IEnumerable<Recipe> recipes)
        {
            _recipeDAL.Add(recipes);
        }

        public List<Recipe> Load(RecipeFilter recipeFilter)
        {
            return _recipeDAL.Load(recipeFilter);
        }

        public void Update(IEnumerable<Recipe> recipes)
        {
            _recipeDAL.Update(recipes);
        }
    }
}
