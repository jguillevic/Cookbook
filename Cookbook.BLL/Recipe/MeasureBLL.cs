using Cookbook.DAL.Recipe;
using Cookbook.Entity.Recipe;
using System;
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

        public List<Measure> Load()
        {
            return _measureDAL.Load();
        }

        public List<Measure> Load(MeasureFilter filter)
        {
            return _measureDAL.Load(filter);
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
