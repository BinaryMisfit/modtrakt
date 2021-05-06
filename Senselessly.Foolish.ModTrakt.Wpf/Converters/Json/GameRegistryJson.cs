namespace Senselessly.Foolish.ModTrakt.Wpf.Converters.Json
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using Interfaces.App;
    using Models.App;

    internal sealed class GameRegistryJson : JsonConverter<IGameRegistry>
    {
        public override IGameRegistry Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var registry = JsonSerializer.Deserialize<GameRegistry>(reader: ref reader, options: options);
            return registry;
        }

        public override void Write(Utf8JsonWriter writer, IGameRegistry value, JsonSerializerOptions options)
        {
            var registry = JsonSerializer.Serialize(value: value, options: options);
            writer.WriteStringValue(registry);
        }
    }
}