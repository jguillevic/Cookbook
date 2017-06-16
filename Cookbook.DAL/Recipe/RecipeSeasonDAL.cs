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
    public class RecipeSeasonDAL : DbDAL<SqlConnectionProvider>
    {
        internal HashSet<RecipeSeason> Load(IEnumerable<Guid> recipeIds)
        {
            var sqb = new SelectQueryBuilder();

            sqb.AddQueriedField(RecipeSeasonTableDescription.RecipeId);
            sqb.AddQueriedField(RecipeSeasonTableDescription.SeasonId);

            sqb.AddFrom(RecipeSeasonTableDescription.TableName);

            sqb.AddWhere(RecipeSeasonTableDescription.RecipeId, Comparison.In, recipeIds);

            var recipeSeasons = sqb.Read<RecipeSeason, HashSet<RecipeSeason>>(DefaultConnectProvider, GetRecipeSeasonFromIDataRecord);

            return recipeSeasons;
        }

        private RecipeSeason GetRecipeSeasonFromIDataRecord(IDataRecord dataRecord)
        {
            var recipeSeason = new RecipeSeason();

            recipeSeason.RecipeId = dataRecord.GetGuid(RecipeSeasonTableDescription.RecipeId);
            recipeSeason.SeasonId = dataRecord.GetGuid(RecipeSeasonTableDescription.SeasonId);

            return recipeSeason;
        }

        internal void Add(IEnumerable<RecipeSeason> recipeSeasons)
        {
            var iqb = new InsertQueryBuilder();

            iqb.SetTableName(RecipeSeasonTableDescription.TableName);

            iqb.AddInsertFields(
                new List<string> {
                    RecipeSeasonTableDescription.RecipeId
                    , RecipeSeasonTableDescription.SeasonId
                });
            iqb.AddInsertValues(GetRecipeSeasonValues(recipeSeasons));

            iqb.Execute(DefaultConnectProvider);
        }

        private List<List<object>> GetRecipeSeasonValues(IEnumerable<RecipeSeason> recipeSeasons)
        {
            var values = new List<List<object>>();
            List<object> value;

            foreach (var recipeSeason in recipeSeasons)
            {
                value = new List<object>();

                value.Add(recipeSeason.RecipeId);
                value.Add(recipeSeason.SeasonId);

                values.Add(value);
            }

            return values;
        }

        internal void Delete(IEnumerable<Guid> recipeIds)
        {
            var dqb = new DeleteQueryBuilder();

            dqb.AddFrom(RecipeSeasonTableDescription.TableName);
            dqb.AddWhere(RecipeSeasonTableDescription.RecipeId, Comparison.In, recipeIds);

            dqb.Execute(DefaultConnectProvider);
        }
    }
}