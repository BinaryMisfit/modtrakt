namespace Senselessly.Foolish.ModTrakt.Wpf.Converters.Json
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using Interfaces.Settings;
    using Models.Settings;

    internal sealed class ConfigurationFileFolderJson : JsonConverter<IConfigurationFileFolder>
    {
        public override IConfigurationFileFolder Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            var folderMap = JsonSerializer.Deserialize<ConfigurationFileFolder>(reader: ref reader, options: options);
            return folderMap;
        }

        public override void Write(Utf8JsonWriter writer, IConfigurationFileFolder value, JsonSerializerOptions options)
        {
            var folderMap = JsonSerializer.Serialize(value: value, options: options);
            writer.WriteStringValue(folderMap);
        }
    }
}