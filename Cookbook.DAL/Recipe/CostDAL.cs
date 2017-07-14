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
    public class CostDAL : DbDAL<SqlConnectionProvider>
    {
        private List<string> _fields;

        public List<Cost> Load(List<string> fields)
        {
            _fields = fields;

            var sqb = new SelectQueryBuilder();

            AddQueriedFields(sqb);

            sqb.AddFrom(CostTableDescription.TableName);

            sqb.AddOrderBy(CostTableDescription.Order, Sorting.Ascending);

            return sqb.Read<Cost, List<Cost>>(DefaultConnectProvider, GetCostFromIDataRecord);
        }

        private void AddQueriedFields(SelectQueryBuilder sqb)
        {
            if (_fields.Contains(CostEntityDescription.Id.ToLower()))
                sqb.AddQueriedField(CostTableDescription.Id);
            if (_fields.Contains(CostEntityDescription.Name.ToLower()))
                sqb.AddQueriedField(CostTableDescription.Name);
            if (_fields.Contains(CostEntityDescription.Code.ToLower()))
                sqb.AddQueriedField(CostTableDescription.Code);
        }

        private Cost GetCostFromIDataRecord(IDataRecord dataRecord)
        {
            var cost = new Cost();

            if (_fields.Contains(CostEntityDescription.Id.ToLower()))
                cost.Id = dataRecord.GetGuid(CostTableDescription.Id);
            if (_fields.Contains(CostEntityDescription.Name.ToLower()))
                cost.Name = dataRecord.GetString(CostTableDescription.Name);
            if (_fields.Contains(CostEntityDescription.Code.ToLower()))
                cost.Code = dataRecord.GetString(CostTableDescription.Code);

            return cost;
        }
    }
}
