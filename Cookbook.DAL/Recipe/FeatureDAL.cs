using Cookbook.DAL.Database;
using Cookbook.Entity.Recipe;
using System.Collections.Generic;
using System.Data;
using Tools.DAL.Database;
using Tools.DAL.QueryBuilder;
using static Cookbook.Entity.Recipe.RecipeEntityDescriptions;

namespace Cookbook.DAL.Recipe
{
    public class FeatureDAL : DbDAL<SqlConnectionProvider>
    {
        private List<string> _fields;

        public List<Feature> Load(List<string> fields)
        {
            _fields = fields;

            var sqb = new SelectQueryBuilder();

            AddQueriedFields(sqb);

            sqb.AddFrom(FeatureTableDescription.TableName);

            return sqb.Read<Feature, List<Feature>>(DefaultConnectProvider, GetFeatureFromIDataRecord);
        }

        private void AddQueriedFields(SelectQueryBuilder sqb)
        {
            if (_fields.Contains(FeatureEntityDescription.Id.ToLower()))
                sqb.AddQueriedField(FeatureTableDescription.Id);
            if (_fields.Contains(FeatureEntityDescription.Name.ToLower()))
                sqb.AddQueriedField(FeatureTableDescription.Name);
            if (_fields.Contains(FeatureEntityDescription.Code.ToLower()))
                sqb.AddQueriedField(FeatureTableDescription.Code);
        }

        private Feature GetFeatureFromIDataRecord(IDataRecord dataRecord)
        {
            var feature = new Feature();

            if (_fields.Contains(FeatureEntityDescription.Id.ToLower()))
                feature.Id = dataRecord.GetGuid(FeatureTableDescription.Id);
            if (_fields.Contains(FeatureEntityDescription.Name.ToLower()))
                feature.Name = dataRecord.GetString(FeatureTableDescription.Name);
            if (_fields.Contains(FeatureEntityDescription.Code.ToLower()))
                feature.Code = dataRecord.GetString(FeatureTableDescription.Code);

            return feature;
        }
    }
}
