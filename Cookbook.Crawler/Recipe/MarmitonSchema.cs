using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Cookbook.Crawler.Recipe
{
    public class MarmitonRecipeSchema
    {
        [JsonProperty(PropertyName = "@context")]
        public string Context { get; set; }
        [JsonProperty(PropertyName = "@type")]
        public string Type { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "image")]
        public string Image { get; set; }
        [JsonProperty(PropertyName = "datePublished")]
        public DateTime PublishedDate { get; set; }
        [JsonProperty(PropertyName = "prepTime")]
        public string PreparationTime { get; set; }
        [JsonProperty(PropertyName = "cookTime")]
        public string CookingTime { get; set; }
        [JsonProperty(PropertyName = "recipeYield")]
        public string PersonNumber { get; set; }
        [JsonProperty(PropertyName = "recipeInstructions")]
        public string Instructions { get; set; }
        [JsonProperty(PropertyName = "recipeIngredient")]
        public List<string> Ingredients { get; set; }
        [JsonProperty(PropertyName = "aggregateRating")]
        public MarmitonAggregateRatingSchema AggragateRating { get; set; }
    }

    public class MarmitonAggregateRatingSchema
    {
        [JsonProperty(PropertyName = "@type")]
        public string Type { get; set; }
        [JsonProperty(PropertyName = "ratingValue")]
        public decimal Rating { get; set; }
        [JsonProperty(PropertyName = "reviewCount")]
        public int Review { get; set; }
        [JsonProperty(PropertyName = "worstRating")]
        public decimal MinRating { get; set; }
        [JsonProperty(PropertyName = "bestRating")]
        public decimal MaxRating { get; set; }
    }
}
