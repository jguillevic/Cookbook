using Cookbook.UWP.RecipeServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookbook.UWP.Recipe
{
    public class VisualCost
    {
        public Cost Cost { get; set; }

        public string Name { get; set; }

        public static readonly VisualCost None = new VisualCost { Cost = Cost.None, Name = string.Empty };
        public static readonly VisualCost Cheap = new VisualCost { Cost = Cost.Cheap, Name = "Peu cher" };
        public static readonly VisualCost Medium = new VisualCost { Cost = Cost.Medium, Name = "Moyen" };
        public static readonly VisualCost Expensive = new VisualCost { Cost = Cost.Expensive, Name = "Cher" };
    }

    public static class CostDataProvider
    {
        public static List<VisualCost> Costs { get; set; }

        static CostDataProvider()
        {
            Costs = new List<VisualCost>();
            Costs.Add(VisualCost.None);
            Costs.Add(VisualCost.Cheap);
            Costs.Add(VisualCost.Medium);
            Costs.Add(VisualCost.Expensive);
        }

        public static VisualCost GetVisualCost(Cost cost)
        {
            var visualCost = VisualCost.None;

            switch (cost)
            {
                case Cost.None:
                    visualCost = VisualCost.None;
                    break;
                case Cost.Cheap:
                    visualCost = VisualCost.Cheap;
                    break;
                case Cost.Medium:
                    visualCost = VisualCost.Medium;
                    break;
                case Cost.Expensive:
                    visualCost = VisualCost.Expensive;
                    break;
                default:
                    break;
            }

            return visualCost;
        }
    }
}
