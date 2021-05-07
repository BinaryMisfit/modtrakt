namespace Senselessly.Foolish.ModTrakt.Wpf.Converters.Json
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using Interfaces.Settings;
    using Models.Settings;

    internal sealed class ConfigurationFileSectionJson : JsonConverter<IConfigurationFileSection>
    {
        public override IConfigurationFileSection Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            var section = JsonSerializer.Deserialize<ConfigurationFileSection>(reader: ref reader, options: options);
            return section;
        }

        public override void Write(
            Utf8JsonWriter writer,
            IConfigurationFileSection value,
            JsonSerializerOptions options)
        {
            var section = JsonSerializer.Serialize(value: value, options: options);
            writer.WriteStringValue(section);
        }
    }
}