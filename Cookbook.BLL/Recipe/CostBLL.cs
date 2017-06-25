using Cookbook.DAL.Recipe;
using Cookbook.Entity.Recipe;
using System.Collections.Generic;

namespace Cookbook.BLL.Recipe
{
    public class CostBLL
    {
        private CostDAL _costDAL;

        public CostBLL()
        {
            _costDAL = new CostDAL();
        }

        public List<Cost> Load()
        {
            return _costDAL.Load();
        }
    }
}
