using System;
using System.Collections.Generic;
using System.Linq;

namespace Cookbook.Entity.Recipe
{
    public class MeasureFilter
    {
        public List<Guid> IdsToLoad { get; set; }

        public MeasureFilter()
        {
            IdsToLoad = new List<Guid>();
        }
    }
}
