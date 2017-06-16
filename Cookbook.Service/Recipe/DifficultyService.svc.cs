using Cookbook.Contract.Recipe;
using Cookbook.DAL.Recipe;
using Cookbook.Entity.Recipe;
using System.Collections.Generic;

namespace Cookbook.Service.Recipe
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "DifficultyService" à la fois dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez DifficultyService.svc ou DifficultyService.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class DifficultyService : IDifficultyService
    {
        private DifficultyDAL _difficultyDAL;

        public DifficultyService()
        {
            _difficultyDAL = new DifficultyDAL();
        }

        public List<Difficulty> Load()
        {
            return _difficultyDAL.Load();
        }
    }
}
