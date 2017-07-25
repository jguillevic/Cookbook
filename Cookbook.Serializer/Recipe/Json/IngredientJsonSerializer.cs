using Cookbook.Entity.Recipe;
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
    public class IngredientJsonSerializer : IJsonSerializer<List<Ingredient>>
    {
        private List<string> _fields;

        public bool UsePropDescrShortName { get; set; }
        public bool IsIndent { get; set; }

        public IngredientJsonSerializer()
        {
            UsePropDescrShortName = true;
            IsIndent = false;
            _fields = new List<string>(IngredientEntityDescription.AllLower);
        }

        public Stream Serialize(List<Ingredient> ingredients)
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

        public void Serialize(List<Ingredient> ingredients, JsonWriter jsonWriter)
        {
            jsonWriter.WriteStartArray();

            for (int i = 0; i < ingredients.Count; i++)
            {
                jsonWriter.WriteStartObject();

                WriteId(ingredients[i], jsonWriter);
                WriteName(ingredients[i], jsonWriter);
                WriteCode(ingredients[i], jsonWriter);
                WriteIngredientKindId(ingredients[i], jsonWriter);
                WriteCalories(ingredients[i], jsonWriter);
                WriteProtein(ingredients[i], jsonWriter);
                WriteCarbohydrate(ingredients[i], jsonWriter);
                WriteLipid(ingredients[i], jsonWriter);
                WriteWater(ingredients[i], jsonWriter);
                WriteFiber(ingredients[i], jsonWriter);

                jsonWriter.WriteEndObject();
            }

            jsonWriter.WriteEndArray();
        }

        private void WriteId(Ingredient ingredient, JsonWriter jsonWriter)
        {
            if (_fields.Contains(IngredientEntityDescription.Id.ToLower()))
            {
                jsonWriter.WritePropertyName(IngredientSerializerDescription.Id.GetName(UsePropDescrShortName));
                jsonWriter.WriteValue(ingredient.Id.ToString("N"));
            }
        }

        private void WriteName(Ingredient ingredient, JsonWriter jsonWriter)
        {
            if (_fields.Contains(IngredientEntityDescription.Name.ToLower()))
            {
                jsonWriter.WritePropertyName(IngredientSerializerDescription.Name.GetName(UsePropDescrShortName));
                jsonWriter.WriteValue(ingredient.Name);
            }
        }

        private void WriteCode(Ingredient ingredient, JsonWriter jsonWriter)
        {
            if (_fields.Contains(IngredientEntityDescription.Code.ToLower()))
            {
                jsonWriter.WritePropertyName(IngredientSerializerDescription.Code.GetName(UsePropDescrShortName));
                jsonWriter.WriteValue(ingredient.Code);
            }
        }

        private void WriteIngredientKindId(Ingredient ingredient, JsonWriter jsonWriter)
        {
            if (_fields.Contains(IngredientEntityDescription.IngredientKindId.ToLower()))
            {
                jsonWriter.WritePropertyName(IngredientSerializerDescription.IngredientKindId.GetName(UsePropDescrShortName));
                jsonWriter.WriteValue(ingredient.IngredientKindId.ToString("N"));
            }
        }

        private void WriteCalories(Ingredient ingredient, JsonWriter jsonWriter)
        {
            if (_fields.Contains(IngredientEntityDescription.Calories.ToLower()))
            {
                jsonWriter.WritePropertyName(IngredientSerializerDescription.Calories.GetName(UsePropDescrShortName));
                jsonWriter.WriteValue(ingredient.Calories);
            }
        }

        private void WriteProtein(Ingredient ingredient, JsonWriter jsonWriter)
        {
            if (_fields.Contains(IngredientEntityDescription.Protein.ToLower()))
            {
                jsonWriter.WritePropertyName(IngredientSerializerDescription.Protein.GetName(UsePropDescrShortName));
                jsonWriter.WriteValue(ingredient.Protein);
            }
        }

        private void WriteCarbohydrate(Ingredient ingredient, JsonWriter jsonWriter)
        {
            if (_fields.Contains(IngredientEntityDescription.Carbohydrate.ToLower()))
            {
                jsonWriter.WritePropertyName(IngredientSerializerDescription.Carbohydrate.GetName(UsePropDescrShortName));
                jsonWriter.WriteValue(ingredient.Carbohydrate);
            }
        }

        private void WriteLipid(Ingredient ingredient, JsonWriter jsonWriter)
        {
            if (_fields.Contains(IngredientEntityDescription.Lipid.ToLower()))
            {
                jsonWriter.WritePropertyName(IngredientSerializerDescription.Lipid.GetName(UsePropDescrShortName));
                jsonWriter.WriteValue(ingredient.Lipid);
            }
        }

        private void WriteWater(Ingredient ingredient, JsonWriter jsonWriter)
        {
            if (_fields.Contains(IngredientEntityDescription.Water.ToLower()))
            {
                jsonWriter.WritePropertyName(IngredientSerializerDescription.Water.GetName(UsePropDescrShortName));
                jsonWriter.WriteValue(ingredient.Water);
            }
        }

        private void WriteFiber(Ingredient ingredient, JsonWriter jsonWriter)
        {
            if (_fields.Contains(IngredientEntityDescription.Fiber.ToLower()))
            {
                jsonWriter.WritePropertyName(IngredientSerializerDescription.Fiber.GetName(UsePropDescrShortName));
                jsonWriter.WriteValue(ingredient.Fiber);
            }
        }

        public List<Ingredient> Deserialize(Stream stream)
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

        public List<Ingredient> Deserialize(JsonReader jsonReader)
        {
            var ingredients = new List<Ingredient>();
            Ingredient ingredient = null;

            while (jsonReader.Read() && jsonReader.TokenType != JsonToken.EndArray)
            {
                if (jsonReader.TokenType == JsonToken.StartObject)
                    ingredient = new Ingredient();

                if (jsonReader.TokenType == JsonToken.PropertyName)
                {
                    bool isAlreadyRead = false;
                    isAlreadyRead = ReadId(jsonReader, ingredient, isAlreadyRead);
                    isAlreadyRead = ReadName(jsonReader, ingredient, isAlreadyRead);
                    isAlreadyRead = ReadCode(jsonReader, ingredient, isAlreadyRead);
                    isAlreadyRead = ReadIngredientKindId(jsonReader, ingredient, isAlreadyRead);
                    isAlreadyRead = ReadCalories(jsonReader, ingredient, isAlreadyRead);
                    isAlreadyRead = ReadProtein(jsonReader, ingredient, isAlreadyRead);
                    isAlreadyRead = ReadCarbohydrate(jsonReader, ingredient, isAlreadyRead);
                    isAlreadyRead = ReadLipid(jsonReader, ingredient, isAlreadyRead);
                    isAlreadyRead = ReadWater(jsonReader, ingredient, isAlreadyRead);
                    isAlreadyRead = ReadFiber(jsonReader, ingredient, isAlreadyRead);
                }

                if (jsonReader.TokenType == JsonToken.EndObject)
                    ingredients.Add(ingredient);
            }

            return ingredients;
        }

        private bool ReadId(JsonReader jsonReader, Ingredient ingredient, bool isAlreadyRead)
        {
            if (!isAlreadyRead)
            {
                if (IngredientSerializerDescription.Id.GetName(UsePropDescrShortName) == jsonReader.Value.ToString())
                {
                    jsonReader.Read();
                    ingredient.Id = new Guid(jsonReader.Value.ToString());

                    return true;
                }
            }

            return isAlreadyRead;
        }

        private bool ReadName(JsonReader jsonReader, Ingredient ingredient, bool isAlreadyRead)
        {
            if (!isAlreadyRead)
            {
                if (IngredientSerializerDescription.Name.GetName(UsePropDescrShortName) == jsonReader.Value.ToString())
                {
                    jsonReader.Read();
                    ingredient.Name = (string)jsonReader.Value;

                    return true;
                }
            }

            return isAlreadyRead;
        }

        private bool ReadCode(JsonReader jsonReader, Ingredient ingredient, bool isAlreadyRead)
        {
            if (!isAlreadyRead)
            {
                if (IngredientSerializerDescription.Code.GetName(UsePropDescrShortName) == jsonReader.Value.ToString())
                {
                    jsonReader.Read();
                    ingredient.Code = (string)jsonReader.Value;

                    return true;
                }
            }

            return isAlreadyRead;
        }

        private bool ReadIngredientKindId(JsonReader jsonReader, Ingredient ingredient, bool isAlreadyRead)
        {
            if (!isAlreadyRead)
            {
                if (IngredientSerializerDescription.IngredientKindId.GetName(UsePropDescrShortName) == jsonReader.Value.ToString())
                {
                    jsonReader.Read();
                    ingredient.IngredientKindId = new Guid(jsonReader.Value.ToString());

                    return true;
                }
            }

            return isAlreadyRead;
        }

        private bool ReadCalories(JsonReader jsonReader, Ingredient ingredient, bool isAlreadyRead)
        {
            if (!isAlreadyRead)
            {
                if (IngredientSerializerDescription.Calories.GetName(UsePropDescrShortName) == jsonReader.Value.ToString())
                {
                    jsonReader.Read();
                    ingredient.Calories = Convert.ToDecimal(jsonReader.Value, CultureInfo.InvariantCulture);

                    return true;
                }
            }

            return isAlreadyRead;
        }

        private bool ReadProtein(JsonReader jsonReader, Ingredient ingredient, bool isAlreadyRead)
        {
            if (!isAlreadyRead)
            {
                if (IngredientSerializerDescription.Protein.GetName(UsePropDescrShortName) == jsonReader.Value.ToString())
                {
                    jsonReader.Read();
                    ingredient.Protein = Convert.ToDecimal(jsonReader.Value, CultureInfo.InvariantCulture);

                    return true;
                }
            }

            return isAlreadyRead;
        }

        private bool ReadCarbohydrate(JsonReader jsonReader, Ingredient ingredient, bool isAlreadyRead)
        {
            if (!isAlreadyRead)
            {
                if (IngredientSerializerDescription.Carbohydrate.GetName(UsePropDescrShortName) == jsonReader.Value.ToString())
                {
                    jsonReader.Read();
                    ingredient.Carbohydrate = Convert.ToDecimal(jsonReader.Value, CultureInfo.InvariantCulture);

                    return true;
                }
            }

            return isAlreadyRead;
        }

        private bool ReadLipid(JsonReader jsonReader, Ingredient ingredient, bool isAlreadyRead)
        {
            if (!isAlreadyRead)
            {
                if (IngredientSerializerDescription.Lipid.GetName(UsePropDescrShortName) == jsonReader.Value.ToString())
                {
                    jsonReader.Read();
                    ingredient.Lipid = Convert.ToDecimal(jsonReader.Value, CultureInfo.InvariantCulture);

                    return true;
                }
            }

            return isAlreadyRead;
        }

        private bool ReadWater(JsonReader jsonReader, Ingredient ingredient, bool isAlreadyRead)
        {
            if (!isAlreadyRead)
            {
                if (IngredientSerializerDescription.Water.GetName(UsePropDescrShortName) == jsonReader.Value.ToString())
                {
                    jsonReader.Read();
                    ingredient.Water = Convert.ToDecimal(jsonReader.Value, CultureInfo.InvariantCulture);

                    return true;
                }
            }

            return isAlreadyRead;
        }

        private bool ReadFiber(JsonReader jsonReader, Ingredient ingredient, bool isAlreadyRead)
        {
            if (!isAlreadyRead)
            {
                if (IngredientSerializerDescription.Fiber.GetName(UsePropDescrShortName) == jsonReader.Value.ToString())
                {
                    jsonReader.Read();
                    ingredient.Fiber = Convert.ToDecimal(jsonReader.Value, CultureInfo.InvariantCulture);

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
