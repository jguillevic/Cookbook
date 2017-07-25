using Tools.Serializer;

namespace Cookbook.Serializer.Recipe
{
    public static class CostSerializerDescription
    {
        public static readonly PropertyDescription Id = new PropertyDescription { Name = "Id", ShortName = "0" };
        public static readonly PropertyDescription Name = new PropertyDescription { Name = "Name", ShortName = "1" };
        public static readonly PropertyDescription Code = new PropertyDescription { Name = "Code", ShortName = "2" };
    }

    public static class DifficultySerializerDescription
    {
        public static readonly PropertyDescription Id = new PropertyDescription { Name = "Id", ShortName = "0" };
        public static readonly PropertyDescription Name = new PropertyDescription { Name = "Name", ShortName = "1" };
        public static readonly PropertyDescription Code = new PropertyDescription { Name = "Code", ShortName = "2" };
    }

    public static class FeatureSerializerDescription
    {
        public static readonly PropertyDescription Id = new PropertyDescription { Name = "Id", ShortName = "0" };
        public static readonly PropertyDescription Name = new PropertyDescription { Name = "Name", ShortName = "1" };
        public static readonly PropertyDescription Code = new PropertyDescription { Name = "Code", ShortName = "2" };
    }

    public static class RecipeKindSerializerDescription
    {
        public static readonly PropertyDescription Id = new PropertyDescription { Name = "Id", ShortName = "0" };
        public static readonly PropertyDescription Name = new PropertyDescription { Name = "Name", ShortName = "1" };
        public static readonly PropertyDescription Code = new PropertyDescription { Name = "Code", ShortName = "2" };
    }

    public static class SeasonSerializerDescription
    {
        public static readonly PropertyDescription Id = new PropertyDescription { Name = "Id", ShortName = "0" };
        public static readonly PropertyDescription Name = new PropertyDescription { Name = "Name", ShortName = "1" };
        public static readonly PropertyDescription Code = new PropertyDescription { Name = "Code", ShortName = "2" };
    }

    public static class MeasureSerializerDescription
    {
        public static readonly PropertyDescription Id = new PropertyDescription { Name = "Id", ShortName = "0" };
        public static readonly PropertyDescription Name = new PropertyDescription { Name = "Name", ShortName = "1" };
        public static readonly PropertyDescription Code = new PropertyDescription { Name = "Code", ShortName = "2" };
    }

    public static class IngredientKindSerializerDescription
    {
        public static readonly PropertyDescription Id = new PropertyDescription { Name = "Id", ShortName = "0" };
        public static readonly PropertyDescription Name = new PropertyDescription { Name = "Name", ShortName = "1" };
        public static readonly PropertyDescription Code = new PropertyDescription { Name = "Code", ShortName = "2" };
    }

    public static class IngredientSerializerDescription
    {
        public static readonly PropertyDescription Id = new PropertyDescription { Name = "Id", ShortName = "0" };
        public static readonly PropertyDescription Name = new PropertyDescription { Name = "Name", ShortName = "1" };
        public static readonly PropertyDescription Code = new PropertyDescription { Name = "Code", ShortName = "2" };
        public static readonly PropertyDescription IngredientKindId = new PropertyDescription { Name = "IngredientKindId", ShortName = "3" };
        public static readonly PropertyDescription Calories = new PropertyDescription { Name = "Calories", ShortName = "4" };
        public static readonly PropertyDescription Protein = new PropertyDescription { Name = "Protein", ShortName = "5" };
        public static readonly PropertyDescription Carbohydrate = new PropertyDescription { Name = "Carbohydrate", ShortName = "6" };
        public static readonly PropertyDescription Lipid = new PropertyDescription { Name = "Lipid", ShortName = "7" };
        public static readonly PropertyDescription Water = new PropertyDescription { Name = "Water", ShortName = "8" };
        public static readonly PropertyDescription Fiber = new PropertyDescription { Name = "Fiber", ShortName = "9" };
    }

    public static class RecipeSerializerDescription
    {
        public static readonly PropertyDescription Id = new PropertyDescription { Name = "Id", ShortName = "0" };
        public static readonly PropertyDescription Name = new PropertyDescription { Name = "Name", ShortName = "1" };
        public static readonly PropertyDescription Description = new PropertyDescription { Name = "Description", ShortName = "2" };
        public static readonly PropertyDescription Instructions = new PropertyDescription { Name = "Instructions", ShortName = "3" };
        public static readonly PropertyDescription PreparationTime = new PropertyDescription { Name = "PreparationTime", ShortName = "4" };
        public static readonly PropertyDescription CookingTime = new PropertyDescription { Name = "CookingTime", ShortName = "5" };
        public static readonly PropertyDescription SeasonIds = new PropertyDescription { Name = "SeasonIds", ShortName = "6" };
        public static readonly PropertyDescription CostId = new PropertyDescription { Name = "CostId", ShortName = "7" };
        public static readonly PropertyDescription DifficultyId = new PropertyDescription { Name = "DifficultyId", ShortName = "8" };
        public static readonly PropertyDescription RecipeKindId = new PropertyDescription { Name = "RecipeKindId", ShortName = "9" };
        public static readonly PropertyDescription FeatureIds = new PropertyDescription { Name = "FeatureIds", ShortName = "10" };
        public static readonly PropertyDescription Ingredients = new PropertyDescription { Name = "Ingredients", ShortName = "11" };
        public static readonly PropertyDescription ExternalUrl = new PropertyDescription { Name = "ExternalUrl", ShortName = "12" };
        public static readonly PropertyDescription UserId = new PropertyDescription { Name = "UserId", ShortName = "13" };
        public static readonly PropertyDescription ImageUrl = new PropertyDescription { Name = "ImageUrl", ShortName = "14" };
    }

    public static class RecipeInstructionSerializerDescription
    {
        public static readonly PropertyDescription RecipeId = new PropertyDescription { Name = "RecipeId", ShortName = "0" };
        public static readonly PropertyDescription Instruction = new PropertyDescription { Name = "Instruction", ShortName = "1" };
        public static readonly PropertyDescription Order = new PropertyDescription { Name = "Order", ShortName = "2" };
    }

    public static class RecipeIngredientSerializerDescription
    {
        public static readonly PropertyDescription RecipeId = new PropertyDescription { Name = "RecipeId", ShortName = "0" };
        public static readonly PropertyDescription IngredientId = new PropertyDescription { Name = "IngredientId", ShortName = "1" };
        public static readonly PropertyDescription MeasureId = new PropertyDescription { Name = "MeasureId", ShortName = "2" };
        public static readonly PropertyDescription Order = new PropertyDescription { Name = "Order", ShortName = "3" };
        public static readonly PropertyDescription Amount = new PropertyDescription { Name = "Amount", ShortName = "4" };
    }
}
