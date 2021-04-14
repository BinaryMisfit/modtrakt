namespace Senselessly.Foolish.Bethesda.Wpf.AppData.Modules
{
    using System.IO;
    using System.Reflection;
    using System.Text.Json;

    public static class JsonFile
    {
        public static T LoadResource<T>(string resourceName)
        {
            var resourcePath = $"{Assembly.GetExecutingAssembly().GetName().Name}.Embedded.{resourceName}.json";
            var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath);
            if (resource == null)
            {
                return default;
            }

            using var reader = new StreamReader(resource);
            var json = reader.ReadToEnd();
            var jsonData = JsonSerializer.Deserialize<T>(json);
            return jsonData;
        }
    }
}