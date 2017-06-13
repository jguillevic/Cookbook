using Cookbook.DAL.Database;
using Cookbook.Entity.Recipe;
using System.Collections.Generic;
using System.Data;
using Tools.DAL.Database;
using Tools.DAL.QueryBuilder;

namespace Cookbook.DAL.Recipe
{
    public class MeasureDAL : DbDAL<SqlConnectionProvider>
    {
        public List<Measure> Load()
        {
            var sqb = new SelectQueryBuilder();

            sqb.AddQueriedField(MeasureTableDescription.Id);
            sqb.AddQueriedField(MeasureTableDescription.Name);
            sqb.AddFrom(MeasureTableDescription.TableName);

            var measures = sqb.Read<Measure, List<Measure>>(DefaultConnectProvider, GetMeasureFromIDataRecord);

            return measures;
        }

        private Measure GetMeasureFromIDataRecord(IDataRecord dataRecord)
        {
            var measure = new Measure();

            measure.Id = dataRecord.GetGuid(MeasureTableDescription.Id);
            measure.Name = dataRecord.GetString(MeasureTableDescription.Name);

            return measure;
        }

        public void Add(IEnumerable<Measure> measures)
        {
            var iqb = new InsertQueryBuilder();

            iqb.SetTableName(MeasureTableDescription.TableName);

            iqb.AddInsertFields(new List<string> { MeasureTableDescription.Id, MeasureTableDescription.Name });
            iqb.AddInsertValues(GetMeasureValues(measures));

            iqb.Execute(DefaultConnectProvider);
        }

        public void Update(IEnumerable<Measure> measures)
        {
            var uqb = new UpdateQueryBuilder();

            uqb.SetTableName(MeasureTableDescription.TableName);

            uqb.AddSettedFields(
                new List<string> { MeasureTableDescription.Id }
                , new List<string> { MeasureTableDescription.Id, MeasureTableDescription.Name }
                , GetMeasureValues(measures));

            uqb.Execute(DefaultConnectProvider);
        }

        private List<List<object>> GetMeasureValues(IEnumerable<Measure> measures)
        {
            var values = new List<List<object>>();
            List<object> value;

            foreach (var measure in measures)
            {
                value = new List<object>();

                value.Add(measure.Id);
                value.Add(measure.Name);

                values.Add(value);
            }

            return values;
        }
    }
}