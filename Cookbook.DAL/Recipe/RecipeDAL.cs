﻿using System.Collections.Generic;
using System.Data;
using System.Linq;
using Tools.DAL.Database;
using Tools.DAL.QueryBuilder;
using Tools.DAL.QueryBuilder.Enum;
using Tools.Helper.Enum;

namespace Cookbook.DAL.Recipe
{
    using Database;
    using Entity.Recipe;

    public class RecipeDAL : DbDAL<SqlConnectionProvider>
    {
        private RecipeIngredientDAL _recipeIngredientDAL;
        private RecipeInstructionDAL _recipeInstructionDAL;
        private RecipeDifficultyDAL _recipeDifficultyDAL;
        private RecipeCostDAL _recipeCostDAL;
        private RecipeSeasonDAL _recipeSeasonDAL;
        private RecipeFeatureDAL _recipeFeatureDAL;
        private RecipeRecipeKindDAL _recipeRecipeKindDAL;

        public RecipeDAL() : base()
        {
            _recipeIngredientDAL = new RecipeIngredientDAL();
            _recipeInstructionDAL = new RecipeInstructionDAL();
            _recipeDifficultyDAL = new RecipeDifficultyDAL();
            _recipeCostDAL = new RecipeCostDAL();
            _recipeSeasonDAL = new RecipeSeasonDAL();
            _recipeFeatureDAL = new RecipeFeatureDAL();
            _recipeRecipeKindDAL = new RecipeRecipeKindDAL();
        }

        public List<Recipe> Load(RecipeFilter filter)
        {
            List<Recipe> recipes = null;

            using (var scope = TransactionScopeHelper.GetTransactionScope())
            {
                recipes = LoadRecipes(filter);

                if (recipes != null && recipes.Count > 0)
                {
                    var recipeIds = recipes.Select(item => item.Id);

                    var recipeIngredients = _recipeIngredientDAL.Load(recipeIds);
                    var recipeInstructions = _recipeInstructionDAL.Load(recipeIds);
                    var recipeDifficulties = _recipeDifficultyDAL.Load(recipeIds);
                    var recipeCosts = _recipeCostDAL.Load(recipeIds);
                    var recipeSeasons = _recipeSeasonDAL.Load(recipeIds);
                    var recipeFeatures = _recipeFeatureDAL.Load(recipeIds);
                    var recipeRecipeKinds = _recipeRecipeKindDAL.Load(recipeIds);

                    foreach (var recipe in recipes)
                    {
                        recipe.Ingredients = new List<RecipeIngredient>(recipeIngredients.Where(item => item.RecipeId == recipe.Id));
                        recipe.Instructions = new List<RecipeInstruction>(recipeInstructions.Where(item => item.RecipeId == recipe.Id));

                        var difficulties = recipeDifficulties.Where(item => item.RecipeId == recipe.Id).Select(item => item.Difficulty);
                        if (difficulties.Any())
                            recipe.Difficulty = difficulties.Aggregate((x, y) => x |= y);

                        var costs = recipeCosts.Where(item => item.RecipeId == recipe.Id).Select(item => item.Cost);
                        if (costs.Any())
                            recipe.Cost = costs.Aggregate((x, y) => x |= y);

                        var seasons = recipeSeasons.Where(item => item.RecipeId == recipe.Id).Select(item => item.Season);
                        if (seasons.Any())
                            recipe.Season = seasons.Aggregate((x, y) => x |= y);

                        var features = recipeFeatures.Where(item => item.RecipeId == recipe.Id).Select(item => item.Feature);
                        if (features.Any())
                            recipe.Feature = features.Aggregate((x, y) => x |= y);

                        var recipeKinds = recipeRecipeKinds.Where(item => item.RecipeId == recipe.Id).Select(item => item.RecipeKind);
                        if (recipeKinds.Any())
                            recipe.RecipeKind = recipeKinds.Aggregate((x, y) => x |= y);
                    }
                }

                scope.Complete();
            }

            return recipes;
        }

        private List<Recipe> LoadRecipes(RecipeFilter filter)
        {
            var sqb = new SelectQueryBuilder();

            sqb.AddDistinct();

            sqb.AddQueriedField(RecipeTableDescription.Id);
            sqb.AddQueriedField(RecipeTableDescription.Name);
            sqb.AddQueriedField(RecipeTableDescription.Description);
            sqb.AddQueriedField(RecipeTableDescription.PreparationTime);
            sqb.AddQueriedField(RecipeTableDescription.CookingTime);
            sqb.AddQueriedField(RecipeTableDescription.ExternalUrl);
            sqb.AddQueriedField(RecipeTableDescription.UserId);

            sqb.AddFrom(RecipeTableDescription.TableName);
            sqb.AddJoin(JoinType.LeftJoin, RecipeTableDescription.TableName, new List<string> { RecipeTableDescription.Id }, Comparison.Equals, RecipeSeasonTableDescription.TableName, new List<string> { RecipeSeasonTableDescription.RecipeId });
            sqb.AddJoin(JoinType.LeftJoin, RecipeTableDescription.TableName, new List<string> { RecipeTableDescription.Id }, Comparison.Equals, RecipeCostTableDescription.TableName, new List<string> { RecipeCostTableDescription.RecipeId });
            sqb.AddJoin(JoinType.LeftJoin, RecipeTableDescription.TableName, new List<string> { RecipeTableDescription.Id }, Comparison.Equals, RecipeFeatureTableDescription.TableName, new List<string> { RecipeFeatureTableDescription.RecipeId });
            sqb.AddJoin(JoinType.LeftJoin, RecipeTableDescription.TableName, new List<string> { RecipeTableDescription.Id }, Comparison.Equals, RecipeDifficultyTableDescription.TableName, new List<string> { RecipeDifficultyTableDescription.RecipeId });
            sqb.AddJoin(JoinType.LeftJoin, RecipeTableDescription.TableName, new List<string> { RecipeTableDescription.Id }, Comparison.Equals, RecipeRecipeKindTableDescription.TableName, new List<string> { RecipeRecipeKindTableDescription.RecipeId });

            bool hasWhere = false;
            hasWhere = AddNameCondition(sqb, filter, hasWhere);
            hasWhere = AddCookingTimeCondition(sqb, filter, hasWhere);
            hasWhere = AddPreparationTimeCondition(sqb, filter, hasWhere);
            hasWhere = AddDifficultyCondition(sqb, filter, hasWhere);
            hasWhere = AddCostCondition(sqb, filter, hasWhere);
            hasWhere = AddSeasonCondition(sqb, filter, hasWhere);
            hasWhere = AddFeatureCondition(sqb, filter, hasWhere);
            hasWhere = AddRecipeKindCondition(sqb, filter, hasWhere);

            sqb.AddOrderBy(RecipeTableDescription.Id, Sorting.Ascending);

            var recipes = sqb.Read <Recipe, List<Recipe>>(DefaultConnectProvider, GetRecipeFromIDataRecord);
            
            return recipes;
        }

        private Recipe GetRecipeFromIDataRecord(IDataRecord dataRecord)
        {
            var recipe = new Recipe();

            recipe.Id = dataRecord.GetGuid(RecipeTableDescription.Id);
            recipe.Name = dataRecord.GetString(RecipeTableDescription.Name);
            recipe.Description = dataRecord.GetString(RecipeTableDescription.Description);
            recipe.PreparationTime = dataRecord.GetInt32(RecipeTableDescription.PreparationTime);
            recipe.CookingTime = dataRecord.GetInt32(RecipeTableDescription.CookingTime);
            recipe.ExternalUrl = dataRecord.GetNullableString(RecipeTableDescription.ExternalUrl);
            recipe.UserId = dataRecord.GetNullableGuid(RecipeTableDescription.UserId);

            return recipe;
        }

        private static bool AddNameCondition(SelectQueryBuilder sqb, RecipeFilter filter, bool hasWhere)
        {
            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                if (!hasWhere)
                {
                    sqb.AddWhere(RecipeTableDescription.Name, Comparison.Like, filter.Name);
                }
                else
                {
                    sqb.AddCondition(LogicOperator.And, RecipeTableDescription.Name, Comparison.Like, filter.Name);
                }

                return true;
            }

            return false;
        }

        private static bool AddCookingTimeCondition(SelectQueryBuilder sqb, RecipeFilter filter, bool hasWhere)
        {
            if (filter.CookingTime.HasValue)
            {
                if (!hasWhere)
                {
                    sqb.AddWhere(RecipeTableDescription.CookingTime, Comparison.LessOrEquals, filter.CookingTime);
                }
                else
                {
                    sqb.AddCondition(LogicOperator.And, RecipeTableDescription.CookingTime, Comparison.LessOrEquals, filter.CookingTime);
                }

                return true;
            }

            return false;
        }

        private static bool AddPreparationTimeCondition(SelectQueryBuilder sqb, RecipeFilter filter, bool hasWhere)
        {
            if (filter.PreparationTime.HasValue)
            {
                if (!hasWhere)
                {
                    sqb.AddWhere(RecipeTableDescription.PreparationTime, Comparison.LessOrEquals, filter.PreparationTime);
                }
                else
                {
                    sqb.AddCondition(LogicOperator.And, RecipeTableDescription.PreparationTime, Comparison.LessOrEquals, filter.PreparationTime);
                }

                return true;
            }

            return false;
        }

        private static bool AddDifficultyCondition(SelectQueryBuilder sqb, RecipeFilter filter, bool hasWhere)
        {
            if (filter.Difficulty != Difficulty.None)
            {
                if (!hasWhere)
                {
                    sqb.AddWhere(RecipeDifficultyTableDescription.DifficultyId, Comparison.In, EnumHelper.GetFlags(filter.Difficulty));
                }
                else
                {
                    sqb.AddCondition(LogicOperator.And, RecipeDifficultyTableDescription.DifficultyId, Comparison.In, EnumHelper.GetFlags(filter.Difficulty));
                }

                return true;
            }

            return false;
        }

        private static bool AddCostCondition(SelectQueryBuilder sqb, RecipeFilter filter, bool hasWhere)
        {
            if (filter.Cost != Cost.None)
            {
                if (!hasWhere)
                {
                    sqb.AddWhere(RecipeCostTableDescription.CostId, Comparison.In, EnumHelper.GetFlags(filter.Cost));
                }
                else
                {
                    sqb.AddCondition(LogicOperator.And, RecipeCostTableDescription.CostId, Comparison.In, EnumHelper.GetFlags(filter.Cost));
                }

                return true;
            }

            return false;
        }

        private static bool AddSeasonCondition(SelectQueryBuilder sqb, RecipeFilter filter, bool hasWhere)
        {
            if (filter.Season != Season.None)
            {
                if (!hasWhere)
                {
                    sqb.AddWhere(RecipeSeasonTableDescription.SeasonId, Comparison.In, EnumHelper.GetFlags(filter.Season));
                }
                else
                {
                    sqb.AddCondition(LogicOperator.And, RecipeSeasonTableDescription.SeasonId, Comparison.In, EnumHelper.GetFlags(filter.Season));
                }

                return true;
            }

            return false;
        }

        private static bool AddFeatureCondition(SelectQueryBuilder sqb, RecipeFilter filter, bool hasWhere)
        {
            if (filter.Feature != Feature.None)
            {
                if (!hasWhere)
                {
                    sqb.AddWhere(RecipeFeatureTableDescription.FeatureId, Comparison.In, EnumHelper.GetFlags(filter.Feature));
                }
                else
                {
                    sqb.AddCondition(LogicOperator.And, RecipeFeatureTableDescription.FeatureId, Comparison.In, EnumHelper.GetFlags(filter.Feature));
                }

                return true;
            }

            return false;
        }

        private static bool AddRecipeKindCondition(SelectQueryBuilder sqb, RecipeFilter filter, bool hasWhere)
        {
            if (filter.RecipeKind != RecipeKind.None)
            {
                if (!hasWhere)
                {
                    sqb.AddWhere(RecipeRecipeKindTableDescription.RecipeKindId, Comparison.In, EnumHelper.GetFlags(filter.RecipeKind));
                }
                else
                {
                    sqb.AddCondition(LogicOperator.And, RecipeRecipeKindTableDescription.RecipeKindId, Comparison.In, EnumHelper.GetFlags(filter.RecipeKind));
                }

                return true;
            }

            return false;
        }
        
        public void Add(IEnumerable<Recipe> recipes)
        {
            using (var scope = TransactionScopeHelper.GetTransactionScope())
            {
                AddRecipes(recipes);

                var recipeIngredients = recipes.SelectMany(item => item.Ingredients);
                if (recipeIngredients != null && recipeIngredients.Any())
                {
                    _recipeIngredientDAL.Add(recipeIngredients);
                }

                var recipeInstructions = recipes.SelectMany(item => item.Instructions);
                if (recipeInstructions != null && recipeInstructions.Any())
                {
                    _recipeInstructionDAL.Add(recipeInstructions);
                }

                var recipeCosts = new List<RecipeCost>();
                var recipeDifficulties = new List<RecipeDifficulty>();
                var recipeSeasons = new List<RecipeSeason>();
                var recipeFeatures = new List<RecipeFeature>();
                var recipeRecipeKinds = new List<RecipeRecipeKind>();

                foreach (var recipe in recipes)
                {
                    foreach (Cost cost in EnumHelper.GetFlags(recipe.Cost))
                        recipeCosts.Add(new RecipeCost { RecipeId = recipe.Id, Cost = cost });

                    foreach (Difficulty difficulty in EnumHelper.GetFlags(recipe.Difficulty))
                        recipeDifficulties.Add(new RecipeDifficulty { RecipeId = recipe.Id, Difficulty = difficulty });

                    foreach (Season season in EnumHelper.GetFlags(recipe.Season))
                        recipeSeasons.Add(new RecipeSeason { RecipeId = recipe.Id, Season = season });

                    foreach (Feature feature in EnumHelper.GetFlags(recipe.Feature))
                        recipeFeatures.Add(new RecipeFeature { RecipeId = recipe.Id, Feature = feature });

                    foreach (RecipeKind recipeKind in EnumHelper.GetFlags(recipe.RecipeKind))
                        recipeRecipeKinds.Add(new RecipeRecipeKind { RecipeId = recipe.Id, RecipeKind = recipeKind });
                }

                if (recipeCosts.Any())
                    _recipeCostDAL.Add(recipeCosts);
                if (recipeDifficulties.Any())
                    _recipeDifficultyDAL.Add(recipeDifficulties);
                if (recipeSeasons.Any())
                    _recipeSeasonDAL.Add(recipeSeasons);
                if (recipeFeatures.Any())
                    _recipeFeatureDAL.Add(recipeFeatures);
                if (recipeRecipeKinds.Any())
                    _recipeRecipeKindDAL.Add(recipeRecipeKinds);


                scope.Complete();
            }
        }

        private void AddRecipes(IEnumerable<Recipe> recipes)
        {
            var iqb = new InsertQueryBuilder();

            iqb.SetTableName(RecipeTableDescription.TableName);

            iqb.AddInsertFields(GetRecipeColumnNames());

            iqb.AddInsertValues(GetRecipeValues(recipes));

            iqb.Execute(DefaultConnectProvider);
        }

        public void Update(IEnumerable<Recipe> recipes)
        {
            using (var scope = TransactionScopeHelper.GetTransactionScope())
            {
                UpdateRecipes(recipes);

                scope.Complete();
            }
        }

        private void UpdateRecipes(IEnumerable<Recipe> recipes)
        {
            var uqb = new UpdateQueryBuilder();

            uqb.SetTableName(RecipeTableDescription.TableName);

            uqb.AddSettedFields(new List<string> { RecipeTableDescription.Id }, GetRecipeColumnNames(), GetRecipeValues(recipes));

            uqb.Execute(DefaultConnectProvider);
        }

        private List<string> GetRecipeColumnNames()
        {
            return new List<string> {
                    RecipeTableDescription.Id
                    , RecipeTableDescription.Name
                    , RecipeTableDescription.Description
                    , RecipeTableDescription.CookingTime
                    , RecipeTableDescription.PreparationTime
                    , RecipeTableDescription.ExternalUrl
                    , RecipeTableDescription.UserId };
        }

        private List<List<object>> GetRecipeValues(IEnumerable<Recipe> recipes)
        {
            var values = new List<List<object>>();
            List<object> value;

            foreach (var recipe in recipes)
            {
                value = new List<object>();

                value.Add(recipe.Id);
                value.Add(recipe.Name);
                value.Add(recipe.Description);
                value.Add(recipe.CookingTime);
                value.Add(recipe.PreparationTime);
                value.Add(recipe.ExternalUrl);
                value.Add(recipe.UserId);

                values.Add(value);
            }

            return values;
        }
    }
}