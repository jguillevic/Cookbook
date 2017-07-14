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
    public class SeasonJsonSerializer : IJsonSerializer<List<Season>>
    {
        private List<string> _fields;

        public bool UsePropDescrShortName { get; set; }
        public bool IsIndent { get; set; }
        
        public SeasonJsonSerializer()
        {
            UsePropDescrShortName = true;
            IsIndent = false;
            _fields = new List<string>(SeasonEntityDescription.AllLower);
        }

        public Stream Serialize(List<Season> seasons)
        {
            var stream = new MemoryStream();

            using (var writer = new StreamWriter(stream, Encoding.UTF8, 512, true))
            {
                using (var jsonWriter = new JsonTextWriter(writer))
                {
                    jsonWriter.Formatting = IsIndent ? Formatting.Indented : Formatting.None;
                    jsonWriter.CloseOutput = false;

                    Serialize(seasons, jsonWriter);
                }
            }

            stream.Position = 0;

            return stream;
        }

        public void Serialize(List<Season> seasons, JsonWriter jsonWriter)
        {
            jsonWriter.WriteStartArray();

            for (int i = 0; i < seasons.Count; i++)
            {
                jsonWriter.WriteStartObject();

                WriteId(seasons[i], jsonWriter);
                WriteName(seasons[i], jsonWriter);
                WriteCode(seasons[i], jsonWriter);

                jsonWriter.WriteEndObject();
            }

            jsonWriter.WriteEndArray();
        }

        private void WriteId(Season season, JsonWriter jsonWriter)
        {
            if (_fields.Contains(SeasonEntityDescription.Id.ToLower()))
            {
                jsonWriter.WritePropertyName(SeasonSerializerDescription.Id.GetName(UsePropDescrShortName));
                jsonWriter.WriteValue(season.Id.ToString("N"));
            }
        }

        private void WriteName(Season season, JsonWriter jsonWriter)
        {
            if (_fields.Contains(SeasonEntityDescription.Name.ToLower()))
            {
                jsonWriter.WritePropertyName(SeasonSerializerDescription.Name.GetName(UsePropDescrShortName));
                jsonWriter.WriteValue(season.Name);
            }
        }

        private void WriteCode(Season season, JsonWriter jsonWriter)
        {
            if (_fields.Contains(SeasonEntityDescription.Code.ToLower()))
            {
                jsonWriter.WritePropertyName(SeasonSerializerDescription.Code.GetName(UsePropDescrShortName));
                jsonWriter.WriteValue(season.Code);
            }
        }

        public List<Season> Deserialize(Stream stream)
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

        public List<Season> Deserialize(JsonReader jsonReader)
        {
            var seasons = new List<Season>();
            Season season = null;

            while (jsonReader.Read())
            {
                if (jsonReader.TokenType == JsonToken.StartObject)
                    season = new Season();

                if (jsonReader.TokenType == JsonToken.PropertyName)
                {
                    bool isAlreadyRead = false;
                    isAlreadyRead = ReadId(jsonReader, season, isAlreadyRead);
                    isAlreadyRead = ReadName(jsonReader, season, isAlreadyRead);
                    isAlreadyRead = ReadCode(jsonReader, season, isAlreadyRead);
                }

                if (jsonReader.TokenType == JsonToken.EndObject)
                    seasons.Add(season);
            }

            return seasons;
        }

        private bool ReadId(JsonReader jsonReader, Season season, bool isAlreadyRead)
        {
            if (!isAlreadyRead)
            {
                if (SeasonSerializerDescription.Id.GetName(UsePropDescrShortName) == jsonReader.Value.ToString())
                {
                    jsonReader.Read();
                    season.Id = new Guid(jsonReader.Value.ToString());

                    return true;
                }
            }

            return isAlreadyRead;
        }

        private bool ReadName(JsonReader jsonReader, Season season, bool isAlreadyRead)
        {
            if (!isAlreadyRead)
            {
                if (SeasonSerializerDescription.Name.GetName(UsePropDescrShortName) == jsonReader.Value.ToString())
                {
                    jsonReader.Read();
                    season.Name = (string)jsonReader.Value;

                    return true;
                }
            }

            return isAlreadyRead;
        }

        private bool ReadCode(JsonReader jsonReader, Season season, bool isAlreadyRead)
        {
            if (!isAlreadyRead)
            {
                if (SeasonSerializerDescription.Code.GetName(UsePropDescrShortName) == jsonReader.Value.ToString())
                {
                    jsonReader.Read();
                    season.Code = (string)jsonReader.Value;

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
