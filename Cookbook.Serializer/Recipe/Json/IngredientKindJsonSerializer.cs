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
    public class IngredientKindJsonSerializer : IJsonSerializer<List<IngredientKind>>
    {
        private List<string> _fields;

        public bool UsePropDescrShortName { get; set; }
        public bool IsIndent { get; set; }

        public IngredientKindJsonSerializer()
        {
            UsePropDescrShortName = true;
            IsIndent = false;
            _fields = new List<string>(IngredientKindEntityDescription.AllLower);
        }

        public Stream Serialize(List<IngredientKind> ingredientKinds)
        {
            var stream = new MemoryStream();

            using (var writer = new StreamWriter(stream, Encoding.UTF8, 512, true))
            {
                using (var jsonWriter = new JsonTextWriter(writer))
                {
                    jsonWriter.Formatting = IsIndent ? Formatting.Indented : Formatting.None;
                    jsonWriter.CloseOutput = false;

                    Serialize(ingredientKinds, jsonWriter);
                }
            }

            stream.Position = 0;

            return stream;
        }

        public void Serialize(List<IngredientKind> ingredientKinds, JsonWriter jsonWriter)
        {
            jsonWriter.WriteStartArray();

            for (int i = 0; i < ingredientKinds.Count; i++)
            {
                jsonWriter.WriteStartObject();

                WriteId(ingredientKinds[i], jsonWriter);
                WriteName(ingredientKinds[i], jsonWriter);
                WriteCode(ingredientKinds[i], jsonWriter);

                jsonWriter.WriteEndObject();
            }

            jsonWriter.WriteEndArray();
        }

        private void WriteId(IngredientKind ingredientKind, JsonWriter jsonWriter)
        {
            if (_fields.Contains(IngredientKindEntityDescription.Id.ToLower()))
            {
                jsonWriter.WritePropertyName(IngredientKindSerializerDescription.Id.GetName(UsePropDescrShortName));
                jsonWriter.WriteValue(ingredientKind.Id.ToString("N"));
            }
        }

        private void WriteName(IngredientKind ingredientKind, JsonWriter jsonWriter)
        {
            if (_fields.Contains(IngredientKindEntityDescription.Name.ToLower()))
            {
                jsonWriter.WritePropertyName(IngredientKindSerializerDescription.Name.GetName(UsePropDescrShortName));
                jsonWriter.WriteValue(ingredientKind.Name);
            }
        }

        private void WriteCode(IngredientKind ingredientKind, JsonWriter jsonWriter)
        {
            if (_fields.Contains(IngredientKindEntityDescription.Code.ToLower()))
            {
                jsonWriter.WritePropertyName(IngredientKindSerializerDescription.Code.GetName(UsePropDescrShortName));
                jsonWriter.WriteValue(ingredientKind.Code);
            }
        }

        public List<IngredientKind> Deserialize(Stream stream)
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

        public List<IngredientKind> Deserialize(JsonReader jsonReader)
        {
            var ingredientKinds = new List<IngredientKind>();
            IngredientKind ingredientKind = null;

            while (jsonReader.Read() && jsonReader.TokenType != JsonToken.EndArray)
            {
                if (jsonReader.TokenType == JsonToken.StartObject)
                    ingredientKind = new IngredientKind();

                if (jsonReader.TokenType == JsonToken.PropertyName)
                {
                    bool isAlreadyRead = false;
                    isAlreadyRead = ReadId(jsonReader, ingredientKind, isAlreadyRead);
                    isAlreadyRead = ReadName(jsonReader, ingredientKind, isAlreadyRead);
                    isAlreadyRead = ReadCode(jsonReader, ingredientKind, isAlreadyRead);
                }

                if (jsonReader.TokenType == JsonToken.EndObject)
                    ingredientKinds.Add(ingredientKind);
            }

            return ingredientKinds;
        }

        private bool ReadId(JsonReader jsonReader, IngredientKind ingredientKind, bool isAlreadyRead)
        {
            if (!isAlreadyRead)
            {
                if (IngredientKindSerializerDescription.Id.GetName(UsePropDescrShortName) == jsonReader.Value.ToString())
                {
                    jsonReader.Read();
                    ingredientKind.Id = new Guid(jsonReader.Value.ToString());

                    return true;
                }
            }

            return isAlreadyRead;
        }

        private bool ReadName(JsonReader jsonReader, IngredientKind ingredientKind, bool isAlreadyRead)
        {
            if (!isAlreadyRead)
            {
                if (IngredientKindSerializerDescription.Name.GetName(UsePropDescrShortName) == jsonReader.Value.ToString())
                {
                    jsonReader.Read();
                    ingredientKind.Name = (string)jsonReader.Value;

                    return true;
                }
            }

            return isAlreadyRead;
        }

        private bool ReadCode(JsonReader jsonReader, IngredientKind ingredientKind, bool isAlreadyRead)
        {
            if (!isAlreadyRead)
            {
                if (IngredientKindSerializerDescription.Code.GetName(UsePropDescrShortName) == jsonReader.Value.ToString())
                {
                    jsonReader.Read();
                    ingredientKind.Code = (string)jsonReader.Value;

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
