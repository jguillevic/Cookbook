using Cookbook.DAL.Database;
using Cookbook.Entity.Recipe;
using System.Collections.Generic;
using System.Data;
using Tools.DAL.Database;
using Tools.DAL.QueryBuilder;
using Tools.DAL.QueryBuilder.Enum;

namespace Cookbook.DAL.Recipe
{
    public class IngredientSummaryDAL : DbDAL<SqlConnectionProvider>
    {
        public List<IngredientSummary> Load()
        {
            return Load(new IngredientFilter());
        }

        public List<IngredientSummary> Load(IngredientFilter filter)
        {
            var sqb = new SelectQueryBuilder();

            AddQueriedFields(sqb);

            AddFrom(sqb);

            if (filter.IdsToLoad.Count > 0)
                sqb.AddWhere(IngredientTableDescription.Id, Comparison.In, filter.IdsToLoad);

            var Ingredients = sqb.Read<IngredientSummary, List<IngredientSummary>>(DefaultConnectProvider, GetIngredientSummaryFromIDataRecord);

            return Ingredients;
        }

        private static void AddQueriedFields(SelectQueryBuilder sqb)
        {
            sqb.AddQueriedField(IngredientTableDescription.Id);
            sqb.AddQueriedField(IngredientTableDescription.Name);
        }

        private static void AddFrom(SelectQueryBuilder sqb)
        {
            sqb.AddFrom(IngredientTableDescription.TableName);
        }

        private IngredientSummary GetIngredientSummaryFromIDataRecord(IDataRecord dataRecord)
        {
            var Ingredient = new IngredientSummary();

            Ingredient.Id = dataRecord.GetGuid(IngredientTableDescription.Id);
            Ingredient.Name = dataRecord.GetString(IngredientTableDescription.Name);

            return Ingredient;
        }
    }
}
