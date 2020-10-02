using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Buffers;
using System.Buffers.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Web_API_Service.util {
	public class LongtoStringConverter : JsonConverter<long> {
        public override long Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options) {
            if (reader.TokenType == JsonTokenType.String) {
                ReadOnlySpan<byte> span = reader.HasValueSequence ? reader.ValueSequence.ToArray() : reader.ValueSpan;
                if (Utf8Parser.TryParse(span, out long number, out int bytesConsumed) && span.Length == bytesConsumed)
                    return number;

                if (Int64.TryParse(reader.GetString(), out number))
                    return number;
            }

            return reader.GetInt64();
        }

        public override void Write(Utf8JsonWriter writer, long longValue, JsonSerializerOptions options) {
            writer.WriteStringValue(longValue.ToString());
        }
    }
    public class ByteArrayConverter : JsonConverter<byte[]> {
        public override byte[] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
            short[] sByteArray = JsonSerializer.Deserialize<short[]>(ref reader);
            byte[] value = new byte[sByteArray.Length];
            for (int i = 0; i < sByteArray.Length; i++) {
                value[i] = (byte)sByteArray[i];
            }

            return value;
        }

        public override void Write(Utf8JsonWriter writer, byte[] value, JsonSerializerOptions options) {
            writer.WriteStartArray();

            foreach (var val in value) {
                writer.WriteNumberValue(val);
            }

            writer.WriteEndArray();
        }
    }


    public class ObjectToInferredTypesConverter
        : JsonConverter<object>
    {
        public override object Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.True)
            {
                return true;
            }

            if (reader.TokenType == JsonTokenType.False)
            {
                return false;
            }

            if (reader.TokenType == JsonTokenType.Number)
            {
                if (reader.TryGetInt64(out long l))
                {
                    return l;
                }

                return reader.GetDouble();
            }

            if (reader.TokenType == JsonTokenType.String)
            {
                if (reader.TryGetDateTime(out DateTime datetime))
                {
                    return datetime;
                }

                return reader.GetString();
            }

            // Use JsonElement as fallback.
            // Newtonsoft uses JArray or JObject.
            using JsonDocument document = JsonDocument.ParseValue(ref reader);
            return document.RootElement.Clone();
        }

        public override void Write(
            Utf8JsonWriter writer,
            object objectToWrite,
            JsonSerializerOptions options) =>
                throw new InvalidOperationException("Should not get here.");
    }





}
