using Cookbook.Contract.Recipe;
using Cookbook.DAL.Recipe;
using Cookbook.Entity.Recipe;
using System.Collections.Generic;

namespace Cookbook.Service.Recipe
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "CostService" à la fois dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez CostService.svc ou CostService.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class CostService : ICostService
    {
        private CostDAL _costDAL;

        public CostService()
        {
            _costDAL = new CostDAL();
        }

        public List<Cost> Load()
        {
            return _costDAL.Load();
        }
    }
}
