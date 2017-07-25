namespace Cookbook.DAL.Recipe
{
    public static class CostTableDescription
    {
        public const string TableName = "Cost";
        public const string Id = "Id";
        public const string Name = "Name";
        public const string Code = "Code";
        public const string Order = "Order";
    }

    public static class DifficultyTableDescription
    {
        public const string TableName = "Difficulty";
        public const string Id = "Id";
        public const string Name = "Name";
        public const string Code = "Code";
        public const string Order = "Order";
    }

    public static class FeatureTableDescription
    {
        public const string TableName = "Feature";
        public const string Id = "Id";
        public const string Name = "Name";
        public const string Code = "Code";
    }

    public static class SeasonTableDescription
    {
        public const string TableName = "Season";
        public const string Id = "Id";
        public const string Name = "Name";
        public const string Code = "Code";
    }

    public static class RecipeKindTableDescription
    {
        public const string TableName = "RecipeKind";
        public const string Id = "Id";
        public const string Name = "Name";
        public const string Code = "Code";
        public const string Order = "Order";
    }

    public static class MeasureTableDescription
    {
        public const string TableName = "Measure";
        public const string Id = "Id";
        public const string Name = "Name";
        public const string Code = "Code";
    }

    public static class IngredientKindTableDescription
    {
        public const string TableName = "IngredientKind";
        public const string Id = "Id";
        public const string Name = "Name";
        public const string Code = "Code";
    }

    public static class IngredientTableDescription
    {
        public const string TableName = "Ingredient";
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
    }

    public static class RecipeTableDescription
    {
        public const string TableName = "Recipe";
        public const string Id = "Id";
        public const string Name = "Name";
        public const string Description = "Description";
        public const string PreparationTime = "PreparationTime";
        public const string CookingTime = "CookingTime";
        public const string CostId = "CostId";
        public const string DifficultyId = "DifficultyId";
        public const string RecipeKindId = "RecipeKindId";
        public const string ExternalUrl = "ExternalUrl";
        public const string ImageUrl = "ImageUrl";
        public const string UserId = "UserId";
        public const string CreationDate = "CreationDate";
        public const string LastUpdatedDate = "LastUpdatedDate";
    }

    public static class RecipeIngredientTableDescription
    {
        public const string TableName = "RecipeIngredient";
        public const string RecipeId = "RecipeId";
        public const string IngredientId = "IngredientId";
        public const string MeasureId = "MeasureId";
        public const string Order = "Order";
        public const string Amount = "Amount";
    }

    public static class RecipeInstructionTableDescription
    {
        public const string TableName = "RecipeInstruction";
        public const string RecipeId = "RecipeId";
        public const string Order = "Order";
        public const string Instruction = "Instruction";
    }

    public static class RecipeFeatureTableDescription
    {
        public const string TableName = "RecipeFeature";
        public const string RecipeId = "RecipeId";
        public const string FeatureId = "FeatureId";
    }

    public static class RecipeSeasonTableDescription
    {
        public const string TableName = "RecipeSeason";
        public const string RecipeId = "RecipeId";
        public const string SeasonId = "SeasonId";
    }
}
