using System;

namespace Cookbook.Entity.Recipe
{
    [Flags]
    public enum Season
    {
        None = 0,
        Winter = 1,
        Spring = 2,
        Summer = 4,
        Autumn = 8,
    }
}