using Cookbook.Entity.Recipe;
using System.Collections.Generic;
using System.ServiceModel;

namespace Cookbook.Service.Recipe
{
    using Entity.Recipe;

    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IRecipeService" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IRecipeService
    {
        [OperationContract]
        List<Recipe> Load(RecipeFilter recipeFilter);

        [OperationContract]
        void Add(IEnumerable<Recipe> recipes);

        [OperationContract]
        void Update(IEnumerable<Recipe> recipes);
    }
}
