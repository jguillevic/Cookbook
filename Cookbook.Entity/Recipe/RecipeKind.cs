using System;

namespace Cookbook.Entity.Recipe
{
    [Flags]
    public enum RecipeKind
    {
        None = 0,
        Starter = 1,
        MainCourse = 2,
        Dessert = 4,
        Sauce = 8,
        Drink = 16,
        SideDish = 32,
        AmuseGueule = 64,
        Sweet = 128
    }
}