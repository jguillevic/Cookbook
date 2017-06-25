using Cookbook.Entity.Recipe;
using System;
using Tools.UI.ViewData;

namespace Cookbook.UI.ViewData.Recipe
{
    public class RecipeInstructionVD : ViewDataBase
    {
        public Guid RecipeId { get; set; }

        public int Order { get; set; }

        public string _instruction;
        public string Instruction
        {
            get { return _instruction; }
            set
            {
                if (_instruction != value)
                {
                    _instruction = value;
                    OnPropertyChanged("Instruction");
                }
            }
        }

        public RecipeInstructionVD(RecipeInstruction recipeInstruction)
        {
            RecipeId = recipeInstruction.RecipeId;
            Order = recipeInstruction.Order;
            Instruction = recipeInstruction.Instruction;
        }

        public RecipeInstruction GetEntity()
        {
            var recipeInstruction = new RecipeInstruction();

            recipeInstruction.RecipeId = RecipeId;
            recipeInstruction.Order = Order;
            recipeInstruction.Instruction = Instruction;

            return recipeInstruction;
        }
    }
}
