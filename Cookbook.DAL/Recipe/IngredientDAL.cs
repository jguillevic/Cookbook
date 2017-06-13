using Cookbook.DAL.Database;
using Cookbook.Entity.Recipe;
using System.Collections.Generic;
using System.Data;
using Tools.DAL.Database;
using Tools.DAL.QueryBuilder;

namespace Cookbook.DAL.Recipe
{
    public class IngredientDAL : DbDAL<SqlConnectionProvider>
    {
        public List<Ingredient> Load()
        {
            var sqb = new SelectQueryBuilder();

            sqb.AddQueriedField(IngredientTableDescription.Id);
            sqb.AddQueriedField(IngredientTableDescription.Name);
            sqb.AddFrom(IngredientTableDescription.TableName);

            var ingredients = sqb.Read<Ingredient, List<Ingredient>>(DefaultConnectProvider, GetIngredientFromIDataRecord);

            return ingredients;
        }

        private Ingredient GetIngredientFromIDataRecord(IDataRecord dataRecord)
        {
            var ingredient = new Ingredient();

            ingredient.Id = dataRecord.GetGuid(IngredientTableDescription.Id);
            ingredient.Name = dataRecord.GetString(IngredientTableDescription.Name);

            return ingredient;
        }

        public void Add(IEnumerable<Ingredient> ingredients)
        {
            var iqb = new InsertQueryBuilder();

            iqb.SetTableName(IngredientTableDescription.TableName);

            iqb.AddInsertFields(new List<string> { IngredientTableDescription.Id, IngredientTableDescription.Name });
            iqb.AddInsertValues(GetIngredientValues(ingredients));

            iqb.Execute(DefaultConnectProvider);
        }

        public void Update(IEnumerable<Ingredient> ingredients)
        {
            var uqb = new UpdateQueryBuilder();

            uqb.SetTableName(IngredientTableDescription.TableName);

            uqb.AddSettedFields(
                new List<string> { IngredientTableDescription.Id }
                , new List<string> { IngredientTableDescription.Id, IngredientTableDescription.Name }
                , GetIngredientValues(ingredients));

            uqb.Execute(DefaultConnectProvider);
        }

        private List<List<object>> GetIngredientValues(IEnumerable<Ingredient> ingredients)
        {
            var values = new List<List<object>>();
            List<object> value;

            foreach (var ingredient in ingredients)
            {
                value = new List<object>();

                value.Add(ingredient.Id);
                value.Add(ingredient.Name);

                values.Add(value);
            }

            return values;
        }
    }
}