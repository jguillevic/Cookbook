using Cookbook.DAL.Database;
using Cookbook.Entity.Recipe;
using System.Collections.Generic;
using System.Data;
using Tools.DAL.Database;
using Tools.DAL.QueryBuilder;
using Tools.DAL.QueryBuilder.Enum;
using static Cookbook.Entity.Recipe.RecipeEntityDescriptions;

namespace Cookbook.DAL.Recipe
{
    public class MeasureDAL : DbDAL<SqlConnectionProvider>
    {
        private List<string> _fields;

        public List<Measure> Load(MeasureFilter filter, List<string> fields)
        {
            _fields = fields;

            var sqb = new SelectQueryBuilder();

            AddQueriedFields(sqb);

            AddFrom(sqb);

            if (filter.IdsToLoad.Count > 0)
                sqb.AddWhere(MeasureTableDescription.Id, Comparison.In, filter.IdsToLoad);

            var measures = sqb.Read<Measure, List<Measure>>(DefaultConnectProvider, GetMeasureFromIDataRecord);

            _fields = null;

            return measures;
        }

        private void AddQueriedFields(SelectQueryBuilder sqb)
        {
            if (_fields.Contains(MeasureEntityDescription.Id.ToLower()))
                sqb.AddQueriedField(MeasureTableDescription.Id);
            if (_fields.Contains(MeasureEntityDescription.Name.ToLower()))
                sqb.AddQueriedField(MeasureTableDescription.Name);
            if (_fields.Contains(MeasureEntityDescription.Code.ToLower()))
                sqb.AddQueriedField(MeasureTableDescription.Code);
        }

        private void AddFrom(SelectQueryBuilder sqb)
        {
            sqb.AddFrom(MeasureTableDescription.TableName);
        }

        private Measure GetMeasureFromIDataRecord(IDataRecord dataRecord)
        {
            var measure = new Measure();

            if (_fields.Contains(MeasureEntityDescription.Id.ToLower()))
                measure.Id = dataRecord.GetGuid(MeasureTableDescription.Id);
            if (_fields.Contains(MeasureEntityDescription.Name.ToLower()))
                measure.Name = dataRecord.GetString(MeasureTableDescription.Name);
            if (_fields.Contains(MeasureEntityDescription.Code.ToLower()))
                measure.Code = dataRecord.GetString(MeasureTableDescription.Code);

            return measure;
        }

        public void Add(IEnumerable<Measure> measures)
        {
            var iqb = new InsertQueryBuilder();

            iqb.SetTableName(MeasureTableDescription.TableName);

            iqb.AddInsertFields(new List<string> { MeasureTableDescription.Id, MeasureTableDescription.Name, MeasureTableDescription.Code });
            iqb.AddInsertValues(GetMeasureValues(measures));

            iqb.Execute(DefaultConnectProvider);
        }

        public void Update(IEnumerable<Measure> measures)
        {
            var uqb = new UpdateQueryBuilder();

            uqb.SetTableName(MeasureTableDescription.TableName);

            uqb.AddSettedFields(
                new List<string> { MeasureTableDescription.Id }
                , new List<string> { MeasureTableDescription.Id, MeasureTableDescription.Name, MeasureTableDescription.Code }
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
                value.Add(measure.Code);

                values.Add(value);
            }

            return values;
        }
    }
}