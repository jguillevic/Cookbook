using Cookbook.DAL.Database;
using Cookbook.Entity.Recipe;
using System;
using System.Collections.Generic;
using System.Data;
using Tools.DAL.Database;
using Tools.DAL.QueryBuilder;
using Tools.DAL.QueryBuilder.Enum;

namespace Cookbook.DAL.Recipe
{
    public class RecipeInstructionDAL : DbDAL<SqlConnectionProvider>
    {
        internal HashSet<RecipeInstruction> Load(IEnumerable<Guid> recipeIds)
        {
            var sqb = new SelectQueryBuilder();

            sqb.AddQueriedField(RecipeInstructionTableDescription.RecipeId);
            sqb.AddQueriedField(RecipeInstructionTableDescription.Instruction);
            sqb.AddQueriedField(RecipeInstructionTableDescription.Order);

            sqb.AddFrom(RecipeInstructionTableDescription.TableName);

            sqb.AddWhere(RecipeInstructionTableDescription.RecipeId, Comparison.In, recipeIds);

            var recipeInstructions = sqb.Read<RecipeInstruction, HashSet<RecipeInstruction>>(DefaultConnectProvider, GetRecipeInstructionFromIDataRecord);

            return recipeInstructions;
        }

        private RecipeInstruction GetRecipeInstructionFromIDataRecord(IDataRecord dataRecord)
        {
            var recipeInstruction = new RecipeInstruction();

            recipeInstruction.RecipeId = dataRecord.GetGuid(RecipeInstructionTableDescription.RecipeId);
            recipeInstruction.Instruction = dataRecord.GetString(RecipeInstructionTableDescription.Instruction);
            recipeInstruction.Order = dataRecord.GetInt32(RecipeInstructionTableDescription.Order);

            return recipeInstruction;
        }

        internal void Add(IEnumerable<RecipeInstruction> recipeInstructions)
        {
            var iqb = new InsertQueryBuilder();

            iqb.SetTableName(RecipeInstructionTableDescription.TableName);

            iqb.AddInsertFields(
                new List<string> {
                    RecipeInstructionTableDescription.RecipeId
                    , RecipeInstructionTableDescription.Instruction
                    , RecipeInstructionTableDescription.Order
                });
            iqb.AddInsertValues(GetRecipeInstructionValues(recipeInstructions));

            iqb.Execute(DefaultConnectProvider);
        }

        private List<List<object>> GetRecipeInstructionValues(IEnumerable<RecipeInstruction> recipeInstructions)
        {
            var values = new List<List<object>>();
            List<object> value;

            foreach (var recipeInstruction in recipeInstructions)
            {
                value = new List<object>();

                value.Add(recipeInstruction.RecipeId);
                value.Add(recipeInstruction.Instruction);
                value.Add(recipeInstruction.Order);

                values.Add(value);
            }

            return values;
        }

        internal void Delete(IEnumerable<Guid> recipeIds)
        {
            var dqb = new DeleteQueryBuilder();

            dqb.AddFrom(RecipeInstructionTableDescription.TableName);
            dqb.AddWhere(RecipeInstructionTableDescription.RecipeId, Comparison.In, recipeIds);

            dqb.Execute(DefaultConnectProvider);
        }
    }
}