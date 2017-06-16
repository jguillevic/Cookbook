using Cookbook.Contract.Recipe;
using Cookbook.Entity.Recipe;
using System.Collections.Generic;
using System.ServiceModel;

namespace Cookbook.ServiceProxy.Recipe
{
    public class CostServiceProxy : ClientBase<ICostService>, ICostService
    {
        public List<Cost> Load()
        {
            return Channel.Load();
        }
    }
}
