using Cookbook.Service.Configuration;
using Cookbook.Service.Recipe;
using System;
using System.Collections.Generic;
using System.Net;
using Tools.Service.Http;

namespace Cookbook.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            var actions = new Dictionary<string, Action<HttpListenerContext>>();

            actions.Add(ServiceConfiguration.Instance.RecipeRoute, RecipeService.Process);
            actions.Add(ServiceConfiguration.Instance.CostRoute, CostService.Process);
            actions.Add(ServiceConfiguration.Instance.DifficultyRoute, DifficultyService.Process);
            actions.Add(ServiceConfiguration.Instance.RecipeKindRoute, RecipeKindService.Process);
            actions.Add(ServiceConfiguration.Instance.SeasonRoute, SeasonService.Process);
            actions.Add(ServiceConfiguration.Instance.FeatureRoute, FeatureService.Process);
            actions.Add(ServiceConfiguration.Instance.IngredientRoute, IngredientService.Process);
            actions.Add(ServiceConfiguration.Instance.MeasureRoute, MeasureService.Process);

            var server = new HttpServer(ServiceConfiguration.Instance.BaseUrl, actions);
            server.Start();
        }
    }
}
