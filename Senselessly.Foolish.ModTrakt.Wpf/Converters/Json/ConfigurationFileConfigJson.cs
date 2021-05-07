namespace Senselessly.Foolish.ModTrakt.Wpf.Converters.Json
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using Interfaces.Settings;
    using Models.Settings;

    internal sealed class ConfigurationFileConfigJson : JsonConverter<IConfigurationFileConfig>
    {
        public override IConfigurationFileConfig Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            var config = JsonSerializer.Deserialize<ConfigurationFileConfig>(reader: ref reader, options: options);
            return config;
        }

        public override void Write(Utf8JsonWriter writer, IConfigurationFileConfig value, JsonSerializerOptions options)
        {
            var config = JsonSerializer.Serialize(value: value, options: options);
            writer.WriteStringValue(config);
        }
    }
}