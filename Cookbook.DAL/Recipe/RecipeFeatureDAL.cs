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
    public class RecipeFeatureDAL : DbDAL<SqlConnectionProvider>
    {
        internal HashSet<RecipeFeature> Load(IEnumerable<Guid> recipeIds)
        {
            var sqb = new SelectQueryBuilder();

            sqb.AddQueriedField(RecipeFeatureTableDescription.RecipeId);
            sqb.AddQueriedField(RecipeFeatureTableDescription.FeatureId);

            sqb.AddFrom(RecipeFeatureTableDescription.TableName);

            sqb.AddWhere(RecipeFeatureTableDescription.RecipeId, Comparison.In, recipeIds);

            var recipeFeatures = sqb.Read<RecipeFeature, HashSet<RecipeFeature>>(DefaultConnectProvider, GetRecipeFeatureFromIDataRecord);

            return recipeFeatures;
        }

        private RecipeFeature GetRecipeFeatureFromIDataRecord(IDataRecord dataRecord)
        {
            var recipeFeature = new RecipeFeature();

            recipeFeature.RecipeId = dataRecord.GetGuid(RecipeFeatureTableDescription.RecipeId);
            recipeFeature.Feature = (Feature)dataRecord.GetInt32(RecipeFeatureTableDescription.FeatureId);

            return recipeFeature;
        }

        internal void Add(IEnumerable<RecipeFeature> recipeFeatures)
        {
            var iqb = new InsertQueryBuilder();

            iqb.SetTableName(RecipeFeatureTableDescription.TableName);

            iqb.AddInsertFields(
                new List<string> {
                    RecipeFeatureTableDescription.RecipeId
                    , RecipeFeatureTableDescription.FeatureId
                });
            iqb.AddInsertValues(GetRecipeFeatureValues(recipeFeatures));

            iqb.Execute(DefaultConnectProvider);
        }

        private List<List<object>> GetRecipeFeatureValues(IEnumerable<RecipeFeature> recipeFeatures)
        {
            var values = new List<List<object>>();
            List<object> value;

            foreach (var recipeFeature in recipeFeatures)
            {
                value = new List<object>();

                value.Add(recipeFeature.RecipeId);
                value.Add((int)recipeFeature.Feature);

                values.Add(value);
            }

            return values;
        }

        internal void Delete(IEnumerable<Guid> recipeIds)
        {
            var dqb = new DeleteQueryBuilder();

            dqb.AddFrom(RecipeFeatureTableDescription.TableName);
            dqb.AddWhere(RecipeFeatureTableDescription.RecipeId, Comparison.In, recipeIds);

            dqb.Execute(DefaultConnectProvider);
        }
    }
}
