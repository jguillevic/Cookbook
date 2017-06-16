using Cookbook.Contract.Recipe;
using Cookbook.DAL.Recipe;
using Cookbook.Entity.Recipe;
using System.Collections.Generic;

namespace Cookbook.Service.Recipe
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "SeasonService" à la fois dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez SeasonService.svc ou SeasonService.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class SeasonService : ISeasonService
    {
        private SeasonDAL _seasonDAL;

        public SeasonService()
        {
            _seasonDAL = new SeasonDAL();
        }

        public List<Season> Load()
        {
            return _seasonDAL.Load();
        }
    }
}
