using Cookbook.DAL.Database;
using Cookbook.Entity.Recipe;
using System.Collections.Generic;
using System.Data;
using Tools.DAL.Database;
using Tools.DAL.QueryBuilder;
using Tools.DAL.QueryBuilder.Enum;
using static Cookbook.Entity.Recipe.RecipeEntityDescriptions;

namespace Cookbook.DAL.Recipe
{
    public class IngredientDAL : DbDAL<SqlConnectionProvider>
    {
        private List<string> _fields;

        public List<Ingredient> Load(IngredientFilter filter, List<string> fields)
        {
            _fields = fields;

            var sqb = new SelectQueryBuilder();

            AddQueriedFields(sqb);

            sqb.AddFrom(IngredientTableDescription.TableName);

            if (filter.IdsToLoad.Count > 0)
                sqb.AddWhere(IngredientTableDescription.Id, Comparison.In, filter.IdsToLoad);

            sqb.AddOrderBy(IngredientTableDescription.Name, Sorting.Ascending);

            var ingredients = sqb.Read<Ingredient, List<Ingredient>>(DefaultConnectProvider, GetIngredientFromIDataRecord);

            return ingredients;
        }

        private void AddQueriedFields(SelectQueryBuilder sqb)
        {
            if (_fields.Contains(IngredientEntityDescription.Id.ToLower()))
                sqb.AddQueriedField(IngredientTableDescription.Id);
            if (_fields.Contains(IngredientEntityDescription.Name.ToLower()))
                sqb.AddQueriedField(IngredientTableDescription.Name);
            if (_fields.Contains(IngredientEntityDescription.Code.ToLower()))
                sqb.AddQueriedField(IngredientTableDescription.Code);
            if (_fields.Contains(IngredientEntityDescription.IngredientKindId.ToLower()))
                sqb.AddQueriedField(IngredientTableDescription.IngredientKindId);
            if (_fields.Contains(IngredientEntityDescription.Calories.ToLower()))
                sqb.AddQueriedField(IngredientTableDescription.Calories);
            if (_fields.Contains(IngredientEntityDescription.Protein.ToLower()))
                sqb.AddQueriedField(IngredientTableDescription.Protein);
            if (_fields.Contains(IngredientEntityDescription.Carbohydrate.ToLower()))
                sqb.AddQueriedField(IngredientTableDescription.Carbohydrate);
            if (_fields.Contains(IngredientEntityDescription.Lipid.ToLower()))
                sqb.AddQueriedField(IngredientTableDescription.Lipid);
            if (_fields.Contains(IngredientEntityDescription.Water.ToLower()))
                sqb.AddQueriedField(IngredientTableDescription.Water);
            if (_fields.Contains(IngredientEntityDescription.Fiber.ToLower()))
                sqb.AddQueriedField(IngredientTableDescription.Fiber);
        }

        private Ingredient GetIngredientFromIDataRecord(IDataRecord dataRecord)
        {
            var ingredient = new Ingredient();

            if (_fields.Contains(IngredientEntityDescription.Id.ToLower()))
                ingredient.Id = dataRecord.GetGuid(IngredientTableDescription.Id);
            if (_fields.Contains(IngredientEntityDescription.Name.ToLower()))
                ingredient.Name = dataRecord.GetString(IngredientTableDescription.Name);
            if (_fields.Contains(IngredientEntityDescription.Code.ToLower()))
                ingredient.Code = dataRecord.GetString(IngredientTableDescription.Code);
            if (_fields.Contains(IngredientEntityDescription.IngredientKindId.ToLower()))
                ingredient.IngredientKindId = dataRecord.GetGuid(IngredientTableDescription.IngredientKindId);
            if (_fields.Contains(IngredientEntityDescription.Calories.ToLower()))
                ingredient.Calories = dataRecord.GetDecimal(IngredientTableDescription.Calories);
            if (_fields.Contains(IngredientEntityDescription.Protein.ToLower()))
                ingredient.Protein = dataRecord.GetDecimal(IngredientTableDescription.Protein);
            if (_fields.Contains(IngredientEntityDescription.Carbohydrate.ToLower()))
                ingredient.Carbohydrate = dataRecord.GetDecimal(IngredientTableDescription.Carbohydrate);
            if (_fields.Contains(IngredientEntityDescription.Lipid.ToLower()))
                ingredient.Lipid = dataRecord.GetDecimal(IngredientTableDescription.Lipid);
            if (_fields.Contains(IngredientEntityDescription.Water.ToLower()))
                ingredient.Water = dataRecord.GetDecimal(IngredientTableDescription.Water);
            if (_fields.Contains(IngredientEntityDescription.Fiber.ToLower()))
                ingredient.Fiber = dataRecord.GetDecimal(IngredientTableDescription.Fiber);

            return ingredient;
        }

        public void Add(IEnumerable<Ingredient> ingredients)
        {
            var iqb = new InsertQueryBuilder();

            iqb.SetTableName(IngredientTableDescription.TableName);

            iqb.AddInsertFields(
                new List<string> {
                    IngredientTableDescription.Id
                    , IngredientTableDescription.Name
                    , IngredientTableDescription.Code
                    , IngredientTableDescription.IngredientKindId
                    , IngredientTableDescription.Calories
                    , IngredientTableDescription.Protein
                    , IngredientTableDescription.Carbohydrate
                    , IngredientTableDescription.Lipid
                    , IngredientTableDescription.Water
                    , IngredientTableDescription.Fiber});
            iqb.AddInsertValues(GetIngredientValues(ingredients));

            iqb.Execute(DefaultConnectProvider);
        }

        public void Update(IEnumerable<Ingredient> ingredients)
        {
            var uqb = new UpdateQueryBuilder();

            uqb.SetTableName(IngredientTableDescription.TableName);

            uqb.AddSettedFields(
                new List<string> { IngredientTableDescription.Id }
                , new List<string> {
                    IngredientTableDescription.Id
                    , IngredientTableDescription.Name
                    , IngredientTableDescription.Code
                    , IngredientTableDescription.IngredientKindId
                    , IngredientTableDescription.Calories
                    , IngredientTableDescription.Protein
                    , IngredientTableDescription.Carbohydrate
                    , IngredientTableDescription.Lipid
                    , IngredientTableDescription.Water
                    , IngredientTableDescription.Fiber}
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
                value.Add(ingredient.Code);
                value.Add(ingredient.IngredientKindId);
                value.Add(ingredient.Calories);
                value.Add(ingredient.Protein);
                value.Add(ingredient.Carbohydrate);
                value.Add(ingredient.Lipid);
                value.Add(ingredient.Water);
                value.Add(ingredient.Fiber);

                values.Add(value);
            }

            return values;
        }
    }
}