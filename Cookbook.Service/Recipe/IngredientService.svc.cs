using Cookbook.Contract.Recipe;
using Cookbook.DAL.Recipe;
using Cookbook.Entity.Recipe;
using System.Collections.Generic;

namespace Cookbook.Service.Recipe
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "IngredientService" à la fois dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez IngredientService.svc ou IngredientService.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class IngredientService : IIngredientService
    {
        private IngredientDAL _ingredientDAL;

        public IngredientService()
        {
            _ingredientDAL = new IngredientDAL();
        }

        public void Add(IEnumerable<Ingredient> ingredients)
        {
            _ingredientDAL.Add(ingredients);
        }

        public List<Ingredient> Load()
        {
            return _ingredientDAL.Load();
        }

        public void Update(IEnumerable<Ingredient> ingredients)
        {
            _ingredientDAL.Update(ingredients);
        }
    }
}
