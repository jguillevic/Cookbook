using Cookbook.Entity.Recipe;
using System.Collections.Generic;
using System.ServiceModel;

namespace Cookbook.Service.Recipe
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IMeasureService" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IMeasureService
    {
        [OperationContract]
        List<Measure> Load();

        [OperationContract]
        void Add(IEnumerable<Measure> measures);

        [OperationContract]
        void Update(IEnumerable<Measure> measures);
    }
}
