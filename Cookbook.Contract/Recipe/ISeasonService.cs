using Cookbook.Entity.Recipe;
using System.Collections.Generic;
using System.ServiceModel;

namespace Cookbook.Contract.Recipe
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "ISeasonService" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface ISeasonService
    {
        [OperationContract]
        List<Season> Load();
    }
}
