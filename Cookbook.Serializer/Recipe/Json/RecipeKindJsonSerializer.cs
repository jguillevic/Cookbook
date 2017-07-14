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
    public class RecipeKindJsonSerializer : IJsonSerializer<List<RecipeKind>>
    {
        private List<string> _fields;

        public bool UsePropDescrShortName { get; set; }
        public bool IsIndent { get; set; }

        public RecipeKindJsonSerializer()
        {
            UsePropDescrShortName = true;
            IsIndent = false;
            _fields = new List<string>(RecipeKindEntityDescription.AllLower);
        }

        public Stream Serialize(List<RecipeKind> recipeKinds)
        {
            var stream = new MemoryStream();

            using (var writer = new StreamWriter(stream, Encoding.UTF8, 512, true))
            {
                using (var jsonWriter = new JsonTextWriter(writer))
                {
                    jsonWriter.Formatting = IsIndent ? Formatting.Indented : Formatting.None;
                    jsonWriter.CloseOutput = false;

                    Serialize(recipeKinds, jsonWriter);
                }
            }

            stream.Position = 0;

            return stream;
        }

        public void Serialize(List<RecipeKind> recipeKinds, JsonWriter jsonWriter)
        {
            jsonWriter.WriteStartArray();

            for (int i = 0; i < recipeKinds.Count; i++)
            {
                jsonWriter.WriteStartObject();

                WriteId(recipeKinds[i], jsonWriter);
                WriteName(recipeKinds[i], jsonWriter);
                WriteCode(recipeKinds[i], jsonWriter);

                jsonWriter.WriteEndObject();
            }

            jsonWriter.WriteEndArray();
        }

        private void WriteId(RecipeKind recipeKind, JsonWriter jsonWriter)
        {
            if (_fields.Contains(RecipeKindEntityDescription.Id.ToLower()))
            {
                jsonWriter.WritePropertyName(RecipeKindSerializerDescription.Id.GetName(UsePropDescrShortName));
                jsonWriter.WriteValue(recipeKind.Id.ToString("N"));
            }
        }

        private void WriteName(RecipeKind recipeKind, JsonWriter jsonWriter)
        {
            if (_fields.Contains(RecipeKindEntityDescription.Name.ToLower()))
            {
                jsonWriter.WritePropertyName(RecipeKindSerializerDescription.Name.GetName(UsePropDescrShortName));
                jsonWriter.WriteValue(recipeKind.Name);
            }
        }

        private void WriteCode(RecipeKind recipeKind, JsonWriter jsonWriter)
        {
            if (_fields.Contains(RecipeKindEntityDescription.Code.ToLower()))
            {
                jsonWriter.WritePropertyName(RecipeKindSerializerDescription.Code.GetName(UsePropDescrShortName));
                jsonWriter.WriteValue(recipeKind.Code);
            }
        }

        public List<RecipeKind> Deserialize(Stream stream)
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

        public List<RecipeKind> Deserialize(JsonReader jsonReader)
        {
            var recipeKinds = new List<RecipeKind>();
            RecipeKind recipeKind = null;

            while (jsonReader.Read())
            {
                if (jsonReader.TokenType == JsonToken.StartObject)
                    recipeKind = new RecipeKind();

                if (jsonReader.TokenType == JsonToken.PropertyName)
                {
                    bool isAlreadyRead = false;
                    isAlreadyRead = ReadId(jsonReader, recipeKind, isAlreadyRead);
                    isAlreadyRead = ReadName(jsonReader, recipeKind, isAlreadyRead);
                    isAlreadyRead = ReadCode(jsonReader, recipeKind, isAlreadyRead);
                }

                if (jsonReader.TokenType == JsonToken.EndObject)
                    recipeKinds.Add(recipeKind);
            }

            return recipeKinds;
        }

        private bool ReadId(JsonReader jsonReader, RecipeKind recipeKind, bool isAlreadyRead)
        {
            if (!isAlreadyRead)
            {
                if (RecipeKindSerializerDescription.Id.GetName(UsePropDescrShortName) == jsonReader.Value.ToString())
                {
                    jsonReader.Read();
                    recipeKind.Id = new Guid(jsonReader.Value.ToString());

                    return true;
                }
            }

            return isAlreadyRead;
        }

        private bool ReadName(JsonReader jsonReader, RecipeKind recipeKind, bool isAlreadyRead)
        {
            if (!isAlreadyRead)
            {
                if (RecipeKindSerializerDescription.Name.GetName(UsePropDescrShortName) == jsonReader.Value.ToString())
                {
                    jsonReader.Read();
                    recipeKind.Name = (string)jsonReader.Value;

                    return true;
                }
            }

            return isAlreadyRead;
        }

        private bool ReadCode(JsonReader jsonReader, RecipeKind recipeKind, bool isAlreadyRead)
        {
            if (!isAlreadyRead)
            {
                if (RecipeKindSerializerDescription.Code.GetName(UsePropDescrShortName) == jsonReader.Value.ToString())
                {
                    jsonReader.Read();
                    recipeKind.Code = (string)jsonReader.Value;

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
