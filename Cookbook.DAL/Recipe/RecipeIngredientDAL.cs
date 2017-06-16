using Cookbook.DAL.Database;
using Cookbook.Entity.Recipe;
using System;
using System.Collections.Generic;
using System.Data;
using Tools.DAL.Database;
using Tools.DAL.QueryBuilder;
using Tools.DAL.QueryBuilder.Enum;

namespace Cookbook.DAL.Recipe
{
    public class RecipeIngredientDAL : DbDAL<SqlConnectionProvider>
    {
        internal HashSet<RecipeIngredient> Load(IEnumerable<Guid> recipeIds)
        {
            var sqb = new SelectQueryBuilder();

            sqb.AddQueriedField(RecipeIngredientTableDescription.RecipeId);
            sqb.AddQueriedField(RecipeIngredientTableDescription.IngredientId);
            sqb.AddQueriedField(RecipeIngredientTableDescription.MeasureId);
            sqb.AddQueriedField(RecipeIngredientTableDescription.Order);
            sqb.AddQueriedField(RecipeIngredientTableDescription.Amount);

            sqb.AddFrom(RecipeIngredientTableDescription.TableName);

            sqb.AddWhere(RecipeIngredientTableDescription.RecipeId, Comparison.In, recipeIds);

            var recipeIngredients = sqb.Read<RecipeIngredient, HashSet<RecipeIngredient>>(DefaultConnectProvider, GetRecipeIngredientFromIDataRecord);

            return recipeIngredients;
        }

        private RecipeIngredient GetRecipeIngredientFromIDataRecord(IDataRecord dataRecord)
        {
            var recipeIngredient = new RecipeIngredient();

            recipeIngredient.RecipeId = dataRecord.GetGuid(RecipeIngredientTableDescription.RecipeId);
            recipeIngredient.IngredientId = dataRecord.GetGuid(RecipeIngredientTableDescription.IngredientId);
            recipeIngredient.MeasureId = dataRecord.GetGuid(RecipeIngredientTableDescription.MeasureId);
            recipeIngredient.Order = dataRecord.GetInt32(RecipeIngredientTableDescription.Order);
            recipeIngredient.Amount = dataRecord.GetDecimal(RecipeIngredientTableDescription.Amount);

            return recipeIngredient;
        }

        internal void Add(IEnumerable<RecipeIngredient> recipeIngredients)
        {
            var iqb = new InsertQueryBuilder();

            iqb.SetTableName(RecipeIngredientTableDescription.TableName);

            iqb.AddInsertFields(
                new List<string> {
                    RecipeIngredientTableDescription.RecipeId
                    , RecipeIngredientTableDescription.IngredientId
                    , RecipeIngredientTableDescription.MeasureId
                    , RecipeIngredientTableDescription.Order
                    , RecipeIngredientTableDescription.Amount
                });
            iqb.AddInsertValues(GetRecipeIngredientValues(recipeIngredients));

            iqb.Execute(DefaultConnectProvider);
        }

        private List<List<object>> GetRecipeIngredientValues(IEnumerable<RecipeIngredient> recipeIngredients)
        {
            var values = new List<List<object>>();
            List<object> value;

            foreach (var recipeIngredient in recipeIngredients)
            {
                value = new List<object>();

                value.Add(recipeIngredient.RecipeId);
                value.Add(recipeIngredient.IngredientId);
                value.Add(recipeIngredient.MeasureId);
                value.Add(recipeIngredient.Order);
                value.Add(recipeIngredient.Amount);

                values.Add(value);
            }

            return values;
        }

        internal void Delete(IEnumerable<Guid> recipeIds)
        {
            var dqb = new DeleteQueryBuilder();

            dqb.AddFrom(RecipeIngredientTableDescription.TableName);
            dqb.AddWhere(RecipeIngredientTableDescription.RecipeId, Comparison.In, recipeIds);

            dqb.Execute(DefaultConnectProvider);
        }
    }
}