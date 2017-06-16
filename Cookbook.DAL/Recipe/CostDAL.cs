using Cookbook.DAL.Database;
using Cookbook.Entity.Recipe;
using System.Collections.Generic;
using System.Data;
using Tools.DAL.Database;
using Tools.DAL.QueryBuilder;

namespace Cookbook.DAL.Recipe
{
    public class CostDAL : DbDAL<SqlConnectionProvider>
    {
        public List<Cost> Load()
        {
            var sqb = new SelectQueryBuilder();

            sqb.AddQueriedField(CostTableDescription.Id);
            sqb.AddQueriedField(CostTableDescription.Name);
            sqb.AddQueriedField(CostTableDescription.Code);

            sqb.AddFrom(CostTableDescription.TableName);

            return sqb.Read<Cost, List<Cost>>(DefaultConnectProvider, GetCostFromIDataRecord);
        }

        private Cost GetCostFromIDataRecord(IDataRecord dataRecord)
        {
            var cost = new Cost();

            cost.Id = dataRecord.GetGuid(CostTableDescription.Id);
            cost.Name = dataRecord.GetString(CostTableDescription.Name);
            cost.Code = dataRecord.GetString(CostTableDescription.Code);

            return cost;
        }
    }
}
