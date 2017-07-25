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
    public class RecipeInstructionJsonSerializer : IJsonSerializer<List<RecipeInstruction>>
    {
        private List<string> _fields;

        public bool UsePropDescrShortName { get; set; }
        public bool IsIndent { get; set; }

        public RecipeInstructionJsonSerializer()
        {
            UsePropDescrShortName = true;
            IsIndent = false;
            _fields = new List<string>(RecipeInstructionEntityDescription.AllLower);
        }

        public Stream Serialize(List<RecipeInstruction> instructions)
        {
            var stream = new MemoryStream();

            using (var writer = new StreamWriter(stream, Encoding.UTF8, 512, true))
            {
                using (var jsonWriter = new JsonTextWriter(writer))
                {
                    jsonWriter.Formatting = IsIndent ? Formatting.Indented : Formatting.None;
                    jsonWriter.CloseOutput = false;

                    Serialize(instructions, jsonWriter);
                }
            }

            stream.Position = 0;

            return stream;
        }

        public void Serialize(List<RecipeInstruction> instructions, JsonWriter jsonWriter)
        {
            jsonWriter.WriteStartArray();

            for (int i = 0; i < instructions.Count; i++)
            {
                jsonWriter.WriteStartObject();

                WriteRecipeId(instructions[i], jsonWriter);
                WriteInstruction(instructions[i], jsonWriter);
                WriteOrder(instructions[i], jsonWriter);

                jsonWriter.WriteEndObject();
            }

            jsonWriter.WriteEndArray();
        }

        private void WriteRecipeId(RecipeInstruction instruction, JsonWriter jsonWriter)
        {
            if (_fields.Contains(RecipeInstructionEntityDescription.RecipeId.ToLower()))
            {
                jsonWriter.WritePropertyName(RecipeInstructionSerializerDescription.RecipeId.GetName(UsePropDescrShortName));
                jsonWriter.WriteValue(instruction.RecipeId.ToString("N"));
            }
        }

        private void WriteInstruction(RecipeInstruction instruction, JsonWriter jsonWriter)
        {
            if (_fields.Contains(RecipeInstructionEntityDescription.Instruction.ToLower()))
            {
                jsonWriter.WritePropertyName(RecipeInstructionSerializerDescription.Instruction.GetName(UsePropDescrShortName));
                jsonWriter.WriteValue(instruction.Instruction);
            }
        }

        private void WriteOrder(RecipeInstruction instruction, JsonWriter jsonWriter)
        {
            if (_fields.Contains(RecipeInstructionEntityDescription.Order.ToLower()))
            {
                jsonWriter.WritePropertyName(RecipeInstructionSerializerDescription.Order.GetName(UsePropDescrShortName));
                jsonWriter.WriteValue(instruction.Order);
            }
        }

        public List<RecipeInstruction> Deserialize(Stream stream)
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

        public List<RecipeInstruction> Deserialize(JsonReader jsonReader)
        {
            var instructions = new List<RecipeInstruction>();
            RecipeInstruction instruction = null;

            while (jsonReader.Read() && jsonReader.TokenType != JsonToken.EndArray)
            {
                if (jsonReader.TokenType == JsonToken.StartObject)
                    instruction = new RecipeInstruction();

                if (jsonReader.TokenType == JsonToken.PropertyName)
                {
                    bool isAlreadyRead = false;
                    isAlreadyRead = ReadRecipeId(jsonReader, instruction, isAlreadyRead);
                    isAlreadyRead = ReadInstruction(jsonReader, instruction, isAlreadyRead);
                    isAlreadyRead = ReadOrder(jsonReader, instruction, isAlreadyRead);
                }

                if (jsonReader.TokenType == JsonToken.EndObject)
                    instructions.Add(instruction);
            }

            return instructions;
        }

        private bool ReadRecipeId(JsonReader jsonReader, RecipeInstruction instruction, bool isAlreadyRead)
        {
            if (!isAlreadyRead)
            {
                if (RecipeInstructionSerializerDescription.RecipeId.GetName(UsePropDescrShortName) == jsonReader.Value.ToString())
                {
                    jsonReader.Read();
                    instruction.RecipeId = new Guid(jsonReader.Value.ToString());

                    return true;
                }
            }

            return isAlreadyRead;
        }

        private bool ReadInstruction(JsonReader jsonReader, RecipeInstruction instruction, bool isAlreadyRead)
        {
            if (!isAlreadyRead)
            {
                if (RecipeInstructionSerializerDescription.Instruction.GetName(UsePropDescrShortName) == jsonReader.Value.ToString())
                {
                    jsonReader.Read();
                    instruction.Instruction = jsonReader.Value.ToString();

                    return true;
                }
            }

            return isAlreadyRead;
        }

        private bool ReadOrder(JsonReader jsonReader, RecipeInstruction instruction, bool isAlreadyRead)
        {
            if (!isAlreadyRead)
            {
                if (RecipeInstructionSerializerDescription.Order.GetName(UsePropDescrShortName) == jsonReader.Value.ToString())
                {
                    jsonReader.Read();
                    instruction.Order = Convert.ToInt32(jsonReader.Value, CultureInfo.InvariantCulture);

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
