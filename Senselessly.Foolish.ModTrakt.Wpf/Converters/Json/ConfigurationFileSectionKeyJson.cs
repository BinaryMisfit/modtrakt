namespace Senselessly.Foolish.ModTrakt.Wpf.Converters.Json
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using Interfaces.Settings;
    using Models.Settings;

    internal sealed class ConfigurationFileSectionKeyJson : JsonConverter<IConfigurationFileSectionKey>
    {
        public override IConfigurationFileSectionKey Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            var keys = JsonSerializer.Deserialize<ConfigurationFileSectionKey>(ref reader, options);
            return keys;
        }

        public override void Write(
            Utf8JsonWriter writer,
            IConfigurationFileSectionKey value,
            JsonSerializerOptions options)
        {
            var keys = JsonSerializer.Serialize(value, options);
            writer.WriteStringValue(keys);
        }
    }
}