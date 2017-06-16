using Cookbook.DAL.Database;
using Cookbook.Entity.Recipe;
using System.Collections.Generic;
using System.Data;
using Tools.DAL.Database;
using Tools.DAL.QueryBuilder;

namespace Cookbook.DAL.Recipe
{
    public class FeatureDAL : DbDAL<SqlConnectionProvider>
    {
        public List<Feature> Load()
        {
            var sqb = new SelectQueryBuilder();

            sqb.AddQueriedField(FeatureTableDescription.Id);
            sqb.AddQueriedField(FeatureTableDescription.Name);
            sqb.AddQueriedField(FeatureTableDescription.Code);

            sqb.AddFrom(FeatureTableDescription.TableName);

            return sqb.Read<Feature, List<Feature>>(DefaultConnectProvider, GetFeatureFromIDataRecord);
        }

        private Feature GetFeatureFromIDataRecord(IDataRecord dataRecord)
        {
            var feature = new Feature();

            feature.Id = dataRecord.GetGuid(FeatureTableDescription.Id);
            feature.Name = dataRecord.GetString(FeatureTableDescription.Name);
            feature.Code = dataRecord.GetString(FeatureTableDescription.Code);

            return feature;
        }
    }
}
