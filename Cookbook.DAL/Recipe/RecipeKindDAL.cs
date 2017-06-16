using Cookbook.DAL.Database;
using Cookbook.Entity.Recipe;
using System.Collections.Generic;
using System.Data;
using Tools.DAL.Database;
using Tools.DAL.QueryBuilder;

namespace Cookbook.DAL.Recipe
{
    public class RecipeKindDAL : DbDAL<SqlConnectionProvider>
    {
        public List<RecipeKind> Load()
        {
            var sqb = new SelectQueryBuilder();

            sqb.AddQueriedField(RecipeKindTableDescription.Id);
            sqb.AddQueriedField(RecipeKindTableDescription.Name);
            sqb.AddQueriedField(RecipeKindTableDescription.Code);

            sqb.AddFrom(RecipeKindTableDescription.TableName);

            return sqb.Read<RecipeKind, List<RecipeKind>>(DefaultConnectProvider, GetRecipeKindFromIDataRecord);
        }

        private RecipeKind GetRecipeKindFromIDataRecord(IDataRecord dataRecord)
        {
            var recipeKind = new RecipeKind();

            recipeKind.Id = dataRecord.GetGuid(RecipeKindTableDescription.Id);
            recipeKind.Name = dataRecord.GetString(RecipeKindTableDescription.Name);
            recipeKind.Code = dataRecord.GetString(RecipeKindTableDescription.Code);

            return recipeKind;
        }
    }
}
