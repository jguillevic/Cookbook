using Cookbook.DAL.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Tools.DAL.Database;
using Tools.DAL.QueryBuilder;
using Tools.DAL.QueryBuilder.Enum;

namespace Cookbook.DAL.Recipe
{
    using Entity.Recipe;

    public class RecipeDAL : DbDAL<SqlConnectionProvider>
    {
        private RecipeIngredientDAL _recipeIngredientDAL;
        private RecipeInstructionDAL _recipeInstructionDAL;
        private RecipeSeasonDAL _recipeSeasonDAL;
        private RecipeFeatureDAL _recipeFeatureDAL;

        public RecipeDAL() : base()
        {
            _recipeIngredientDAL = new RecipeIngredientDAL();
            _recipeInstructionDAL = new RecipeInstructionDAL();
            _recipeSeasonDAL = new RecipeSeasonDAL();
            _recipeFeatureDAL = new RecipeFeatureDAL();
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
                    var recipeSeasons = _recipeSeasonDAL.Load(recipeIds);
                    var recipeFeatures = _recipeFeatureDAL.Load(recipeIds);

                    foreach (var recipe in recipes)
                    {
                        recipe.Ingredients = new List<RecipeIngredient>(recipeIngredients.Where(item => item.RecipeId == recipe.Id));
                        recipe.Instructions = new List<RecipeInstruction>(recipeInstructions.Where(item => item.RecipeId == recipe.Id));

                        var seasonIds = recipeSeasons.Where(item => item.RecipeId == recipe.Id).Select(item => item.SeasonId);
                        if (seasonIds.Any())
                            recipe.SeasonIds = new List<Guid>(seasonIds);

                        var featureIds = recipeFeatures.Where(item => item.RecipeId == recipe.Id).Select(item => item.FeatureId);
                        if (featureIds.Any())
                            recipe.FeatureIds = new List<Guid>(featureIds);
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
            sqb.AddQueriedField(RecipeTableDescription.CostId);
            sqb.AddQueriedField(RecipeTableDescription.DifficultyId);
            sqb.AddQueriedField(RecipeTableDescription.RecipeKindId);
            sqb.AddQueriedField(RecipeTableDescription.ExternalUrl);
            sqb.AddQueriedField(RecipeTableDescription.UserId);

            sqb.AddFrom(RecipeTableDescription.TableName);
            sqb.AddJoin(JoinType.LeftJoin, RecipeTableDescription.TableName, new List<string> { RecipeTableDescription.Id }, Comparison.Equals, RecipeSeasonTableDescription.TableName, new List<string> { RecipeSeasonTableDescription.RecipeId });
            sqb.AddJoin(JoinType.LeftJoin, RecipeTableDescription.TableName, new List<string> { RecipeTableDescription.Id }, Comparison.Equals, RecipeFeatureTableDescription.TableName, new List<string> { RecipeFeatureTableDescription.RecipeId });

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
            recipe.CostId = dataRecord.GetGuid(RecipeTableDescription.CostId);
            recipe.DifficultyId = dataRecord.GetGuid(RecipeTableDescription.DifficultyId);
            recipe.RecipeKindId = dataRecord.GetGuid(RecipeTableDescription.RecipeKindId);
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
            if (filter.DifficultyIds.Any())
            {
                if (!hasWhere)
                {
                    sqb.AddWhere(RecipeTableDescription.DifficultyId, Comparison.In, filter.DifficultyIds);
                }
                else
                {
                    sqb.AddCondition(LogicOperator.And, RecipeTableDescription.DifficultyId, Comparison.In, filter.DifficultyIds);
                }

                return true;
            }

            return false;
        }

        private static bool AddCostCondition(SelectQueryBuilder sqb, RecipeFilter filter, bool hasWhere)
        {
            if (filter.CostIds.Any())
            {
                if (!hasWhere)
                {
                    sqb.AddWhere(RecipeTableDescription.CostId, Comparison.In, filter.CostIds);
                }
                else
                {
                    sqb.AddCondition(LogicOperator.And, RecipeTableDescription.CostId, Comparison.In, filter.CostIds);
                }

                return true;
            }

            return false;
        }

        private static bool AddSeasonCondition(SelectQueryBuilder sqb, RecipeFilter filter, bool hasWhere)
        {
            if (filter.SeasonIds.Any())
            {
                if (!hasWhere)
                {
                    sqb.AddWhere(RecipeSeasonTableDescription.SeasonId, Comparison.In, filter.SeasonIds);
                }
                else
                {
                    sqb.AddCondition(LogicOperator.And, RecipeSeasonTableDescription.SeasonId, Comparison.In, filter.SeasonIds);
                }

                return true;
            }

            return false;
        }

        private static bool AddFeatureCondition(SelectQueryBuilder sqb, RecipeFilter filter, bool hasWhere)
        {
            if (filter.FeatureIds.Any())
            {
                if (!hasWhere)
                {
                    sqb.AddWhere(RecipeFeatureTableDescription.FeatureId, Comparison.In, filter.FeatureIds);
                }
                else
                {
                    sqb.AddCondition(LogicOperator.And, RecipeFeatureTableDescription.FeatureId, Comparison.In, filter.FeatureIds);
                }

                return true;
            }

            return false;
        }

        private static bool AddRecipeKindCondition(SelectQueryBuilder sqb, RecipeFilter filter, bool hasWhere)
        {
            if (filter.RecipeKindIds.Any())
            {
                if (!hasWhere)
                {
                    sqb.AddWhere(RecipeTableDescription.RecipeKindId, Comparison.In, filter.RecipeKindIds);
                }
                else
                {
                    sqb.AddCondition(LogicOperator.And, RecipeTableDescription.RecipeKindId, Comparison.In, filter.RecipeKindIds);
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

                var recipeSeasons = new List<RecipeSeason>();
                var recipeFeatures = new List<RecipeFeature>();

                foreach (var recipe in recipes)
                {
                    foreach (var seasonId in recipe.SeasonIds)
                        recipeSeasons.Add(new RecipeSeason { RecipeId = recipe.Id, SeasonId = seasonId });

                    foreach (var featureId in recipe.FeatureIds)
                        recipeFeatures.Add(new RecipeFeature { RecipeId = recipe.Id, FeatureId = featureId });
                }

                if (recipeSeasons.Any())
                    _recipeSeasonDAL.Add(recipeSeasons);
                if (recipeFeatures.Any())
                    _recipeFeatureDAL.Add(recipeFeatures);

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

                // TODO : Faire des méthodes par concept.
                // Idem pour Add.
                // Faire du MVVM.
                // Faire des ICommand.
                // Plus de code behind dans le xaml.

                var recipeIds = recipes.Select(item => item.Id);

                _recipeIngredientDAL.Delete(recipeIds);
                _recipeInstructionDAL.Delete(recipeIds);
                _recipeSeasonDAL.Delete(recipeIds);
                _recipeFeatureDAL.Delete(recipeIds);

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

                var recipeSeasons = new List<RecipeSeason>();
                var recipeFeatures = new List<RecipeFeature>();

                foreach (var recipe in recipes)
                {
                    foreach (var seasonId in recipe.SeasonIds)
                        recipeSeasons.Add(new RecipeSeason { RecipeId = recipe.Id, SeasonId = seasonId });

                    foreach (var featureId in recipe.FeatureIds)
                        recipeFeatures.Add(new RecipeFeature { RecipeId = recipe.Id, FeatureId = featureId });
                }

                if (recipeSeasons.Any())
                    _recipeSeasonDAL.Add(recipeSeasons);
                if (recipeFeatures.Any())
                    _recipeFeatureDAL.Add(recipeFeatures);

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
                    , RecipeTableDescription.CostId
                    , RecipeTableDescription.DifficultyId
                    , RecipeTableDescription.RecipeKindId
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
                value.Add(recipe.CostId);
                value.Add(recipe.DifficultyId);
                value.Add(recipe.RecipeKindId);
                value.Add(recipe.ExternalUrl);
                value.Add(recipe.UserId);

                values.Add(value);
            }

            return values;
        }
    }
}