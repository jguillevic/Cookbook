using Cookbook.Contract.Recipe;
using Cookbook.DAL.Recipe;
using Cookbook.Entity.Recipe;
using System.Collections.Generic;

namespace Cookbook.Service.Recipe
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "RecipeKindService" à la fois dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez RecipeKindService.svc ou RecipeKindService.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class RecipeKindService : IRecipeKindService
    {
        private RecipeKindDAL _recipeKindDAL;

        public RecipeKindService()
        {
            _recipeKindDAL = new RecipeKindDAL();
        }

        public List<RecipeKind> Load()
        {
            return _recipeKindDAL.Load();
        }
    }
}
