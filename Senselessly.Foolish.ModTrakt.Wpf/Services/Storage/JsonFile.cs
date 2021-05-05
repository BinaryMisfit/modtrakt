namespace Senselessly.Foolish.ModTrakt.Wpf.Services.Storage
{
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Converters.Json;

    internal static class JsonFile
    {
        public static async Task<T> LoadResourceAsync<T>(string resourceName)
        {
            var resourceSpace = Assembly.GetExecutingAssembly()
                                        .GetTypes()
                                        .Where(
                                             t => t.Namespace != null && !t.Namespace.Equals("XamlGeneratedNamespace"))
                                        .Select(t => t.Namespace)
                                        .Distinct()
                                        .First();
            var resourcePath = $"{resourceSpace}.Resources.Json.{resourceName}.json";
            var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath);
            if (resource == null) { return default; }

            var jsonReader = new StreamReader(resource);
            var jsonText = await jsonReader.ReadToEndAsync();
            var jsonOptions = new JsonSerializerOptions {Converters = {new GameRegistryJson()}};
            var jsonData = JsonSerializer.Deserialize<T>(json: jsonText, options: jsonOptions);
            return jsonData;
        }
    }
}