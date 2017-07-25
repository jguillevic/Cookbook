using System;
using System.Collections.Generic;

namespace Cookbook.Entity.Recipe
{
    public class Measure
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<string> AlternativeNames { get; set; }
        public string Code { get; set; }

        public Measure()
        {
            AlternativeNames = new List<string>();
        }
    }
}