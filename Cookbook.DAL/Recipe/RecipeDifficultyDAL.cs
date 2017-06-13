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
    public class RecipeDifficultyDAL : DbDAL<SqlConnectionProvider>
    {
        internal HashSet<RecipeDifficulty> Load(IEnumerable<Guid> recipeIds)
        {
            var sqb = new SelectQueryBuilder();

            sqb.AddQueriedField(RecipeDifficultyTableDescription.RecipeId);
            sqb.AddQueriedField(RecipeDifficultyTableDescription.DifficultyId);

            sqb.AddFrom(RecipeDifficultyTableDescription.TableName);

            sqb.AddWhere(RecipeDifficultyTableDescription.RecipeId, Comparison.In, recipeIds);

            var recipeDifficultys = sqb.Read<RecipeDifficulty, HashSet<RecipeDifficulty>>(DefaultConnectProvider, GetRecipeDifficultyFromIDataRecord);

            return recipeDifficultys;
        }

        private RecipeDifficulty GetRecipeDifficultyFromIDataRecord(IDataRecord dataRecord)
        {
            var recipeDifficulty = new RecipeDifficulty();

            recipeDifficulty.RecipeId = dataRecord.GetGuid(RecipeDifficultyTableDescription.RecipeId);
            recipeDifficulty.Difficulty = (Difficulty)dataRecord.GetInt32(RecipeDifficultyTableDescription.DifficultyId);

            return recipeDifficulty;
        }

        internal void Add(IEnumerable<RecipeDifficulty> recipeDifficultys)
        {
            var iqb = new InsertQueryBuilder();

            iqb.SetTableName(RecipeDifficultyTableDescription.TableName);

            iqb.AddInsertFields(
                new List<string> {
                    RecipeDifficultyTableDescription.RecipeId
                    , RecipeDifficultyTableDescription.DifficultyId
                });
            iqb.AddInsertValues(GetRecipeDifficultyValues(recipeDifficultys));

            iqb.Execute(DefaultConnectProvider);
        }

        private List<List<object>> GetRecipeDifficultyValues(IEnumerable<RecipeDifficulty> recipeDifficultys)
        {
            var values = new List<List<object>>();
            List<object> value;

            foreach (var recipeDifficulty in recipeDifficultys)
            {
                value = new List<object>();

                value.Add(recipeDifficulty.RecipeId);
                value.Add((int)recipeDifficulty.Difficulty);

                values.Add(value);
            }

            return values;
        }

        internal void Delete(IEnumerable<Guid> recipeIds)
        {
            var dqb = new DeleteQueryBuilder();

            dqb.AddFrom(RecipeDifficultyTableDescription.TableName);
            dqb.AddWhere(RecipeDifficultyTableDescription.RecipeId, Comparison.In, recipeIds);

            dqb.Execute(DefaultConnectProvider);
        }
    }
}
