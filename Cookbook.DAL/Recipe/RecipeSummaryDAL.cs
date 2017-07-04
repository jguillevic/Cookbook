using Cookbook.DAL.Database;
using Cookbook.Entity.Recipe;
using System.Collections.Generic;
using System.Data;
using Tools.DAL.Database;
using Tools.DAL.QueryBuilder;
using Tools.DAL.QueryBuilder.Enum;

namespace Cookbook.DAL.Recipe
{
    public class RecipeSummaryDAL : DbDAL<SqlConnectionProvider>
    {
        public List<RecipeSummary> Load()
        {
            return Load(new RecipeFilter());
        }

        public List<RecipeSummary> Load(RecipeFilter filter)
        {
            var sqb = new SelectQueryBuilder();

            AddQueriedFields(sqb);

            AddFrom(sqb);

            if (filter.IdsToLoad.Count > 0)
                sqb.AddWhere(RecipeTableDescription.Id, Comparison.In, filter.IdsToLoad);

            var Recipes = sqb.Read<RecipeSummary, List<RecipeSummary>>(DefaultConnectProvider, GetRecipeSummaryFromIDataRecord);

            return Recipes;
        }

        private static void AddQueriedFields(SelectQueryBuilder sqb)
        {
            sqb.AddQueriedField(RecipeTableDescription.Id);
            sqb.AddQueriedField(RecipeTableDescription.Name);
        }

        private static void AddFrom(SelectQueryBuilder sqb)
        {
            sqb.AddFrom(RecipeTableDescription.TableName);
        }

        private RecipeSummary GetRecipeSummaryFromIDataRecord(IDataRecord dataRecord)
        {
            var Recipe = new RecipeSummary();

            Recipe.Id = dataRecord.GetGuid(RecipeTableDescription.Id);
            Recipe.Name = dataRecord.GetString(RecipeTableDescription.Name);

            return Recipe;
        }
    }
}
