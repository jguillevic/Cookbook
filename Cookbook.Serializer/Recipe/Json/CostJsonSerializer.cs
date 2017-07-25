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
    public class CostJsonSerializer : IJsonSerializer<List<Cost>>
    {
        private List<string> _fields;

        public bool UsePropDescrShortName { get; set; }
        public bool IsIndent { get; set; }
        
        public CostJsonSerializer()
        {
            UsePropDescrShortName = true;
            IsIndent = false;
            _fields = new List<string>(CostEntityDescription.AllLower);
        }

        public Stream Serialize(List<Cost> costs)
        {
            var stream = new MemoryStream();

            using (var writer = new StreamWriter(stream, Encoding.UTF8, 512, true))
            {
                using (var jsonWriter = new JsonTextWriter(writer))
                {
                    jsonWriter.Formatting = IsIndent ? Formatting.Indented : Formatting.None;
                    jsonWriter.CloseOutput = false;

                    Serialize(costs, jsonWriter);
                }
            }

            stream.Position = 0;

            return stream;
        }

        public void Serialize(List<Cost> costs, JsonWriter jsonWriter)
        {
            jsonWriter.WriteStartArray();

            for (int i = 0; i < costs.Count; i++)
            {
                jsonWriter.WriteStartObject();

                WriteId(costs[i], jsonWriter);
                WriteName(costs[i], jsonWriter);
                WriteCode(costs[i], jsonWriter);

                jsonWriter.WriteEndObject();
            }

            jsonWriter.WriteEndArray();
        }

        private void WriteId(Cost cost, JsonWriter jsonWriter)
        {
            if (_fields.Contains(CostEntityDescription.Id.ToLower()))
            {
                jsonWriter.WritePropertyName(CostSerializerDescription.Id.GetName(UsePropDescrShortName));
                jsonWriter.WriteValue(cost.Id.ToString("N"));
            }
        }

        private void WriteName(Cost cost, JsonWriter jsonWriter)
        {
            if (_fields.Contains(CostEntityDescription.Name.ToLower()))
            {
                jsonWriter.WritePropertyName(CostSerializerDescription.Name.GetName(UsePropDescrShortName));
                jsonWriter.WriteValue(cost.Name);
            }
        }

        private void WriteCode(Cost cost, JsonWriter jsonWriter)
        {
            if (_fields.Contains(CostEntityDescription.Code.ToLower()))
            {
                jsonWriter.WritePropertyName(CostSerializerDescription.Code.GetName(UsePropDescrShortName));
                jsonWriter.WriteValue(cost.Code);
            }
        }

        public List<Cost> Deserialize(Stream stream)
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

        public List<Cost> Deserialize(JsonReader jsonReader)
        {
            var costs = new List<Cost>();
            Cost cost = null;

            while (jsonReader.Read() && jsonReader.TokenType != JsonToken.EndArray)
            {
                if (jsonReader.TokenType == JsonToken.StartObject)
                    cost = new Cost();

                if (jsonReader.TokenType == JsonToken.PropertyName)
                {
                    bool isAlreadyRead = false;
                    isAlreadyRead = ReadId(jsonReader, cost, isAlreadyRead);
                    isAlreadyRead = ReadName(jsonReader, cost, isAlreadyRead);
                    isAlreadyRead = ReadCode(jsonReader, cost, isAlreadyRead);
                }

                if (jsonReader.TokenType == JsonToken.EndObject)
                    costs.Add(cost);
            }

            return costs;
        }

        private bool ReadId(JsonReader jsonReader, Cost cost, bool isAlreadyRead)
        {
            if (!isAlreadyRead)
            {
                if (CostSerializerDescription.Id.GetName(UsePropDescrShortName) == jsonReader.Value.ToString())
                {
                    jsonReader.Read();
                    cost.Id = new Guid(jsonReader.Value.ToString());

                    return true;
                }
            }

            return isAlreadyRead;
        }

        private bool ReadName(JsonReader jsonReader, Cost cost, bool isAlreadyRead)
        {
            if (!isAlreadyRead)
            {
                if (CostSerializerDescription.Name.GetName(UsePropDescrShortName) == jsonReader.Value.ToString())
                {
                    jsonReader.Read();
                    cost.Name = (string)jsonReader.Value;

                    return true;
                }
            }

            return isAlreadyRead;
        }

        private bool ReadCode(JsonReader jsonReader, Cost cost, bool isAlreadyRead)
        {
            if (!isAlreadyRead)
            {
                if (CostSerializerDescription.Code.GetName(UsePropDescrShortName) == jsonReader.Value.ToString())
                {
                    jsonReader.Read();
                    cost.Code = (string)jsonReader.Value;

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
