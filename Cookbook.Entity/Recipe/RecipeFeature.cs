using System;
using System.Runtime.Serialization;

namespace Cookbook.Entity.Recipe
{
    [DataContract]
    public class RecipeFeature
    {
        [DataMember]
        public Guid RecipeId { get; set; }
        [DataMember]
        public Guid FeatureId { get; set; }
    }
}
