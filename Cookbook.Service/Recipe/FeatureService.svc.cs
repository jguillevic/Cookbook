using Cookbook.Contract.Recipe;
using Cookbook.DAL.Recipe;
using Cookbook.Entity.Recipe;
using System.Collections.Generic;

namespace Cookbook.Service.Recipe
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "FeatureService" à la fois dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez FeatureService.svc ou FeatureService.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class FeatureService : IFeatureService
    {
        private FeatureDAL _featureDAL;

        public FeatureService()
        {
            _featureDAL = new FeatureDAL();
        }

        public List<Feature> Load()
        {
            return _featureDAL.Load();
        }
    }
}
