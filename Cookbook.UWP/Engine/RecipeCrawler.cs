using Cookbook.UWP.RecipeServiceReference;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cookbook.UWP.Engine
{
    public class RecipeCrawler
    {
        public static async Task<List<RecipeServiceReference.Recipe>> CrawlRecipes(int recipeNumber)
        {
            var recipes = new List<RecipeServiceReference.Recipe>();
            RecipeServiceReference.Recipe recipe;
            Match match;

            string marmitonUrl = "http://www.marmiton.org/recettes/recette-hasard.aspx";


            HttpClient httpClient = new HttpClient();
            string result;
            string recipeResult;
            string[] recipeResults;

            for (int i = 0; i < recipeNumber; i++)
            {
                // Récupération des liens des recettes incontournables. 
                result = await httpClient.GetStringAsync(marmitonUrl);
                result = result.Replace(Environment.NewLine, string.Empty);

                // Construction de la recette.
                recipe = new RecipeServiceReference.Recipe();

                // Affectation d'un nouvel identifiant.
                recipe.Id = Guid.NewGuid();

                // Affectation du lien source.
                match = Regex.Match(result, "<link rel=\"canonical\" href=\"[^>]*\"/>");
                recipeResult = match.Value;
                recipeResult = recipeResult.Replace("<link rel=\"canonical\" href=\"", string.Empty);
                recipeResult = recipeResult.Replace("\"/>", string.Empty);
                recipe.ExternalUrl = recipeResult.Trim();

                // Récupération du titre.
                match = Regex.Match(result, "<span class=\"fn\">[^<]*</span>");
                recipeResult = match.Value;
                recipeResult = recipeResult.Replace("<span class=\"fn\">", string.Empty);
                recipeResult = recipeResult.Replace("</span>", string.Empty);
                recipe.Name = recipeResult.Trim();

                // Récupération du type de plat - difficulté - coût.
                match = Regex.Match(result, "<div class=\"m_content_recette_breadcrumb\">[^<]*</div>");
                recipeResult = match.Value;
                recipeResult = recipeResult.Replace("<div class=\"m_content_recette_breadcrumb\">", string.Empty);
                recipeResult = recipeResult.Replace("</div>", string.Empty);
                recipeResults = recipeResult.Split('-');

                if (recipeResults.Length >= 1)
                {
                    recipe.RecipeKind = RecognizeRecipeKind(recipeResults[0]);
                }

                if (recipeResults.Length >= 2)
                {
                    recipe.Difficulty = RecognizeDifficulty(recipeResults[1]);
                }

                if (recipeResults.Length >= 3)
                {
                    recipe.Cost = RecognizeCost(recipeResults[2]);
                }

                if (recipeResults.Length >= 4)
                {
                    recipe.Feature = RecognizeFeature(recipeResults[3]);
                }

                // Récupération du temps de préparation.
                match = Regex.Match(result, "<span class=\"preptime\">[^<]*<");
                recipeResult = match.Value;
                recipeResult = recipeResult.Replace("<span class=\"preptime\">", string.Empty);
                recipeResult = recipeResult.Replace("<", string.Empty);
                recipe.PreparationTime = int.Parse(recipeResult.Trim());

                // Récupération du temps de cuisson.
                match = Regex.Match(result, "<span class=\"cooktime\">[^<]*<");
                recipeResult = match.Value;
                recipeResult = recipeResult.Replace("<span class=\"cooktime\">", string.Empty);
                recipeResult = recipeResult.Replace("<", string.Empty);
                recipe.CookingTime = int.Parse(recipeResult.Trim());

                // Récupération des étapes de préparation.
                match = Regex.Match(result, "paration de la recette :</h4>[^@]*<div class=\"m_content_recette_ps\">");
                recipeResult = match.Value;
                recipeResult = recipeResult.Replace("paration de la recette :</h4>                    <br />", string.Empty);
                recipeResult = recipeResult.Replace("<div class=\"m_content_recette_ps\">", string.Empty);
                recipe.Instructions = new List<RecipeInstruction> { new RecipeInstruction { RecipeId = recipe.Id, Instruction = recipeResult.Trim(), Order = 1 } };

                // Récupération des ingrédients.
                match = Regex.Match(result, "<div class=\"m_content_recette_ingredients m_avec_substitution\" data-content=\"switch-conversion\">[^@]*<div class=\"mrtn_format sas_FormatID_38565\">");
                recipeResult = match.Value;
                recipeResult = recipeResult.Replace("<div class=\"m_content_recette_ingredients m_avec_substitution\" data-content=\"switch-conversion\">", string.Empty);
                recipeResult = recipeResult.Replace("<div class=\"mrtn_format sas_FormatID_38565\">", string.Empty);
                match = Regex.Match(RemoveDiacritics(recipeResult.ToLower()), "<span>ingredients \\(pour [^<]*<");
                int numberPers = int.Parse(match.Value.Replace("<span>ingredients \\(pour ", string.Empty).Replace("<", string.Empty)); 

                recipes.Add(recipe);
            }

            return recipes;
        }

        private static string RemoveDiacritics(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            text = text.Normalize(NormalizationForm.FormD);
            var chars = text.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray();
            return new string(chars).Normalize(NormalizationForm.FormC);
        }

        private static RecipeKind RecognizeRecipeKind(string label)
        {
            var recipeKind = RecipeKind.None;

            switch (RemoveDiacritics(label.Trim().ToLower()))
            {
                case "entree":
                    recipeKind = RecipeKind.Starter;
                    break;
                case "plat principal":
                    recipeKind = RecipeKind.MainCourse;
                    break;
                case "dessert":
                    recipeKind = RecipeKind.Dessert;
                    break;
                case "boisson":
                    recipeKind = RecipeKind.Drink;
                    break;
                case "sauce":
                    recipeKind = RecipeKind.Sauce;
                    break;
                case "accompagnement":
                    recipeKind = RecipeKind.SideDish;
                    break;
                case "amuse-gueule":
                    recipeKind = RecipeKind.AmuseGueule;
                    break;
                case "confiserie":
                    recipeKind = RecipeKind.Sweet;
                    break;
                default:
                    break;
            }

            return recipeKind;
        }

        private static Difficulty RecognizeDifficulty(string label)
        {
            var difficulty = Difficulty.None;

            switch (RemoveDiacritics(label.Trim().ToLower()))
            {
                case "tres facile":
                    difficulty = Difficulty.VeryEasy;
                    break;
                case "facile":
                    difficulty = Difficulty.Easy;
                    break;
                case "moyenne":
                case "moyennement difficile":
                    difficulty = Difficulty.Medium;
                    break;
                case "difficile":
                    difficulty = Difficulty.Difficult;
                    break;
                default:
                    break;
            }

            return difficulty;
        }

        private static Cost RecognizeCost(string label)
        {
            var cost = Cost.None;

            switch (RemoveDiacritics(label.Trim().ToLower()))
            {
                case "bon marche":
                    cost = Cost.Cheap;
                    break;
                case "moyen":
                    cost = Cost.Medium;
                    break;
                case "assez cher":
                    cost = Cost.Expensive;
                    break;
                default:
                    break;
            }

            return cost;
        }

        private static Feature RecognizeFeature(string label)
        {
            var feature = Feature.None;

            switch (RemoveDiacritics(label.Trim().ToLower()))
            {
                case "vegetarien":
                    feature = Feature.Vegetarian;
                    break;
                default:
                    break;
            }

            return feature;
        }
    }
}
