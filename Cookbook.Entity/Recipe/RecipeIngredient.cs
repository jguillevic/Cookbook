using System;
using System.Runtime.Serialization;

namespace Cookbook.Entity.Recipe
{
    [DataContract]
    public class RecipeIngredient
    {
        [DataMember]
        public Guid RecipeId { get; set; }
        [DataMember]
        public Guid IngredientId { get; set; }
        [DataMember]
        public Guid MeasureId { get; set; }
        [DataMember]
        public int Order { get; set; }
        [DataMember]
        public decimal Amount { get; set; }
    }
}