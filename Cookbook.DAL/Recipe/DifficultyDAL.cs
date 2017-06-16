using Cookbook.DAL.Database;
using Cookbook.Entity.Recipe;
using System.Collections.Generic;
using System.Data;
using Tools.DAL.Database;
using Tools.DAL.QueryBuilder;

namespace Cookbook.DAL.Recipe
{
    public class DifficultyDAL : DbDAL<SqlConnectionProvider>
    {
        public List<Difficulty> Load()
        {
            var sqb = new SelectQueryBuilder();

            sqb.AddQueriedField(DifficultyTableDescription.Id);
            sqb.AddQueriedField(DifficultyTableDescription.Name);
            sqb.AddQueriedField(DifficultyTableDescription.Code);

            sqb.AddFrom(DifficultyTableDescription.TableName);

            return sqb.Read<Difficulty, List<Difficulty>>(DefaultConnectProvider, GetDifficultyFromIDataRecord);
        }

        private Difficulty GetDifficultyFromIDataRecord(IDataRecord dataRecord)
        {
            var difficulty = new Difficulty();

            difficulty.Id = dataRecord.GetGuid(DifficultyTableDescription.Id);
            difficulty.Name = dataRecord.GetString(DifficultyTableDescription.Name);
            difficulty.Code = dataRecord.GetString(DifficultyTableDescription.Code);

            return difficulty;
        }
    }
}
