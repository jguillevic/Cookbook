using System;

namespace Cookbook.Entity.Recipe
{
    [Flags]
    public enum Cost
    {
        None = 0,
        Cheap = 1,
        Medium = 2,
        Expensive = 4
    }
}