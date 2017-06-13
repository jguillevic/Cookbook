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
    public class RecipeRecipeKindDAL : DbDAL<SqlConnectionProvider>
    {
        internal HashSet<RecipeRecipeKind> Load(IEnumerable<Guid> recipeIds)
        {
            var sqb = new SelectQueryBuilder();

            sqb.AddQueriedField(RecipeRecipeKindTableDescription.RecipeId);
            sqb.AddQueriedField(RecipeRecipeKindTableDescription.RecipeKindId);

            sqb.AddFrom(RecipeRecipeKindTableDescription.TableName);

            sqb.AddWhere(RecipeRecipeKindTableDescription.RecipeId, Comparison.In, recipeIds);

            var recipeRecipeKinds = sqb.Read<RecipeRecipeKind, HashSet<RecipeRecipeKind>>(DefaultConnectProvider, GetRecipeRecipeKindFromIDataRecord);

            return recipeRecipeKinds;
        }

        private RecipeRecipeKind GetRecipeRecipeKindFromIDataRecord(IDataRecord dataRecord)
        {
            var recipeRecipeKind = new RecipeRecipeKind();

            recipeRecipeKind.RecipeId = dataRecord.GetGuid(RecipeRecipeKindTableDescription.RecipeId);
            recipeRecipeKind.RecipeKind = (RecipeKind)dataRecord.GetInt32(RecipeRecipeKindTableDescription.RecipeKindId);

            return recipeRecipeKind;
        }

        internal void Add(IEnumerable<RecipeRecipeKind> recipeRecipeKinds)
        {
            var iqb = new InsertQueryBuilder();

            iqb.SetTableName(RecipeRecipeKindTableDescription.TableName);

            iqb.AddInsertFields(
                new List<string> {
                    RecipeRecipeKindTableDescription.RecipeId
                    , RecipeRecipeKindTableDescription.RecipeKindId
                });
            iqb.AddInsertValues(GetRecipeRecipeKindValues(recipeRecipeKinds));

            iqb.Execute(DefaultConnectProvider);
        }

        private List<List<object>> GetRecipeRecipeKindValues(IEnumerable<RecipeRecipeKind> recipeRecipeKinds)
        {
            var values = new List<List<object>>();
            List<object> value;

            foreach (var recipeRecipeKind in recipeRecipeKinds)
            {
                value = new List<object>();

                value.Add(recipeRecipeKind.RecipeId);
                value.Add((int)recipeRecipeKind.RecipeKind);

                values.Add(value);
            }

            return values;
        }

        internal void Delete(IEnumerable<Guid> recipeIds)
        {
            var dqb = new DeleteQueryBuilder();

            dqb.AddFrom(RecipeRecipeKindTableDescription.TableName);
            dqb.AddWhere(RecipeRecipeKindTableDescription.RecipeId, Comparison.In, recipeIds);

            dqb.Execute(DefaultConnectProvider);
        }
    }
}
