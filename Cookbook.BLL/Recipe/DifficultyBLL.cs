using Cookbook.DAL.Recipe;
using Cookbook.Entity.Recipe;
using System.Collections.Generic;

namespace Cookbook.BLL.Recipe
{
    public class DifficultyBLL
    {
        private DifficultyDAL _difficultyDAL;

        public DifficultyBLL()
        {
            _difficultyDAL = new DifficultyDAL();
        }

        public List<Difficulty> Load()
        {
            return _difficultyDAL.Load();
        }
    }
}
