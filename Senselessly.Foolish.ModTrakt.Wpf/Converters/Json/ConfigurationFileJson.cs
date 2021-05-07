namespace Senselessly.Foolish.ModTrakt.Wpf.Converters.Json
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using Interfaces.Settings;
    using Models.Settings;

    internal sealed class ConfigurationFileJson : JsonConverter<IConfigurationFile>
    {
        public override IConfigurationFile Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            var configurationFile = JsonSerializer.Deserialize<ConfigurationFile>(reader: ref reader, options: options);
            return configurationFile;
        }

        public override void Write(Utf8JsonWriter writer, IConfigurationFile value, JsonSerializerOptions options)
        {
            var configurationFile = JsonSerializer.Serialize(value: value, options: options);
            writer.WriteStringValue(configurationFile);
        }
    }
}