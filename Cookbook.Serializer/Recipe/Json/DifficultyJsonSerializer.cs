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
    public class DifficultyJsonSerializer : IJsonSerializer<List<Difficulty>>
    {
        private List<string> _fields;

        public bool UsePropDescrShortName { get; set; }
        public bool IsIndent { get; set; }
     
        public DifficultyJsonSerializer()
        {
            UsePropDescrShortName = true;
            IsIndent = false;
            _fields = new List<string>(DifficultyEntityDescription.AllLower);
        }

        public Stream Serialize(List<Difficulty> difficulties)
        {
            var stream = new MemoryStream();

            using (var writer = new StreamWriter(stream, Encoding.UTF8, 512, true))
            {
                using (var jsonWriter = new JsonTextWriter(writer))
                {
                    jsonWriter.Formatting = IsIndent ? Formatting.Indented : Formatting.None;
                    jsonWriter.CloseOutput = false;

                    Serialize(difficulties, jsonWriter);
                }
            }

            stream.Position = 0;

            return stream;
        }

        public void Serialize(List<Difficulty> difficulties, JsonWriter jsonWriter)
        {
            jsonWriter.WriteStartArray();

            for (int i = 0; i < difficulties.Count; i++)
            {
                jsonWriter.WriteStartObject();

                WriteId(difficulties[i], jsonWriter);
                WriteName(difficulties[i], jsonWriter);
                WriteCode(difficulties[i], jsonWriter);

                jsonWriter.WriteEndObject();
            }

            jsonWriter.WriteEndArray();
        }

        private void WriteId(Difficulty difficulty, JsonWriter jsonWriter)
        {
            if (_fields.Contains(DifficultyEntityDescription.Id.ToLower()))
            {
                jsonWriter.WritePropertyName(DifficultySerializerDescription.Id.GetName(UsePropDescrShortName));
                jsonWriter.WriteValue(difficulty.Id.ToString("N"));
            }
        }

        private void WriteName(Difficulty difficulty, JsonWriter jsonWriter)
        {
            if (_fields.Contains(DifficultyEntityDescription.Name.ToLower()))
            {
                jsonWriter.WritePropertyName(DifficultySerializerDescription.Name.GetName(UsePropDescrShortName));
                jsonWriter.WriteValue(difficulty.Name);
            }
        }

        private void WriteCode(Difficulty difficulty, JsonWriter jsonWriter)
        {
            if (_fields.Contains(DifficultyEntityDescription.Code.ToLower()))
            {
                jsonWriter.WritePropertyName(DifficultySerializerDescription.Code.GetName(UsePropDescrShortName));
                jsonWriter.WriteValue(difficulty.Code);
            }
        }

        public List<Difficulty> Deserialize(Stream stream)
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

        public List<Difficulty> Deserialize(JsonReader jsonReader)
        {
            var difficultys = new List<Difficulty>();
            Difficulty difficulty = null;

            while (jsonReader.Read())
            {
                if (jsonReader.TokenType == JsonToken.StartObject)
                    difficulty = new Difficulty();

                if (jsonReader.TokenType == JsonToken.PropertyName)
                {
                    bool isAlreadyRead = false;
                    isAlreadyRead = ReadId(jsonReader, difficulty, isAlreadyRead);
                    isAlreadyRead = ReadName(jsonReader, difficulty, isAlreadyRead);
                    isAlreadyRead = ReadCode(jsonReader, difficulty, isAlreadyRead);
                }

                if (jsonReader.TokenType == JsonToken.EndObject)
                    difficultys.Add(difficulty);
            }

            return difficultys;
        }

        private bool ReadId(JsonReader jsonReader, Difficulty difficulty, bool isAlreadyRead)
        {
            if (!isAlreadyRead)
            {
                if (DifficultySerializerDescription.Id.GetName(UsePropDescrShortName) == jsonReader.Value.ToString())
                {
                    jsonReader.Read();
                    difficulty.Id = new Guid(jsonReader.Value.ToString());

                    return true;
                }
            }

            return isAlreadyRead;
        }

        private bool ReadName(JsonReader jsonReader, Difficulty difficulty, bool isAlreadyRead)
        {
            if (!isAlreadyRead)
            {
                if (DifficultySerializerDescription.Name.GetName(UsePropDescrShortName) == jsonReader.Value.ToString())
                {
                    jsonReader.Read();
                    difficulty.Name = (string)jsonReader.Value;

                    return true;
                }
            }

            return isAlreadyRead;
        }

        private bool ReadCode(JsonReader jsonReader, Difficulty difficulty, bool isAlreadyRead)
        {
            if (!isAlreadyRead)
            {
                if (DifficultySerializerDescription.Code.GetName(UsePropDescrShortName) == jsonReader.Value.ToString())
                {
                    jsonReader.Read();
                    difficulty.Code = (string)jsonReader.Value;

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
