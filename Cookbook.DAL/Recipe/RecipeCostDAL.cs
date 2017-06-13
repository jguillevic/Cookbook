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
    public class RecipeCostDAL : DbDAL<SqlConnectionProvider>
    {
        internal HashSet<RecipeCost> Load(IEnumerable<Guid> recipeIds)
        {
            var sqb = new SelectQueryBuilder();

            sqb.AddQueriedField(RecipeCostTableDescription.RecipeId);
            sqb.AddQueriedField(RecipeCostTableDescription.CostId);

            sqb.AddFrom(RecipeCostTableDescription.TableName);

            sqb.AddWhere(RecipeCostTableDescription.RecipeId, Comparison.In, recipeIds);

            var recipeCosts = sqb.Read<RecipeCost, HashSet<RecipeCost>>(DefaultConnectProvider, GetRecipeCostFromIDataRecord);

            return recipeCosts;
        }

        private RecipeCost GetRecipeCostFromIDataRecord(IDataRecord dataRecord)
        {
            var recipeCost = new RecipeCost();

            recipeCost.RecipeId = dataRecord.GetGuid(RecipeCostTableDescription.RecipeId);
            recipeCost.Cost = (Cost)dataRecord.GetInt32(RecipeCostTableDescription.CostId);

            return recipeCost;
        }

        internal void Add(IEnumerable<RecipeCost> recipeCosts)
        {
            var iqb = new InsertQueryBuilder();

            iqb.SetTableName(RecipeCostTableDescription.TableName);

            iqb.AddInsertFields(
                new List<string> {
                    RecipeCostTableDescription.RecipeId
                    , RecipeCostTableDescription.CostId
                });
            iqb.AddInsertValues(GetRecipeCostValues(recipeCosts));

            iqb.Execute(DefaultConnectProvider);
        }

        private List<List<object>> GetRecipeCostValues(IEnumerable<RecipeCost> recipeCosts)
        {
            var values = new List<List<object>>();
            List<object> value;

            foreach (var recipeCost in recipeCosts)
            {
                value = new List<object>();

                value.Add(recipeCost.RecipeId);
                value.Add((int)recipeCost.Cost);

                values.Add(value);
            }

            return values;
        }

        internal void Delete(IEnumerable<Guid> recipeIds)
        {
            var dqb = new DeleteQueryBuilder();

            dqb.AddFrom(RecipeCostTableDescription.TableName);
            dqb.AddWhere(RecipeCostTableDescription.RecipeId, Comparison.In, recipeIds);

            dqb.Execute(DefaultConnectProvider);
        }
    }
}
