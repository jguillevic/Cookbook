using Cookbook.Entity.Recipe;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tools.Serializer.Json;
using static Cookbook.Entity.Recipe.RecipeEntityDescriptions;

namespace Cookbook.Serializer.Recipe.Json
{
    public class RecipeIngredientJsonSerializer : IJsonSerializer<List<RecipeIngredient>>
    {
        private List<string> _fields;

        public bool UsePropDescrShortName { get; set; }
        public bool IsIndent { get; set; }

        public RecipeIngredientJsonSerializer()
        {
            UsePropDescrShortName = true;
            IsIndent = false;
            _fields = new List<string>(RecipeIngredientEntityDescription.AllLower);
        }

        public Stream Serialize(List<RecipeIngredient> ingredients)
        {
            var stream = new MemoryStream();

            using (var writer = new StreamWriter(stream, Encoding.UTF8, 512, true))
            {
                using (var jsonWriter = new JsonTextWriter(writer))
                {
                    jsonWriter.Formatting = IsIndent ? Formatting.Indented : Formatting.None;
                    jsonWriter.CloseOutput = false;

                    Serialize(ingredients, jsonWriter);
                }
            }

            stream.Position = 0;

            return stream;
        }

        public void Serialize(List<RecipeIngredient> ingredients, JsonWriter jsonWriter)
        {
            jsonWriter.WriteStartArray();

            for (int i = 0; i < ingredients.Count; i++)
            {
                jsonWriter.WriteStartObject();

                WriteRecipeId(ingredients[i], jsonWriter);
                WriteIngredientId(ingredients[i], jsonWriter);
                WriteMeasureId(ingredients[i], jsonWriter);
                WriteAmount(ingredients[i], jsonWriter);
                WriteOrder(ingredients[i], jsonWriter);

                jsonWriter.WriteEndObject();
            }

            jsonWriter.WriteEndArray();
        }

        private void WriteRecipeId(RecipeIngredient ingredient, JsonWriter jsonWriter)
        {
            if (_fields.Contains(RecipeIngredientEntityDescription.RecipeId.ToLower()))
            {
                jsonWriter.WritePropertyName(RecipeIngredientSerializerDescription.RecipeId.GetName(UsePropDescrShortName));
                jsonWriter.WriteValue(ingredient.RecipeId.ToString("N"));
            }
        }

        private void WriteIngredientId(RecipeIngredient ingredient, JsonWriter jsonWriter)
        {
            if (_fields.Contains(RecipeIngredientEntityDescription.IngredientId.ToLower()))
            {
                jsonWriter.WritePropertyName(RecipeIngredientSerializerDescription.IngredientId.GetName(UsePropDescrShortName));
                jsonWriter.WriteValue(ingredient.IngredientId.ToString("N"));
            }
        }

        private void WriteMeasureId(RecipeIngredient ingredient, JsonWriter jsonWriter)
        {
            if (_fields.Contains(RecipeIngredientEntityDescription.MeasureId.ToLower()))
            {
                jsonWriter.WritePropertyName(RecipeIngredientSerializerDescription.MeasureId.GetName(UsePropDescrShortName));
                jsonWriter.WriteValue(ingredient.MeasureId.ToString("N"));
            }
        }

        private void WriteAmount(RecipeIngredient ingredient, JsonWriter jsonWriter)
        {
            if (_fields.Contains(RecipeIngredientEntityDescription.Amount.ToLower()))
            {
                jsonWriter.WritePropertyName(RecipeIngredientSerializerDescription.Amount.GetName(UsePropDescrShortName));
                jsonWriter.WriteValue(ingredient.Amount);
            }
        }

        private void WriteOrder(RecipeIngredient ingredient, JsonWriter jsonWriter)
        {
            if (_fields.Contains(RecipeIngredientEntityDescription.Order.ToLower()))
            {
                jsonWriter.WritePropertyName(RecipeIngredientSerializerDescription.Order.GetName(UsePropDescrShortName));
                jsonWriter.WriteValue(ingredient.Order);
            }
        }

        public List<RecipeIngredient> Deserialize(Stream stream)
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

        public List<RecipeIngredient> Deserialize(JsonReader jsonReader)
        {
            var ingredients = new List<RecipeIngredient>();
            RecipeIngredient ingredient = null;

            while (jsonReader.Read())
            {
                if (jsonReader.TokenType == JsonToken.StartObject)
                    ingredient = new RecipeIngredient();

                if (jsonReader.TokenType == JsonToken.PropertyName)
                {
                    bool isAlreadyRead = false;
                    isAlreadyRead = ReadRecipeId(jsonReader, ingredient, isAlreadyRead);
                    isAlreadyRead = ReadIngredientId(jsonReader, ingredient, isAlreadyRead);
                    isAlreadyRead = ReadMeasureId(jsonReader, ingredient, isAlreadyRead);
                    isAlreadyRead = ReadAmount(jsonReader, ingredient, isAlreadyRead);
                    isAlreadyRead = ReadOrder(jsonReader, ingredient, isAlreadyRead);
                }

                if (jsonReader.TokenType == JsonToken.EndObject)
                    ingredients.Add(ingredient);
            }

            return ingredients;
        }

        private bool ReadRecipeId(JsonReader jsonReader, RecipeIngredient ingredient, bool isAlreadyRead)
        {
            if (!isAlreadyRead)
            {
                if (RecipeIngredientSerializerDescription.RecipeId.GetName(UsePropDescrShortName) == jsonReader.Value.ToString())
                {
                    jsonReader.Read();
                    ingredient.RecipeId = new Guid(jsonReader.Value.ToString());

                    return true;
                }
            }

            return isAlreadyRead;
        }

        private bool ReadIngredientId(JsonReader jsonReader, RecipeIngredient ingredient, bool isAlreadyRead)
        {
            if (!isAlreadyRead)
            {
                if (RecipeIngredientSerializerDescription.IngredientId.GetName(UsePropDescrShortName) == jsonReader.Value.ToString())
                {
                    jsonReader.Read();
                    ingredient.IngredientId = new Guid(jsonReader.Value.ToString());

                    return true;
                }
            }

            return isAlreadyRead;
        }

        private bool ReadMeasureId(JsonReader jsonReader, RecipeIngredient ingredient, bool isAlreadyRead)
        {
            if (!isAlreadyRead)
            {
                if (RecipeIngredientSerializerDescription.MeasureId.GetName(UsePropDescrShortName) == jsonReader.Value.ToString())
                {
                    jsonReader.Read();
                    ingredient.MeasureId = new Guid(jsonReader.Value.ToString());

                    return true;
                }
            }

            return isAlreadyRead;
        }

        private bool ReadAmount(JsonReader jsonReader, RecipeIngredient ingredient, bool isAlreadyRead)
        {
            if (!isAlreadyRead)
            {
                if (RecipeIngredientSerializerDescription.Amount.GetName(UsePropDescrShortName) == jsonReader.Value.ToString())
                {
                    jsonReader.Read();
                    ingredient.Amount = int.Parse(jsonReader.Value.ToString());

                    return true;
                }
            }

            return isAlreadyRead;
        }

        private bool ReadOrder(JsonReader jsonReader, RecipeIngredient ingredient, bool isAlreadyRead)
        {
            if (!isAlreadyRead)
            {
                if (RecipeIngredientSerializerDescription.Order.GetName(UsePropDescrShortName) == jsonReader.Value.ToString())
                {
                    jsonReader.Read();
                    ingredient.Order = int.Parse(jsonReader.Value.ToString());

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
