using System.Text.Json.Serialization;
using System.Text.Json;

namespace BearsEngine.Source.Tools.IO.JSON;

public class TwoDimensionalArrayConverter : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert.IsArray && typeToConvert.GetArrayRank() == 2;
    }

    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        Type elementType = typeToConvert.GetElementType();

        // Create the converter with the correct generic type argument
        JsonConverter converter = (JsonConverter)Activator.CreateInstance(
            typeof(TwoDimensionalArrayConverterInner<>).MakeGenericType(elementType));

        return converter;
    }

    private class TwoDimensionalArrayConverterInner<TElement> : JsonConverter<TElement[,]>
    {
        public override TElement[,] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartArray)
            {
                throw new JsonException();
            }

            var rows = new List<TElement[]>();
            while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
            {
                if (reader.TokenType != JsonTokenType.StartArray)
                {
                    throw new JsonException();
                }

                var columns = new List<TElement>();
                while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                {
                    TElement element = JsonSerializer.Deserialize<TElement>(ref reader, options);
                    columns.Add(element);
                }

                rows.Add(columns.ToArray());
            }

            return ConvertToTwoDimensionalArray(rows);
        }

        public override void Write(Utf8JsonWriter writer, TElement[,] value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            for (int i = 0; i < value.GetLength(0); i++)
            {
                writer.WriteStartArray();
                for (int j = 0; j < value.GetLength(1); j++)
                {
                    TElement element = value[i, j];
                    JsonSerializer.Serialize(writer, element, options);
                }
                writer.WriteEndArray();
            }
            writer.WriteEndArray();
        }

        private static TElement[,] ConvertToTwoDimensionalArray(List<TElement[]> list)
        {
            int rowCount = list.Count;
            int columnCount = list.Max(x => x.Length);

            TElement[,] result = new TElement[rowCount, columnCount];
            for (int i = 0; i < rowCount; i++)
            {
                var row = list[i];
                for (int j = 0; j < row.Length; j++)
                {
                    result[i, j] = row[j];
                }
            }
            return result;
        }
    }
}