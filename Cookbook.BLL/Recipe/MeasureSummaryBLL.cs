using Cookbook.DAL.Recipe;
using Cookbook.Entity.Recipe;
using System.Collections.Generic;

namespace Cookbook.BLL.Recipe
{
    public class MeasureSummaryBLL
    {
        private MeasureSummaryDAL _measureSummaryDAL;

        public MeasureSummaryBLL()
        {
            _measureSummaryDAL = new MeasureSummaryDAL();
        }

        public List<MeasureSummary> Load()
        {
            return _measureSummaryDAL.Load();
        }

        public List<MeasureSummary> Load(MeasureFilter filter)
        {
            return _measureSummaryDAL.Load(filter);
        }
    }
}
