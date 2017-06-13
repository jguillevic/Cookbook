using Cookbook.UWP.RecipeServiceReference;
using System.Collections.Generic;

namespace Cookbook.UWP.Recipe
{
    public class VisualRecipeKind
    {
        public RecipeKind RecipeKind { get; set; }

        public string Name { get; set; }

        public static readonly VisualRecipeKind None = new VisualRecipeKind { RecipeKind = RecipeKind.None, Name = string.Empty };
        public static readonly VisualRecipeKind AmuseGueule = new VisualRecipeKind { RecipeKind = RecipeKind.AmuseGueule, Name = "Amuse-Gueule" };
        public static readonly VisualRecipeKind Dessert = new VisualRecipeKind { RecipeKind = RecipeKind.Dessert, Name = "Dessert" };
        public static readonly VisualRecipeKind Drink = new VisualRecipeKind { RecipeKind = RecipeKind.Drink, Name = "Boisson" };
        public static readonly VisualRecipeKind MainCourse = new VisualRecipeKind { RecipeKind = RecipeKind.MainCourse, Name = "Plat principal" };
        public static readonly VisualRecipeKind Sauce = new VisualRecipeKind { RecipeKind = RecipeKind.Sauce, Name = "Sauce" };
        public static readonly VisualRecipeKind SideDish = new VisualRecipeKind { RecipeKind = RecipeKind.SideDish, Name = "Accompagnement" };
        public static readonly VisualRecipeKind Starter = new VisualRecipeKind { RecipeKind = RecipeKind.Starter, Name = "Entrée" };
        public static readonly VisualRecipeKind Sweet = new VisualRecipeKind { RecipeKind = RecipeKind.Sweet, Name = "Confiserie" };
    }

    public static class RecipeKindDataProvider
    {
        public static List<VisualRecipeKind> RecipeKinds { get; set; }

        static RecipeKindDataProvider()
        {
            RecipeKinds = new List<VisualRecipeKind>();
            RecipeKinds.Add(VisualRecipeKind.None);
            RecipeKinds.Add(VisualRecipeKind.AmuseGueule);
            RecipeKinds.Add(VisualRecipeKind.Dessert);
            RecipeKinds.Add(VisualRecipeKind.Drink);
            RecipeKinds.Add(VisualRecipeKind.MainCourse);
            RecipeKinds.Add(VisualRecipeKind.Sauce);
            RecipeKinds.Add(VisualRecipeKind.SideDish);
            RecipeKinds.Add(VisualRecipeKind.Starter);
            RecipeKinds.Add(VisualRecipeKind.Sweet);
        }

        public static VisualRecipeKind GetVisualRecipeKind(RecipeKind recipeKind)
        {
            VisualRecipeKind visualRecipeKind = VisualRecipeKind.None; ;

            switch (recipeKind)
            {
                case RecipeKind.None:
                    visualRecipeKind = VisualRecipeKind.None;
                    break;
                case RecipeKind.Starter:
                    visualRecipeKind = VisualRecipeKind.Starter;
                    break;
                case RecipeKind.MainCourse:
                    visualRecipeKind = VisualRecipeKind.MainCourse;
                    break;
                case RecipeKind.Dessert:
                    visualRecipeKind = VisualRecipeKind.Dessert;
                    break;
                case RecipeKind.Sauce:
                    visualRecipeKind = VisualRecipeKind.Sauce;
                    break;
                case RecipeKind.Drink:
                    visualRecipeKind = VisualRecipeKind.Drink;
                    break;
                case RecipeKind.SideDish:
                    visualRecipeKind = VisualRecipeKind.SideDish;
                    break;
                case RecipeKind.AmuseGueule:
                    visualRecipeKind = VisualRecipeKind.AmuseGueule;
                    break;
                case RecipeKind.Sweet:
                    visualRecipeKind = VisualRecipeKind.Sweet;
                    break;
                default:
                    break;
            }

            return visualRecipeKind;
        }
    }
}
