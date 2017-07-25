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
    public class FeatureJsonSerializer : IJsonSerializer<List<Feature>>
    {
        private List<string> _fields;

        public bool UsePropDescrShortName { get; set; }
        public bool IsIndent { get; set; }

        public FeatureJsonSerializer()
        {
            UsePropDescrShortName = true;
            IsIndent = false;
            _fields = new List<string>(FeatureEntityDescription.AllLower);
        }

        public Stream Serialize(List<Feature> features)
        {
            var stream = new MemoryStream();

            using (var writer = new StreamWriter(stream, Encoding.UTF8, 512, true))
            {
                using (var jsonWriter = new JsonTextWriter(writer))
                {
                    jsonWriter.Formatting = IsIndent ? Formatting.Indented : Formatting.None;
                    jsonWriter.CloseOutput = false;

                    Serialize(features, jsonWriter);
                }
            }

            stream.Position = 0;

            return stream;
        }

        public void Serialize(List<Feature> features, JsonWriter jsonWriter)
        {
            jsonWriter.WriteStartArray();

            for (int i = 0; i < features.Count; i++)
            {
                jsonWriter.WriteStartObject();

                WriteId(features[i], jsonWriter);
                WriteName(features[i], jsonWriter);
                WriteCode(features[i], jsonWriter);

                jsonWriter.WriteEndObject();
            }

            jsonWriter.WriteEndArray();
        }

        private void WriteId(Feature feature, JsonWriter jsonWriter)
        {
            if (_fields.Contains(FeatureEntityDescription.Id.ToLower()))
            {
                jsonWriter.WritePropertyName(FeatureSerializerDescription.Id.GetName(UsePropDescrShortName));
                jsonWriter.WriteValue(feature.Id.ToString("N"));
            }
        }

        private void WriteName(Feature feature, JsonWriter jsonWriter)
        {
            if (_fields.Contains(FeatureEntityDescription.Name.ToLower()))
            {
                jsonWriter.WritePropertyName(FeatureSerializerDescription.Name.GetName(UsePropDescrShortName));
                jsonWriter.WriteValue(feature.Name);
            }
        }

        private void WriteCode(Feature feature, JsonWriter jsonWriter)
        {
            if (_fields.Contains(FeatureEntityDescription.Code.ToLower()))
            {
                jsonWriter.WritePropertyName(FeatureSerializerDescription.Code.GetName(UsePropDescrShortName));
                jsonWriter.WriteValue(feature.Code);
            }
        }

        public List<Feature> Deserialize(Stream stream)
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

        public List<Feature> Deserialize(JsonReader jsonReader)
        {
            var features = new List<Feature>();
            Feature feature = null;

            while (jsonReader.Read() && jsonReader.TokenType != JsonToken.EndArray)
            {
                if (jsonReader.TokenType == JsonToken.StartObject)
                    feature = new Feature();

                if (jsonReader.TokenType == JsonToken.PropertyName)
                {
                    bool isAlreadyRead = false;
                    isAlreadyRead = ReadId(jsonReader, feature, isAlreadyRead);
                    isAlreadyRead = ReadName(jsonReader, feature, isAlreadyRead);
                    isAlreadyRead = ReadCode(jsonReader, feature, isAlreadyRead);
                }

                if (jsonReader.TokenType == JsonToken.EndObject)
                    features.Add(feature);
            }

            return features;
        }

        private bool ReadId(JsonReader jsonReader, Feature feature, bool isAlreadyRead)
        {
            if (!isAlreadyRead)
            {
                if (FeatureSerializerDescription.Id.GetName(UsePropDescrShortName) == jsonReader.Value.ToString())
                {
                    jsonReader.Read();
                    feature.Id = new Guid(jsonReader.Value.ToString());

                    return true;
                }
            }

            return isAlreadyRead;
        }

        private bool ReadName(JsonReader jsonReader, Feature feature, bool isAlreadyRead)
        {
            if (!isAlreadyRead)
            {
                if (FeatureSerializerDescription.Name.GetName(UsePropDescrShortName) == jsonReader.Value.ToString())
                {
                    jsonReader.Read();
                    feature.Name = (string)jsonReader.Value;

                    return true;
                }
            }

            return isAlreadyRead;
        }

        private bool ReadCode(JsonReader jsonReader, Feature feature, bool isAlreadyRead)
        {
            if (!isAlreadyRead)
            {
                if (FeatureSerializerDescription.Code.GetName(UsePropDescrShortName) == jsonReader.Value.ToString())
                {
                    jsonReader.Read();
                    feature.Code = (string)jsonReader.Value;

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
