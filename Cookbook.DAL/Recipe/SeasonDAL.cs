using Cookbook.DAL.Database;
using Cookbook.Entity.Recipe;
using System.Collections.Generic;
using System.Data;
using Tools.DAL.Database;
using Tools.DAL.QueryBuilder;

namespace Cookbook.DAL.Recipe
{
    public class SeasonDAL : DbDAL<SqlConnectionProvider>
    {
        public List<Season> Load()
        {
            var sqb = new SelectQueryBuilder();

            sqb.AddQueriedField(SeasonTableDescription.Id);
            sqb.AddQueriedField(SeasonTableDescription.Name);
            sqb.AddQueriedField(SeasonTableDescription.Code);

            sqb.AddFrom(SeasonTableDescription.TableName);

            return sqb.Read<Season, List<Season>>(DefaultConnectProvider, GetSeasonFromIDataRecord);
        }

        private Season GetSeasonFromIDataRecord(IDataRecord dataRecord)
        {
            var season = new Season();

            season.Id = dataRecord.GetGuid(SeasonTableDescription.Id);
            season.Name = dataRecord.GetString(SeasonTableDescription.Name);
            season.Code = dataRecord.GetString(SeasonTableDescription.Code);

            return season;
        }
    }
}
