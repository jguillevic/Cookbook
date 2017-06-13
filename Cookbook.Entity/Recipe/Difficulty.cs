using System;

namespace Cookbook.Entity.Recipe
{
    [Flags]
    public enum Difficulty
    {
        None = 0,
        VeryEasy = 1,
        Easy = 2,
        Medium = 4,
        Difficult = 8,
        VeryDifficult = 16
    }
}