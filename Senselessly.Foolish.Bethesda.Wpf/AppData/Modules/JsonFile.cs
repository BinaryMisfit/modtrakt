namespace Senselessly.Foolish.Bethesda.Wpf.AppData.Modules
{
    using System.IO;
    using System.Reflection;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Converters;

    public static class JsonFile
    {
        public static async Task<T> LoadResourceAsync<T>(string resourceName)
        {
            var resourcePath = $"{Assembly.GetExecutingAssembly().GetName().Name}.Embedded.{resourceName}.json";
            var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath);
            if (resource == null)
            {
                return default;
            }

            var jsonReader = new StreamReader(resource);
            var jsonText = await jsonReader.ReadToEndAsync();
            var jsonOptions = new JsonSerializerOptions()
            {
                Converters =
                {
                    new GameRegistryJson(),
                },
            };
            var jsonData = JsonSerializer.Deserialize<T>(json: jsonText, options: jsonOptions);
            return jsonData;
        }
    }
}