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
    public class IngredientKindDAL : DbDAL<SqlConnectionProvider>
    {
        private List<string> _fields;

        public List<IngredientKind> Load(List<string> fields)
        {
            _fields = fields;

            var sqb = new SelectQueryBuilder();

            AddQueriedFields(sqb);

            sqb.AddFrom(IngredientKindTableDescription.TableName);

            sqb.AddOrderBy(IngredientKindTableDescription.Name, Sorting.Ascending);

            return sqb.Read<IngredientKind, List<IngredientKind>>(DefaultConnectProvider, GetIngredientKindFromIDataRecord);
        }

        private void AddQueriedFields(SelectQueryBuilder sqb)
        {
            if (_fields.Contains(IngredientKindEntityDescription.Id.ToLower()))
                sqb.AddQueriedField(IngredientKindTableDescription.Id);
            if (_fields.Contains(IngredientKindEntityDescription.Name.ToLower()))
                sqb.AddQueriedField(IngredientKindTableDescription.Name);
            if (_fields.Contains(IngredientKindEntityDescription.Code.ToLower()))
                sqb.AddQueriedField(IngredientKindTableDescription.Code);
        }

        private IngredientKind GetIngredientKindFromIDataRecord(IDataRecord dataRecord)
        {
            var ingredientKind = new IngredientKind();

            if (_fields.Contains(IngredientKindEntityDescription.Id.ToLower()))
                ingredientKind.Id = dataRecord.GetGuid(IngredientKindTableDescription.Id);
            if (_fields.Contains(IngredientKindEntityDescription.Name.ToLower()))
                ingredientKind.Name = dataRecord.GetString(IngredientKindTableDescription.Name);
            if (_fields.Contains(IngredientKindEntityDescription.Code.ToLower()))
                ingredientKind.Code = dataRecord.GetString(IngredientKindTableDescription.Code);

            return ingredientKind;
        }
    }
}
