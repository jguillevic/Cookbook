using Cookbook.DAL.Database;
using Cookbook.Entity.Recipe;
using System.Collections.Generic;
using System.Data;
using Tools.DAL.Database;
using Tools.DAL.QueryBuilder;
using Tools.DAL.QueryBuilder.Enum;
using static Cookbook.Entity.Recipe.RecipeEntityDescriptions;

namespace Cookbook.DAL.Recipe
{
    public class RecipeKindDAL : DbDAL<SqlConnectionProvider>
    {
        private List<string> _fields;

        public List<RecipeKind> Load(List<string> fields)
        {
            _fields = fields;

            var sqb = new SelectQueryBuilder();

            AddQueriedFields(sqb);

            sqb.AddFrom(RecipeKindTableDescription.TableName);

            sqb.AddOrderBy(RecipeKindTableDescription.Order, Sorting.Ascending);

            return sqb.Read<RecipeKind, List<RecipeKind>>(DefaultConnectProvider, GetRecipeKindFromIDataRecord);
        }

        private void AddQueriedFields(SelectQueryBuilder sqb)
        {
            if (_fields.Contains(RecipeKindEntityDescription.Id.ToLower()))
                sqb.AddQueriedField(RecipeKindTableDescription.Id);
            if (_fields.Contains(RecipeKindEntityDescription.Name.ToLower()))
                sqb.AddQueriedField(RecipeKindTableDescription.Name);
            if (_fields.Contains(RecipeKindEntityDescription.Code.ToLower()))
                sqb.AddQueriedField(RecipeKindTableDescription.Code);
        }

        private RecipeKind GetRecipeKindFromIDataRecord(IDataRecord dataRecord)
        {
            var recipeKind = new RecipeKind();

            if (_fields.Contains(RecipeKindEntityDescription.Id.ToLower()))
                recipeKind.Id = dataRecord.GetGuid(RecipeKindTableDescription.Id);
            if (_fields.Contains(RecipeKindEntityDescription.Name.ToLower()))
                recipeKind.Name = dataRecord.GetString(RecipeKindTableDescription.Name);
            if (_fields.Contains(RecipeKindEntityDescription.Code.ToLower()))
                recipeKind.Code = dataRecord.GetString(RecipeKindTableDescription.Code);

            return recipeKind;
        }
    }
}
