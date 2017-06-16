using Cookbook.Entity.Recipe;
using System.Collections.Generic;
using System.ServiceModel;

namespace Cookbook.Contract.Recipe
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IFeatureService" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IFeatureService
    {
        [OperationContract]
        List<Feature> Load();
    }
}
