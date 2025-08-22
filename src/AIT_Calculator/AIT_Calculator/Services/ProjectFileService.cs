using AIT_Calculator.Models;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AIT_Calculator.Services
{
    public static class ProjectFileService
    {
        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new ObservableCollectionConverter() }
        };

        public static void SaveProject(CarDataModel model, string filePath)
        {
            var json = JsonSerializer.Serialize(model, _jsonOptions);
            File.WriteAllText(filePath, json);
        }

        public static CarDataModel LoadProject(string filePath)
        {
            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<CarDataModel>(json, _jsonOptions);
        }
    }
    public class ObservableCollectionConverter : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsGenericType &&
                   typeToConvert.GetGenericTypeDefinition() == typeof(ObservableCollection<>);
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            Type elementType = typeToConvert.GetGenericArguments()[0];
            Type converterType = typeof(ObservableCollectionConverterInner<>).MakeGenericType(elementType);

            return (JsonConverter)Activator.CreateInstance(converterType);
        }

        private class ObservableCollectionConverterInner<T> : JsonConverter<ObservableCollection<T>>
        {
            public override ObservableCollection<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                var list = JsonSerializer.Deserialize<T[]>(ref reader, options);
                return new ObservableCollection<T>(list);
            }

            public override void Write(Utf8JsonWriter writer, ObservableCollection<T> value, JsonSerializerOptions options)
            {
                JsonSerializer.Serialize(writer, value.ToArray(), options);
            }
        }
    }
}