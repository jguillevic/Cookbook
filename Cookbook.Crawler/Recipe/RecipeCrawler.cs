using Cookbook.Entity.Recipe;
using Cookbook.Rule.Recipe;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Cookbook.Crawler.Recipe
{
    public class RecipeCrawler
    {
        AutoResetEvent _autoEvent = new AutoResetEvent(false);

        public bool IsCrawling { get; set; }
        public bool IsPaused { get; set; }

        public Action<string, IList<Measure>> MeasureNotRecognizedAction { get; set; }
        public Action<string, IList<Ingredient>> IngredientNotRecognizedAction { get; set; }
        public Action CrawlStartedAction { get; set; }
        public Action CrawlFinishedAction { get; set; }
        public Action<int> CrawlRecipeStartedAction { get; set; }

        public RecipeCrawler()
        {
            _autoEvent = new AutoResetEvent(false);

            IsCrawling = false;
            IsPaused = false;
        }

        public async Task<List<Entity.Recipe.Recipe>> CrawlAsync(int number, IList<Measure> measures, IList<Ingredient> ingredients)
        {
            return await Task.Run(() => InnerCrawlAsync(number, measures, ingredients));
        }

        private async Task<List<Entity.Recipe.Recipe>> InnerCrawlAsync(int number, IList<Measure> measures, IList<Ingredient> ingredients)
        {
            IsCrawling = true;
            CrawlStartedAction?.Invoke();

            var recipes = new List<Entity.Recipe.Recipe>();

            HtmlWeb web = new HtmlWeb();
            HtmlDocument document;
            IEnumerable<HtmlNode> nodes;
            string content;
            MarmitonRecipeSchema marmitonRecipe;
            Entity.Recipe.Recipe recipe;
            string url;

            for (int i = 0; i < number; i++)
            {
                CrawlRecipeStartedAction?.Invoke(i + 1);

                document = await web.LoadFromWebAsync("http://www.marmiton.org/recettes/recette-hasard.aspx");

                nodes = document.DocumentNode.Descendants("meta").Where(x => x.Attributes.Contains("property") && x.Attributes["property"].Value == "og:url");
                content = nodes.First().Attributes["content"].Value;
                url = string.Format(CultureInfo.InvariantCulture, "www.marmiton.org{0}", content);

                nodes = document.DocumentNode.Descendants("script").Where(x => x.Attributes.Contains("type") && x.Attributes["type"].Value == "application/ld+json");
                content = nodes.First().InnerText.Replace("\r\n", string.Empty).Replace("//]]>", string.Empty).Replace("//<![CDATA[", string.Empty).Trim();

                marmitonRecipe = JsonConvert.DeserializeObject<MarmitonRecipeSchema>(content);

                recipe = RecipeRule.GetDefault();
                recipe.Name = marmitonRecipe.Name;
                recipe.Description = marmitonRecipe.Name;
                recipe.ImageUrl = marmitonRecipe.Image;
                recipe.CookingTime = XmlConvert.ToTimeSpan(marmitonRecipe.CookingTime).Minutes;
                recipe.PreparationTime = XmlConvert.ToTimeSpan(marmitonRecipe.PreparationTime).Minutes;
                recipe.ExternalUrl = url;

                var instructions = Regex.Split(marmitonRecipe.Instructions, @"(?<=[\.!\?])\s+");
                for (int j = 0; j < instructions.Length; j++)
                    recipe.Instructions.Add(new RecipeInstruction { RecipeId = recipe.Id, Order = j + 1, Instruction = instructions[j].Trim() });

                int personNumber = 1;
                Regex regex = new Regex(@"^-?\d+(?:\.\d+)?");
                Match match = regex.Match(marmitonRecipe.PersonNumber);
                if (match.Success)
                    personNumber = Convert.ToInt32(match.Value, CultureInfo.InvariantCulture);

                for (int j = 0; j < marmitonRecipe.Ingredients.Count; j++)
                {
                    var recipeIngredient = new RecipeIngredient { RecipeId = recipe.Id, Order = j + 1 };
                    var marmitonRecipeIng = marmitonRecipe.Ingredients[j];

                    regex = new Regex(@"^-?\d+(?:\.\d+)?");
                    match = regex.Match(marmitonRecipeIng);
                    if (match.Success)
                    {
                        recipeIngredient.Amount = Convert.ToDecimal(match.Value, CultureInfo.InvariantCulture) / personNumber;
                        marmitonRecipeIng = marmitonRecipeIng.Replace(match.Value, string.Empty);
                    }

                    string measureStr;
                    bool isRecognized = false;
                    foreach (var measure in measures)
                    {
                        if (!string.IsNullOrWhiteSpace(measure.Name))
                        {
                            measureStr = string.Concat(" ", measure.Name, " ");
                            if (marmitonRecipeIng.IndexOf(measureStr, StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                isRecognized = true;
                                recipeIngredient.MeasureId = measure.Id;
                                marmitonRecipeIng = marmitonRecipeIng.Replace(measureStr, string.Empty).Trim();
                                break;
                            }
                        }
                    }

                    if (!isRecognized)
                    {
                        MeasureNotRecognizedPauseCrawl(marmitonRecipeIng, measures);
                    }

                    isRecognized = false;
                    foreach (var ingredient in ingredients)
                    {
                        if (!string.IsNullOrWhiteSpace(ingredient.Name))
                        {
                            if (marmitonRecipeIng.IndexOf(ingredient.Name, StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                isRecognized = true;
                                recipeIngredient.IngredientId = ingredient.Id;
                                break;
                            }
                        }
                    }

                    if (!isRecognized)
                    {
                        IngredientNotRecognizedPauseCrawl(marmitonRecipeIng, ingredients);                    
                    }

                    recipe.Ingredients.Add(recipeIngredient);
                }

                recipes.Add(recipe);
            }

            IsCrawling = false;
            CrawlFinishedAction?.Invoke();

            return recipes;
        }

        public void PauseCrawl()
        {
            if (!IsCrawling || IsPaused)
                throw new InvalidOperationException("Can't pause if Crawler isn't crawling or already paused.");

            _autoEvent.WaitOne();
            IsPaused = true;
        }

        private void MeasureNotRecognizedPauseCrawl(string measureName, IList<Measure> measures)
        {
            if (!IsCrawling || IsPaused)
                throw new InvalidOperationException("Can't pause if Crawler isn't crawling or already paused.");

            IsPaused = true;
            MeasureNotRecognizedAction?.Invoke(measureName, measures);
            _autoEvent.WaitOne(); 
        }

        private void IngredientNotRecognizedPauseCrawl(string ingredientName, IList<Ingredient> ingredients)
        {
            if (!IsCrawling || IsPaused)
                throw new InvalidOperationException("Can't pause if Crawler isn't crawling or already paused.");

            IsPaused = true;
            IngredientNotRecognizedAction?.Invoke(ingredientName, ingredients);
            _autoEvent.WaitOne();
        }

        public void ResumeCrawl()
        {
            if (!IsCrawling || !IsPaused)
                throw new InvalidOperationException("Can't resume if Crawler isn't crawling or not paused.");

            IsPaused = false;
            _autoEvent.Set();          
        } 
    }
}
