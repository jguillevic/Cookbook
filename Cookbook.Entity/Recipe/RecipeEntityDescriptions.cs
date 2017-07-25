using System.Collections.Generic;

namespace Cookbook.Entity.Recipe
{
    public static class RecipeEntityDescriptions
    {
        public static class CostEntityDescription
        {
            public const string Id = "Id";
            public const string Name = "Name";
            public const string Code = "Code";
            public static List<string> AllLower = new List<string> { Id.ToLower(), Name.ToLower(), Code.ToLower() };
        }

        public static class DifficultyEntityDescription
        {
            public const string Id = "Id";
            public const string Name = "Name";
            public const string Code = "Code";
            public static List<string> AllLower = new List<string> { Id.ToLower(), Name.ToLower(), Code.ToLower() };
        }

        public static class FeatureEntityDescription
        {
            public const string Id = "Id";
            public const string Name = "Name";
            public const string Code = "Code";
            public static List<string> AllLower = new List<string> { Id.ToLower(), Name.ToLower(), Code.ToLower() };
        }

        public static class RecipeKindEntityDescription
        {
            public const string Id = "Id";
            public const string Name = "Name";
            public const string Code = "Code";
            public static List<string> AllLower = new List<string> { Id.ToLower(), Name.ToLower(), Code.ToLower() };
        }

        public static class SeasonEntityDescription
        {
            public const string Id = "Id";
            public const string Name = "Name";
            public const string Code = "Code";
            public static List<string> AllLower = new List<string> { Id.ToLower(), Name.ToLower(), Code.ToLower() };
        }

        public static class MeasureEntityDescription
        {
            public const string Id = "Id";
            public const string Name = "Name";
            public const string Code = "Code";
            public static List<string> AllLower = new List<string> { Id.ToLower(), Name.ToLower(), Code.ToLower() };
        }

        public static class IngredientKindEntityDescription
        {
            public const string Id = "Id";
            public const string Name = "Name";
            public const string Code = "Code";
            public static List<string> AllLower = new List<string> { Id.ToLower(), Name.ToLower(), Code.ToLower() };
        }

        public static class IngredientEntityDescription
        {
            public const string Id = "Id";
            public const string Name = "Name";
            public const string Code = "Code";
            public const string IngredientKindId = "IngredientKindId";
            public const string Calories = "Calories";
            public const string Protein = "Protein";
            public const string Carbohydrate = "Carbohydrate";
            public const string Lipid = "Lipid";
            public const string Water = "Water";
            public const string Fiber = "Fiber";
            public static List<string> AllLower 
                = new List<string> {
                    Id.ToLower()
                    , Name.ToLower()
                    , Code.ToLower()
                    , IngredientKindId.ToLower()
                    , Calories.ToLower()
                    , Protein.ToLower()
                    , Carbohydrate.ToLower()
                    , Lipid.ToLower()
                    , Water.ToLower()
                    , Fiber.ToLower()};
        }

        public static class RecipeEntityDescription
        {
            public const string Id = "Id";
            public const string Name = "Name";
            public const string Description = "Description";
            public const string PreparationTime = "PreparationTime";
            public const string CookingTime = "CookingTime";
            public const string SeasonIds = "SeasonIds";
            public const string CostId = "CostId";
            public const string DifficultyId = "DifficultyId";
            public const string RecipeKindId = "RecipeKindId";
            public const string FeatureIds = "FeatureIds";
            public const string Ingredients = "Ingredients";
            public const string Instructions = "Instructions";
            public const string ExternalUrl = "ExternalUrl";
            public const string UserId = "UserId";
            public const string ImageUrl = "ImageUrl";
            public static List<string> AllLower 
                = new List<string> {
                    Id.ToLower()
                    , Name.ToLower()
                    , Description.ToLower()
                    , PreparationTime.ToLower()
                    , CookingTime.ToLower()
                    , SeasonIds.ToLower()
                    , CostId.ToLower()
                    , DifficultyId.ToLower()
                    , RecipeKindId.ToLower()
                    , FeatureIds.ToLower()
                    , Ingredients.ToLower()
                    , Instructions.ToLower()
                    , ExternalUrl.ToLower()
                    , UserId.ToLower()
                    , ImageUrl.ToLower() };
        }

        public static class RecipeInstructionEntityDescription
        {
            public const string RecipeId = "RecipeId";
            public const string Instruction = "Instruction";
            public const string Order = "Order";
            public static List<string> AllLower = new List<string> { RecipeId.ToLower(), Instruction.ToLower(), Order.ToLower() };
        }

        public static class RecipeIngredientEntityDescription
        {
            public const string RecipeId = "RecipeId";
            public const string IngredientId = "IngredientId";
            public const string MeasureId = "MeasureId";
            public const string Order = "Order";
            public const string Amount = "Amount";
            public static List<string> AllLower 
                = new List<string> {
                    RecipeId.ToLower()
                    , IngredientId.ToLower()
                    , MeasureId.ToLower()
                    , Order.ToLower()
                    , Amount.ToLower() };
        }
    }
}
