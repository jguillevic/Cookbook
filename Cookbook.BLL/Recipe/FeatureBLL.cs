using Cookbook.DAL.Recipe;
using Cookbook.Entity.Recipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookbook.BLL.Recipe
{
    public class FeatureBLL
    {
        private FeatureDAL _featureDAL;

        public FeatureBLL()
        {
            _featureDAL = new FeatureDAL();
        }

        public List<Feature> Load()
        {
            return _featureDAL.Load();
        }
    }
}
