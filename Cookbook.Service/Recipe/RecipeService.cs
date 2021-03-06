﻿using Cookbook.BLL.Recipe;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using Tools.Helper.Compress;
using Tools.Service.Http;

namespace Cookbook.Service.Recipe
{
    using Cookbook.Serializer.Recipe.Json;
    using Entity.Recipe;
    using static Cookbook.Entity.Recipe.RecipeEntityDescriptions;

    public static class RecipeService
    {
        private static RecipeBLL _recipeBLL;

        static RecipeService()
        {
            _recipeBLL = new RecipeBLL();
        }

        public static void Process(HttpListenerContext context)
        {
            switch (context.Request.HttpMethod)
            {
                case HttpMethod.Get:
                    Load(context);
                    break;
                case HttpMethod.Post:
                    Add(context);
                    break;
                case HttpMethod.Put:
                    Update(context);
                    break;
                default:
                    context.Response.StatusCode = (int)HttpStatusCode.NotImplemented;
                    break;
            }
        }

        private static void Load(HttpListenerContext context)
        {
            if (context.IsAcceptGZipJson())
            {
                var filter = GetFilter(context);
                var fields = GetFields(context);

                var recipes = _recipeBLL.Load(filter, fields);

                var serializer = new RecipeJsonSerializer();
                serializer.SetFields(fields);

                using (var stream = serializer.Serialize(recipes))
                {
                    using (var gzip = GZipHelper.Compress(stream))
                    {
                        gzip.CopyTo(context.Response.OutputStream);
                    }
                }

                context.Response.StatusCode = (int)HttpStatusCode.OK;
            }
            else
            {
                // TODO : Indiquer pourquoi.
                context.Response.StatusCode = (int)HttpStatusCode.NotAcceptable;
            }
        }

        private static RecipeFilter GetFilter(HttpListenerContext context)
        {
            var filter = new RecipeFilter();

            foreach (var key in context.Request.QueryString.AllKeys)
            {
                switch (key.ToLower())
                {
                    case "id":
                        foreach (var id in context.Request.QueryString[key].Split(','))
                            filter.IdsToLoad.Add(new Guid(id));
                        break;
                    case "name":
                        filter.Name = context.Request.QueryString[key].ToLower();
                        break;
                    case "preparationtime":
                        filter.PreparationTime = int.Parse(context.Request.QueryString[key], CultureInfo.InvariantCulture);
                        break;
                    case "cookingtime":
                        filter.CookingTime = int.Parse(context.Request.QueryString[key], CultureInfo.InvariantCulture);
                        break;
                    case "costid":
                        foreach (var id in context.Request.QueryString[key].Split(','))
                            filter.CostIds.Add(new Guid(id));
                        break;
                    case "recipekindid":
                        foreach (var id in context.Request.QueryString[key].Split(','))
                            filter.RecipeKindIds.Add(new Guid(id));
                        break;
                    case "featureid":
                        foreach (var id in context.Request.QueryString[key].Split(','))
                            filter.FeatureIds.Add(new Guid(id));
                        break;
                    case "difficultyid":
                        foreach (var id in context.Request.QueryString[key].Split(','))
                            filter.DifficultyIds.Add(new Guid(id));
                        break;
                    case "seasonid":
                        foreach (var id in context.Request.QueryString[key].Split(','))
                            filter.SeasonIds.Add(new Guid(id));
                        break;
                    default:
                        break;
                }
            }

            return filter;
        }

        private static List<string> GetFields(HttpListenerContext context)
        {
            var fields = new List<string>();

            foreach (var key in context.Request.QueryString.AllKeys)
            {
                switch (key.ToLower())
                {
                    case "field":
                        foreach (var field in context.Request.QueryString[key].Split(','))
                            fields.Add(field.ToLower());
                        break;
                    default:
                        break;
                }
            }

            if (fields.Count > 0)
                return fields;
            else
                return new List<string>(RecipeEntityDescription.AllLower);
        }

        private static void Add(HttpListenerContext context)
        {
            if (context.IsContentGZipJson()
                && context.Request.ContentLength64 > 0)
            {
                using (var gzip = GZipHelper.Decompress(context.Request.InputStream))
                {
                    var recipes = new RecipeJsonSerializer().Deserialize(gzip);

                    if (recipes.Count > 0)
                    {
                        _recipeBLL.Add(recipes);
                    }
                }

                context.Response.StatusCode = (int)HttpStatusCode.OK;
            }
            else
            {
                // TODO : Indiquer pourquoi.
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
        }

        private static void Update(HttpListenerContext context)
        {
            if (context.IsContentGZipJson()
                && context.Request.ContentLength64 > 0)
            {
                using (var gzip = GZipHelper.Decompress(context.Request.InputStream))
                {
                    var recipes = new RecipeJsonSerializer().Deserialize(gzip);

                    if (recipes.Count > 0)
                    {
                        _recipeBLL.Update(recipes);
                    }
                }

                context.Response.StatusCode = (int)HttpStatusCode.OK;
            }
            else
            {
                // TODO : Indiquer pourquoi.
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
        }
    }
}
