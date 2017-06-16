using System;
using System.Runtime.Serialization;

namespace Cookbook.Entity.Recipe
{
    [DataContract]
    public class RecipeSeason
    {
        [DataMember]
        public Guid RecipeId { get; set; }
        [DataMember]
        public Guid SeasonId { get; set; }
    }
}
