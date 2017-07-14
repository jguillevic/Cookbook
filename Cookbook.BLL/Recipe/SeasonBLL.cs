using Cookbook.DAL.Recipe;
using Cookbook.Entity.Recipe;
using System.Collections.Generic;

namespace Cookbook.BLL.Recipe
{
    public class SeasonBLL
    {
        private SeasonDAL _seasonDAL;

        public SeasonBLL()
        {
            _seasonDAL = new SeasonDAL();
        }

        public List<Season> Load(List<string> fields)
        {
            return _seasonDAL.Load(fields);
        }
    }
}
