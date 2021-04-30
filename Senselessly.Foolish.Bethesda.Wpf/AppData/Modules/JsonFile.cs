namespace Senselessly.Foolish.Bethesda.Wpf.AppData.Modules
{
    using System.IO;
    using System.Reflection;
    using System.Text.Json;
    using System.Threading.Tasks;

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
            var jsonData = JsonSerializer.Deserialize<T>(jsonText);
            return jsonData;
        }
    }
}