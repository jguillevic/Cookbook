using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Tools.Serializer.Json;
using static Cookbook.Entity.Recipe.RecipeEntityDescriptions;

namespace Cookbook.Serializer.Recipe.Json
{
    public class RecipeJsonSerializer : IJsonSerializer<List<Entity.Recipe.Recipe>>
    {
        private List<string> _fields;

        public bool UsePropDescrShortName { get; set; }
        public bool IsIndent { get; set; }

        public RecipeJsonSerializer()
        {
            UsePropDescrShortName = true;
            IsIndent = false;
            _fields = new List<string>(RecipeEntityDescription.AllLower);
        }

        public Stream Serialize(List<Entity.Recipe.Recipe> recipes)
        {
            var stream = new MemoryStream();

            using (var writer = new StreamWriter(stream, Encoding.UTF8, 512, true))
            {
                using (var jsonWriter = new JsonTextWriter(writer))
                {
                    jsonWriter.Formatting = IsIndent ? Formatting.Indented : Formatting.None;
                    jsonWriter.CloseOutput = false;

                    Serialize(recipes, jsonWriter);
                }
            }

            stream.Position = 0;

            return stream;
        }

        public void Serialize(List<Entity.Recipe.Recipe> recipes, JsonWriter jsonWriter)
        {
            jsonWriter.WriteStartArray();

            for (int i = 0; i < recipes.Count; i++)
            {
                jsonWriter.WriteStartObject();

                WriteId(recipes[i], jsonWriter);
                WriteName(recipes[i], jsonWriter);
                WriteDescription(recipes[i], jsonWriter);
                WriteInstructions(recipes[i], jsonWriter);
                WritePreparationTime(recipes[i], jsonWriter);
                WriteCookingTime(recipes[i], jsonWriter);
                WriteSeasonIds(recipes[i], jsonWriter);
                WriteCostId(recipes[i], jsonWriter);
                WriteDifficultyId(recipes[i], jsonWriter);
                WriteRecipeKindId(recipes[i], jsonWriter);
                WriteFeatureIds(recipes[i], jsonWriter);
                WriteIngredients(recipes[i], jsonWriter);
                WriteExternalUrl(recipes[i], jsonWriter);
                WriteUserId(recipes[i], jsonWriter);
                WriteImageUrl(recipes[i], jsonWriter);

                jsonWriter.WriteEndObject();
            }

            jsonWriter.WriteEndArray();
        }

        private void WriteId(Entity.Recipe.Recipe recipe, JsonWriter jsonWriter)
        {
            if (_fields.Contains(RecipeEntityDescription.Id.ToLower()))
            {
                jsonWriter.WritePropertyName(RecipeSerializerDescription.Id.GetName(UsePropDescrShortName));
                jsonWriter.WriteValue(recipe.Id.ToString("N"));
            }
        }

        private void WriteName(Entity.Recipe.Recipe recipe, JsonWriter jsonWriter)
        {
            if (_fields.Contains(RecipeEntityDescription.Name.ToLower()))
            {
                jsonWriter.WritePropertyName(RecipeSerializerDescription.Name.GetName(UsePropDescrShortName));
                jsonWriter.WriteValue(recipe.Name);
            }
        }

        private void WriteDescription(Entity.Recipe.Recipe recipe, JsonWriter jsonWriter)
        {
            if (_fields.Contains(RecipeEntityDescription.Description.ToLower()))
            {
                jsonWriter.WritePropertyName(RecipeSerializerDescription.Description.GetName(UsePropDescrShortName));
                jsonWriter.WriteValue(recipe.Description);
            }
        }

        private void WriteInstructions(Entity.Recipe.Recipe recipe, JsonWriter jsonWriter)
        {
            if (_fields.Contains(RecipeEntityDescription.Instructions.ToLower()))
            {
                jsonWriter.WritePropertyName(RecipeSerializerDescription.Instructions.GetName(UsePropDescrShortName));
                new RecipeInstructionJsonSerializer().Serialize(recipe.Instructions, jsonWriter);
            }
        }

        private void WritePreparationTime(Entity.Recipe.Recipe recipe, JsonWriter jsonWriter)
        {
            if (_fields.Contains(RecipeEntityDescription.PreparationTime.ToLower()))
            {
                jsonWriter.WritePropertyName(RecipeSerializerDescription.PreparationTime.GetName(UsePropDescrShortName));
                jsonWriter.WriteValue(recipe.PreparationTime);
            }
        }

        private void WriteCookingTime(Entity.Recipe.Recipe recipe, JsonWriter jsonWriter)
        {
            if (_fields.Contains(RecipeEntityDescription.CookingTime.ToLower()))
            {
                jsonWriter.WritePropertyName(RecipeSerializerDescription.CookingTime.GetName(UsePropDescrShortName));
                jsonWriter.WriteValue(recipe.CookingTime);
            }
        }

        private void WriteSeasonIds(Entity.Recipe.Recipe recipe, JsonWriter jsonWriter)
        {
            if (_fields.Contains(RecipeEntityDescription.SeasonIds.ToLower()))
            {
                jsonWriter.WritePropertyName(RecipeSerializerDescription.SeasonIds.GetName(UsePropDescrShortName));

                jsonWriter.WriteStartArray();

                for (int j = 0; j < recipe.SeasonIds.Count; j++)
                    jsonWriter.WriteValue(recipe.SeasonIds[j]);

                jsonWriter.WriteEndArray();
            }
        }

        private void WriteCostId(Entity.Recipe.Recipe recipe, JsonWriter jsonWriter)
        {
            if (_fields.Contains(RecipeEntityDescription.CostId.ToLower()))
            {
                jsonWriter.WritePropertyName(RecipeSerializerDescription.CostId.GetName(UsePropDescrShortName));
                jsonWriter.WriteValue(recipe.CostId);
            }
        }

        private void WriteDifficultyId(Entity.Recipe.Recipe recipe, JsonWriter jsonWriter)
        {
            if (_fields.Contains(RecipeEntityDescription.DifficultyId.ToLower()))
            {
                jsonWriter.WritePropertyName(RecipeSerializerDescription.DifficultyId.GetName(UsePropDescrShortName));
                jsonWriter.WriteValue(recipe.DifficultyId);
            }
        }

        private void WriteRecipeKindId(Entity.Recipe.Recipe recipe, JsonWriter jsonWriter)
        {
            if (_fields.Contains(RecipeEntityDescription.RecipeKindId.ToLower()))
            {
                jsonWriter.WritePropertyName(RecipeSerializerDescription.RecipeKindId.GetName(UsePropDescrShortName));
                jsonWriter.WriteValue(recipe.RecipeKindId);
            }
        }

        private void WriteFeatureIds(Entity.Recipe.Recipe recipe, JsonWriter jsonWriter)
        {
            if (_fields.Contains(RecipeEntityDescription.FeatureIds.ToLower()))
            {
                jsonWriter.WritePropertyName(RecipeSerializerDescription.FeatureIds.GetName(UsePropDescrShortName));

                jsonWriter.WriteStartArray();

                for (int j = 0; j < recipe.FeatureIds.Count; j++)
                    jsonWriter.WriteValue(recipe.FeatureIds[j]);

                jsonWriter.WriteEndArray();
            }
        }

        private void WriteIngredients(Entity.Recipe.Recipe recipe, JsonWriter jsonWriter)
        {
            if (_fields.Contains(RecipeEntityDescription.Ingredients.ToLower()))
            {
                jsonWriter.WritePropertyName(RecipeSerializerDescription.Ingredients.GetName(UsePropDescrShortName));
                new RecipeIngredientJsonSerializer().Serialize(recipe.Ingredients, jsonWriter);
            }
        }

        private void WriteExternalUrl(Entity.Recipe.Recipe recipe, JsonWriter jsonWriter)
        {
            if (_fields.Contains(RecipeEntityDescription.ExternalUrl.ToLower()))
            {
                jsonWriter.WritePropertyName(RecipeSerializerDescription.ExternalUrl.GetName(UsePropDescrShortName));
                jsonWriter.WriteValue(recipe.ExternalUrl);
            }
        }

        private void WriteUserId(Entity.Recipe.Recipe recipe, JsonWriter jsonWriter)
        {
            if (_fields.Contains(RecipeEntityDescription.UserId.ToLower()))
            {
                jsonWriter.WritePropertyName(RecipeSerializerDescription.UserId.GetName(UsePropDescrShortName));

                string value = null;
                if (recipe.UserId.HasValue)
                    value = recipe.UserId.Value.ToString("N");
                jsonWriter.WriteValue(value);
            }
        }

        private void WriteImageUrl(Entity.Recipe.Recipe recipe, JsonWriter jsonWriter)
        {
            if (_fields.Contains(RecipeEntityDescription.ImageUrl.ToLower()))
            {
                jsonWriter.WritePropertyName(RecipeSerializerDescription.ImageUrl.GetName(UsePropDescrShortName));
                jsonWriter.WriteValue(recipe.ImageUrl);
            }
        }

        public List<Entity.Recipe.Recipe> Deserialize(Stream stream)
        {
            using (var reader = new StreamReader(stream, Encoding.UTF8, false, 512, true))
            {
                using (var jsonReader = new JsonTextReader(reader))
                {
                    jsonReader.CloseInput = false;

                    return Deserialize(jsonReader);
                }
            }
        }

        public List<Entity.Recipe.Recipe> Deserialize(JsonReader jsonReader)
        {
            var recipes = new List<Entity.Recipe.Recipe>();
            Entity.Recipe.Recipe recipe = null;

            while (jsonReader.Read() && jsonReader.TokenType != JsonToken.EndArray)
            {
                if (jsonReader.TokenType == JsonToken.StartObject)
                    recipe = new Entity.Recipe.Recipe();

                if (jsonReader.TokenType == JsonToken.PropertyName)
                {
                    bool isAlreadyRead = false;
                    isAlreadyRead = ReadId(jsonReader, recipe, isAlreadyRead);
                    isAlreadyRead = ReadName(jsonReader, recipe, isAlreadyRead);
                    isAlreadyRead = ReadDescription(jsonReader, recipe, isAlreadyRead);
                    isAlreadyRead = ReadInstructions(jsonReader, recipe, isAlreadyRead);
                    isAlreadyRead = ReadPreparationTime(jsonReader, recipe, isAlreadyRead);
                    isAlreadyRead = ReadCookingTime(jsonReader, recipe, isAlreadyRead);
                    isAlreadyRead = ReadSeasonIds(jsonReader, recipe, isAlreadyRead);
                    isAlreadyRead = ReadCostId(jsonReader, recipe, isAlreadyRead);
                    isAlreadyRead = ReadDifficultyId(jsonReader, recipe, isAlreadyRead);
                    isAlreadyRead = ReadRecipeKindId(jsonReader, recipe, isAlreadyRead);
                    isAlreadyRead = ReadFeatureIds(jsonReader, recipe, isAlreadyRead);
                    isAlreadyRead = ReadIngredients(jsonReader, recipe, isAlreadyRead);
                    isAlreadyRead = ReadExternalUrl(jsonReader, recipe, isAlreadyRead);
                    isAlreadyRead = ReadUserId(jsonReader, recipe, isAlreadyRead);
                    isAlreadyRead = ReadImageUrl(jsonReader, recipe, isAlreadyRead);
                }

                if (jsonReader.TokenType == JsonToken.EndObject)
                    recipes.Add(recipe);
            }

            return recipes;
        }

        private bool ReadId(JsonReader jsonReader, Entity.Recipe.Recipe recipe, bool isAlreadyRead)
        {
            if (!isAlreadyRead)
            {
                if (RecipeSerializerDescription.Id.GetName(UsePropDescrShortName) == jsonReader.Value.ToString())
                {
                    jsonReader.Read();
                    recipe.Id = new Guid(jsonReader.Value.ToString());

                    return true;
                }
            }

            return isAlreadyRead;
        }

        private bool ReadName(JsonReader jsonReader, Entity.Recipe.Recipe recipe, bool isAlreadyRead)
        {
            if (!isAlreadyRead)
            {
                if (RecipeSerializerDescription.Name.GetName(UsePropDescrShortName) == jsonReader.Value.ToString())
                {
                    jsonReader.Read();
                    recipe.Name = (string)jsonReader.Value;

                    return true;
                }
            }

            return isAlreadyRead;
        }

        private bool ReadDescription(JsonReader jsonReader, Entity.Recipe.Recipe recipe, bool isAlreadyRead)
        {
            if (!isAlreadyRead)
            {
                if (RecipeSerializerDescription.Description.GetName(UsePropDescrShortName) == jsonReader.Value.ToString())
                {
                    jsonReader.Read();
                    recipe.Description = (string)jsonReader.Value;

                    return true;
                }
            }

            return isAlreadyRead;
        }

        private bool ReadInstructions(JsonReader jsonReader, Entity.Recipe.Recipe recipe, bool isAlreadyRead)
        {
            if (!isAlreadyRead)
            {
                if (RecipeSerializerDescription.Instructions.GetName(UsePropDescrShortName) == jsonReader.Value.ToString())
                {
                    recipe.Instructions.AddRange(new RecipeInstructionJsonSerializer().Deserialize(jsonReader));

                    return true;
                }
            }

            return isAlreadyRead;
        }

        private bool ReadPreparationTime(JsonReader jsonReader, Entity.Recipe.Recipe recipe, bool isAlreadyRead)
        {
            if (!isAlreadyRead)
            {
                if (RecipeSerializerDescription.PreparationTime.GetName(UsePropDescrShortName) == jsonReader.Value.ToString())
                {
                    jsonReader.Read();
                    recipe.PreparationTime = Convert.ToInt32(jsonReader.Value, CultureInfo.InvariantCulture);

                    return true;
                }
            }

            return isAlreadyRead;
        }

        private bool ReadCookingTime(JsonReader jsonReader, Entity.Recipe.Recipe recipe, bool isAlreadyRead)
        {
            if (!isAlreadyRead)
            {
                if (RecipeSerializerDescription.CookingTime.GetName(UsePropDescrShortName) == jsonReader.Value.ToString())
                {
                    jsonReader.Read();
                    recipe.CookingTime = Convert.ToInt32(jsonReader.Value, CultureInfo.InvariantCulture);

                    return true;
                }
            }

            return isAlreadyRead;
        }

        private bool ReadSeasonIds(JsonReader jsonReader, Entity.Recipe.Recipe recipe, bool isAlreadyRead)
        {
            if (!isAlreadyRead)
            {
                if (RecipeSerializerDescription.SeasonIds.GetName(UsePropDescrShortName) == jsonReader.Value.ToString())
                {
                    while (jsonReader.TokenType != JsonToken.EndArray)
                    {
                        jsonReader.Read();

                        if (jsonReader.TokenType == JsonToken.PropertyName)
                            recipe.SeasonIds.Add(new Guid(jsonReader.Value.ToString()));
                    }

                    return true;
                }
            }

            return isAlreadyRead;
        }

        private bool ReadCostId(JsonReader jsonReader, Entity.Recipe.Recipe recipe, bool isAlreadyRead)
        {
            if (!isAlreadyRead)
            {
                if (RecipeSerializerDescription.CostId.GetName(UsePropDescrShortName) == jsonReader.Value.ToString())
                {
                    jsonReader.Read();
                    recipe.CostId = new Guid(jsonReader.Value.ToString());

                    return true;
                }
            }

            return isAlreadyRead;
        }

        private bool ReadDifficultyId(JsonReader jsonReader, Entity.Recipe.Recipe recipe, bool isAlreadyRead)
        {
            if (!isAlreadyRead)
            {
                if (RecipeSerializerDescription.DifficultyId.GetName(UsePropDescrShortName) == jsonReader.Value.ToString())
                {
                    jsonReader.Read();
                    recipe.DifficultyId = new Guid(jsonReader.Value.ToString());

                    return true;
                }
            }

            return isAlreadyRead;
        }

        private bool ReadRecipeKindId(JsonReader jsonReader, Entity.Recipe.Recipe recipe, bool isAlreadyRead)
        {
            if (!isAlreadyRead)
            {
                if (RecipeSerializerDescription.RecipeKindId.GetName(UsePropDescrShortName) == jsonReader.Value.ToString())
                {
                    jsonReader.Read();
                    recipe.RecipeKindId = new Guid(jsonReader.Value.ToString());

                    return true;
                }
            }

            return isAlreadyRead;
        }

        private bool ReadFeatureIds(JsonReader jsonReader, Entity.Recipe.Recipe recipe, bool isAlreadyRead)
        {
            if (!isAlreadyRead)
            {
                if (RecipeSerializerDescription.FeatureIds.GetName(UsePropDescrShortName) == jsonReader.Value.ToString())
                {
                    while (jsonReader.TokenType != JsonToken.EndArray)
                    {
                        jsonReader.Read();

                        if (jsonReader.TokenType == JsonToken.PropertyName)
                            recipe.SeasonIds.Add(new Guid(jsonReader.Value.ToString()));
                    }

                    return true;
                }
            }

            return isAlreadyRead;
        }

        private bool ReadIngredients(JsonReader jsonReader, Entity.Recipe.Recipe recipe, bool isAlreadyRead)
        {
            if (!isAlreadyRead)
            {
                if (RecipeSerializerDescription.Ingredients.GetName(UsePropDescrShortName) == jsonReader.Value.ToString())
                {
                    recipe.Ingredients.AddRange(new RecipeIngredientJsonSerializer().Deserialize(jsonReader));

                    return true;
                }
            }

            return isAlreadyRead;
        }

        private bool ReadExternalUrl(JsonReader jsonReader, Entity.Recipe.Recipe recipe, bool isAlreadyRead)
        {
            if (!isAlreadyRead)
            {
                if (RecipeSerializerDescription.ExternalUrl.GetName(UsePropDescrShortName) == jsonReader.Value.ToString())
                {
                    jsonReader.Read();
                    recipe.ExternalUrl = (string)jsonReader.Value;

                    return true;
                }
            }

            return isAlreadyRead;
        }

        private bool ReadUserId(JsonReader jsonReader, Entity.Recipe.Recipe recipe, bool isAlreadyRead)
        {
            if (!isAlreadyRead)
            {
                if (RecipeSerializerDescription.UserId.GetName(UsePropDescrShortName) == jsonReader.Value.ToString())
                {
                    jsonReader.Read();
                    if (jsonReader.Value != null)
                        recipe.UserId = new Guid(jsonReader.Value.ToString());

                    return true;
                }
            }

            return isAlreadyRead;
        }

        private bool ReadImageUrl(JsonReader jsonReader, Entity.Recipe.Recipe recipe, bool isAlreadyRead)
        {
            if (!isAlreadyRead)
            {
                if (RecipeSerializerDescription.ImageUrl.GetName(UsePropDescrShortName) == jsonReader.Value.ToString())
                {
                    jsonReader.Read();
                    recipe.ImageUrl = (string)jsonReader.Value;

                    return true;
                }
            }

            return isAlreadyRead;
        }

        public void SetFields(List<string> fields)
        {
            _fields.Clear();
            _fields.AddRange(fields);
        }
    }
}
