using Cookbook.DAL.Recipe;
using Cookbook.Entity.Recipe;
using System.Collections.Generic;

namespace Cookbook.BLL.Recipe
{
    public class FeatureBLL
    {
        private FeatureDAL _featureDAL;

        public FeatureBLL()
        {
            _featureDAL = new FeatureDAL();
        }

        public List<Feature> Load(List<string> fields)
        {
            return _featureDAL.Load(fields);
        }
    }
}
