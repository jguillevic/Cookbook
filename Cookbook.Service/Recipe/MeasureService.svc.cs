using Cookbook.Contract.Recipe;
using Cookbook.DAL.Recipe;
using Cookbook.Entity.Recipe;
using System.Collections.Generic;

namespace Cookbook.Service.Recipe
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "MeasureService" à la fois dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez MeasureService.svc ou MeasureService.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class MeasureService : IMeasureService
    {
        private MeasureDAL _measureDAL;

        public MeasureService()
        {
            _measureDAL = new MeasureDAL();
        }

        public void Add(IEnumerable<Measure> measures)
        {
            _measureDAL.Add(measures);
        }

        public List<Measure> Load()
        {
            return _measureDAL.Load();
        }

        public void Update(IEnumerable<Measure> measures)
        {
            _measureDAL.Update(measures);
        }
    }
}
