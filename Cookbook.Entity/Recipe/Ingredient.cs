﻿using System;
using System.Collections.Generic;

namespace Cookbook.Entity.Recipe
{
    public class Ingredient
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<string> AlternativeNames { get; set; }
        public string Code { get; set; }
        public Guid IngredientKindId { get; set; }
        public decimal Calories { get; set; }
        public decimal Protein { get; set; }
        public decimal Carbohydrate { get; set; }
        public decimal Lipid { get; set; }
        public decimal Water { get; set; }
        public decimal Fiber { get; set; }
    }
}