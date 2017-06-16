using System;
using System.Runtime.Serialization;

namespace Cookbook.Entity.Recipe
{
    [DataContract]
    public class RecipeInstruction
    {
        [DataMember]
        public Guid RecipeId { get; set; }
        [DataMember]
        public string Instruction { get; set; }
        [DataMember]
        public int Order { get; set; }
    }
}