using Cookbook.Entity.Recipe;
using System;

namespace Cookbook.Rule.Recipe
{
    public static class MeasureRule
    {
        public static Measure GetDefault()
        {
            var measure = new Measure();

            measure.Id = Guid.NewGuid();

            return measure;
        }
    }
}
