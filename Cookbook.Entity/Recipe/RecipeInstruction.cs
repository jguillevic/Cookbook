using System;

namespace Cookbook.Entity.Recipe
{
    public class RecipeInstruction
    {
        public Guid RecipeId { get; set; }
        public string Instruction { get; set; }
        public int Order { get; set; }
    }
}