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
    public class MeasureJsonSerializer : IJsonSerializer<List<Measure>>
    {
        private List<string> _fields;

        public bool UsePropDescrShortName { get; set; }
        public bool IsIndent { get; set; }
        
        public MeasureJsonSerializer()
        {
            UsePropDescrShortName = true;
            IsIndent = false;
            _fields = new List<string>(MeasureEntityDescription.AllLower);
        }

        public Stream Serialize(List<Measure> measures)
        {
            var stream = new MemoryStream();

            using (var writer = new StreamWriter(stream, Encoding.UTF8, 512, true))
            {
                using (var jsonWriter = new JsonTextWriter(writer))
                {
                    jsonWriter.Formatting = IsIndent ? Formatting.Indented : Formatting.None;
                    jsonWriter.CloseOutput = false;

                    Serialize(measures, jsonWriter);
                }
            }

            stream.Position = 0;

            return stream;
        }

        public void Serialize(List<Measure> measures, JsonWriter jsonWriter)
        {
            jsonWriter.WriteStartArray();

            for (int i = 0; i < measures.Count; i++)
            {
                jsonWriter.WriteStartObject();

                WriteId(measures[i], jsonWriter);
                WriteName(measures[i], jsonWriter);
                WriteCode(measures[i], jsonWriter);

                jsonWriter.WriteEndObject();
            }

            jsonWriter.WriteEndArray();
        }

        private void WriteId(Measure measure, JsonWriter jsonWriter)
        {
            if (_fields.Contains(MeasureEntityDescription.Id.ToLower()))
            {
                jsonWriter.WritePropertyName(MeasureSerializerDescription.Id.GetName(UsePropDescrShortName));
                jsonWriter.WriteValue(measure.Id.ToString("N"));
            }
        }

        private void WriteName(Measure measure, JsonWriter jsonWriter)
        {
            if (_fields.Contains(MeasureEntityDescription.Name.ToLower()))
            {
                jsonWriter.WritePropertyName(MeasureSerializerDescription.Name.GetName(UsePropDescrShortName));
                jsonWriter.WriteValue(measure.Name);
            }
        }

        private void WriteCode(Measure measure, JsonWriter jsonWriter)
        {
            if (_fields.Contains(MeasureEntityDescription.Code.ToLower()))
            {
                jsonWriter.WritePropertyName(MeasureSerializerDescription.Code.GetName(UsePropDescrShortName));
                jsonWriter.WriteValue(measure.Code);
            }
        }

        public List<Measure> Deserialize(Stream stream)
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

        public List<Measure> Deserialize(JsonReader jsonReader)
        {
            var measures = new List<Measure>();
            Measure measure = null;

            while (jsonReader.Read() && jsonReader.TokenType != JsonToken.EndArray)
            {
                if (jsonReader.TokenType == JsonToken.StartObject)
                    measure = new Measure();

                if (jsonReader.TokenType == JsonToken.PropertyName)
                {
                    bool isAlreadyRead = false;
                    isAlreadyRead = ReadId(jsonReader, measure, isAlreadyRead);
                    isAlreadyRead = ReadName(jsonReader, measure, isAlreadyRead);
                    isAlreadyRead = ReadCode(jsonReader, measure, isAlreadyRead);
                }

                if (jsonReader.TokenType == JsonToken.EndObject)
                    measures.Add(measure);
            }

            return measures;
        }

        private bool ReadId(JsonReader jsonReader, Measure measure, bool isAlreadyRead)
        {
            if (!isAlreadyRead)
            {
                if (MeasureSerializerDescription.Id.GetName(UsePropDescrShortName) == jsonReader.Value.ToString())
                {
                    jsonReader.Read();
                    measure.Id = new Guid(jsonReader.Value.ToString());

                    return true;
                }
            }

            return isAlreadyRead;
        }

        private bool ReadName(JsonReader jsonReader, Measure measure, bool isAlreadyRead)
        {
            if (!isAlreadyRead)
            {
                if (MeasureSerializerDescription.Name.GetName(UsePropDescrShortName) == jsonReader.Value.ToString())
                {
                    jsonReader.Read();
                    measure.Name = (string)jsonReader.Value;

                    return true;
                }
            }

            return isAlreadyRead;
        }

        private bool ReadCode(JsonReader jsonReader, Measure measure, bool isAlreadyRead)
        {
            if (!isAlreadyRead)
            {
                if (MeasureSerializerDescription.Code.GetName(UsePropDescrShortName) == jsonReader.Value.ToString())
                {
                    jsonReader.Read();
                    measure.Code = (string)jsonReader.Value;

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
