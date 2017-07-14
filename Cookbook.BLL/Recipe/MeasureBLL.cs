using Cookbook.DAL.Recipe;
using Cookbook.Entity.Recipe;
using System.Collections.Generic;

namespace Cookbook.BLL.Recipe
{
    public class MeasureBLL
    {
        private MeasureDAL _measureDAL;

        public MeasureBLL()
        {
            _measureDAL = new MeasureDAL();
        }

        public List<Measure> Load(MeasureFilter filter, List<string> fields)
        {
            return _measureDAL.Load(filter, fields);
        }

        public void Add(IEnumerable<Measure> measures)
        {
            _measureDAL.Add(measures);
        }

        public void Update(IEnumerable<Measure> measures)
        {
            _measureDAL.Update(measures);
        }
    }
}
