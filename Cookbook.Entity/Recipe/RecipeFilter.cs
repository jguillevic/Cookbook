using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Cookbook.Entity.Recipe
{
    [DataContract]
    public class RecipeFilter
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public List<Guid> CostIds { get; set; }
        [DataMember]
        public List<Guid> DifficultyIds { get; set; }
        [DataMember]
        public List<Guid> FeatureIds { get; set; }
        [DataMember]
        public List<Guid> RecipeKindIds { get; set; }
        [DataMember]
        public List<Guid> SeasonIds { get; set; }
        [DataMember]
        public int? PreparationTime { get; set; }
        [DataMember]
        public int? CookingTime { get; set; }
    }
}