using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Cookbook.Entity.Recipe
{
    [DataContract]
    public class Recipe
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public List<RecipeInstruction> Instructions { get; set; }
        [DataMember]
        public int PreparationTime { get; set; }
        [DataMember]
        public int CookingTime { get; set; }
        [DataMember]
        public List<Guid> SeasonIds { get; set; }
        [DataMember]
        public Guid CostId { get; set; }
        [DataMember]
        public Guid DifficultyId { get; set; }
        [DataMember]
        public Guid RecipeKindId { get; set; }
        [DataMember]
        public List<Guid> FeatureIds { get; set; }
        [DataMember]
        public List<RecipeIngredient> Ingredients { get; set; }
        [DataMember]
        public string ExternalUrl { get; set; }
        [DataMember]
        public Guid? UserId { get; set; }
    }
}