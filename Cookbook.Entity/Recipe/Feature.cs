using System;

namespace Cookbook.Entity.Recipe
{
    [Flags]
    public enum Feature
    {
        None = 0,
        Vegetarian = 1,
        Festive = 2,
    }
}