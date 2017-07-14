using Cookbook.DAL.Database;
using Cookbook.Entity.Recipe;
using System.Collections.Generic;
using System.Data;
using Tools.DAL.Database;
using Tools.DAL.QueryBuilder;
using static Cookbook.Entity.Recipe.RecipeEntityDescriptions;

namespace Cookbook.DAL.Recipe
{
    public class SeasonDAL : DbDAL<SqlConnectionProvider>
    {
        private List<string> _fields;

        public List<Season> Load(List<string> fields)
        {
            _fields = fields;

            var sqb = new SelectQueryBuilder();

            AddQueriedFields(sqb);

            sqb.AddFrom(SeasonTableDescription.TableName);

            return sqb.Read<Season, List<Season>>(DefaultConnectProvider, GetSeasonFromIDataRecord);
        }

        private void AddQueriedFields(SelectQueryBuilder sqb)
        {
            if (_fields.Contains(SeasonEntityDescription.Id.ToLower()))
                sqb.AddQueriedField(SeasonTableDescription.Id);
            if (_fields.Contains(SeasonEntityDescription.Name.ToLower()))
                sqb.AddQueriedField(SeasonTableDescription.Name);
            if (_fields.Contains(SeasonEntityDescription.Code.ToLower()))
                sqb.AddQueriedField(SeasonTableDescription.Code);
        }

        private Season GetSeasonFromIDataRecord(IDataRecord dataRecord)
        {
            var season = new Season();

            if (_fields.Contains(SeasonEntityDescription.Id.ToLower()))
                season.Id = dataRecord.GetGuid(SeasonTableDescription.Id);
            if (_fields.Contains(SeasonEntityDescription.Name.ToLower()))
                season.Name = dataRecord.GetString(SeasonTableDescription.Name);
            if (_fields.Contains(SeasonEntityDescription.Code.ToLower()))
                season.Code = dataRecord.GetString(SeasonTableDescription.Code);

            return season;
        }
    }
}
