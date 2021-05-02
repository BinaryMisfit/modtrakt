namespace Senselessly.Foolish.ModTrakt.Wpf.AppData.Converters
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using Interface;
    using Models;

    public class GameRegistryJson : JsonConverter<IGameRegistry>
    {
        public override IGameRegistry Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var registry = JsonSerializer.Deserialize<GameRegistry>(reader: ref reader);
            return registry;
        }

        public override void Write(Utf8JsonWriter writer, IGameRegistry value, JsonSerializerOptions options)
        {
            var registry = JsonSerializer.Serialize(value);
            writer.WriteStringValue(registry);
        }
    }
}