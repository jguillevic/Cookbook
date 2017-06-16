using System;
using System.Runtime.Serialization;

namespace Cookbook.Entity.Recipe
{
    [DataContract]
    public class Measure
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public string Name { get; set; }
    }
}