using System;
using System.Runtime.Serialization;

namespace Cookbook.Entity.Recipe
{
    [DataContract]
    public class Ingredient
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string NationalName { get; set; }
        [DataMember]
        public string NationalCode { get; set; }
        [DataMember]
        public Guid IngredientKindId { get; set; }
        [DataMember]
        public decimal Calories { get; set; }
        [DataMember]
        public decimal Protein { get; set; }
        [DataMember]
        public decimal Carbohydrate { get; set; }
        [DataMember]
        public decimal Lipid { get; set; }
        [DataMember]
        public decimal Water { get; set; }
    }
}