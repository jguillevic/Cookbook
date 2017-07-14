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
    public class DifficultyDAL : DbDAL<SqlConnectionProvider>
    {
        private List<string> _fields;

        public List<Difficulty> Load(List<string> fields)
        {
            _fields = fields;

            var sqb = new SelectQueryBuilder();

            AddQueriedFields(sqb);

            sqb.AddFrom(DifficultyTableDescription.TableName);

            sqb.AddOrderBy(DifficultyTableDescription.Order, Sorting.Ascending);

            return sqb.Read<Difficulty, List<Difficulty>>(DefaultConnectProvider, GetDifficultyFromIDataRecord);
        }

        private void AddQueriedFields(SelectQueryBuilder sqb)
        {
            if (_fields.Contains(DifficultyEntityDescription.Id.ToLower()))
                sqb.AddQueriedField(DifficultyTableDescription.Id);
            if (_fields.Contains(DifficultyEntityDescription.Id.ToLower()))
                sqb.AddQueriedField(DifficultyTableDescription.Name);
            if (_fields.Contains(DifficultyEntityDescription.Id.ToLower()))
                sqb.AddQueriedField(DifficultyTableDescription.Code);
        }

        private Difficulty GetDifficultyFromIDataRecord(IDataRecord dataRecord)
        {
            var difficulty = new Difficulty();

            if (_fields.Contains(DifficultyEntityDescription.Id.ToLower()))
                difficulty.Id = dataRecord.GetGuid(DifficultyTableDescription.Id);
            if (_fields.Contains(DifficultyEntityDescription.Id.ToLower()))
                difficulty.Name = dataRecord.GetString(DifficultyTableDescription.Name);
            if (_fields.Contains(DifficultyEntityDescription.Id.ToLower()))
                difficulty.Code = dataRecord.GetString(DifficultyTableDescription.Code);

            return difficulty;
        }
    }
}
