using Cookbook.DAL.Database;
using Cookbook.Entity.Recipe;
using System.Collections.Generic;
using System.Data;
using Tools.DAL.Database;
using Tools.DAL.QueryBuilder;
using Tools.DAL.QueryBuilder.Enum;

namespace Cookbook.DAL.Recipe
{
    public class MeasureSummaryDAL : DbDAL<SqlConnectionProvider>
    {
        public List<MeasureSummary> Load()
        {
            return Load(new MeasureFilter());
        }

        public List<MeasureSummary> Load(MeasureFilter filter)
        {
            var sqb = new SelectQueryBuilder();

            AddQueriedFields(sqb);

            AddFrom(sqb);

            if (filter.IdsToLoad.Count > 0)
                sqb.AddWhere(MeasureTableDescription.Id, Comparison.In, filter.IdsToLoad);

            var measures = sqb.Read<MeasureSummary, List<MeasureSummary>>(DefaultConnectProvider, GetMeasureSummaryFromIDataRecord);

            return measures;
        }

        private static void AddQueriedFields(SelectQueryBuilder sqb)
        {
            sqb.AddQueriedField(MeasureTableDescription.Id);
            sqb.AddQueriedField(MeasureTableDescription.Name);
        }

        private static void AddFrom(SelectQueryBuilder sqb)
        {
            sqb.AddFrom(MeasureTableDescription.TableName);
        }

        private MeasureSummary GetMeasureSummaryFromIDataRecord(IDataRecord dataRecord)
        {
            var measure = new MeasureSummary();

            measure.Id = dataRecord.GetGuid(MeasureTableDescription.Id);
            measure.Name = dataRecord.GetString(MeasureTableDescription.Name);

            return measure;
        }
    }
}
