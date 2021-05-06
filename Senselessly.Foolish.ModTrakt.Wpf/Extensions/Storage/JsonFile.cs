namespace Senselessly.Foolish.ModTrakt.Wpf.Extensions.Storage
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;
    using Converters.Json;
    using Interfaces.Extensions;
    using Messages;
    using Models.App;

    internal static class JsonFile
    {
        private static readonly IList<JsonConverter> Converters = new JsonConverter[] {
            new GameRegistryJson(),
            new FolderMapJson()
        };

        public static async Task<T> JsonResourceAsync<T>(this IJsonSource sender, string resourceName)
        {
            if (sender == null) { return default; }

            var resourceSpace = Assembly.GetExecutingAssembly()
                                        .GetTypes()
                                        .Where(
                                             t => t.Namespace != null && !t.Namespace.Equals("XamlGeneratedNamespace"))
                                        .Select(t => t.Namespace)
                                        .Distinct()
                                        .First();
            var resourcePath = $"{resourceSpace}.Resources.Json.{resourceName}.json";
            Stream resource;
            try { resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath); }
            catch (Exception e)
            {
                var message = new ExceptionInfo(sourceName: nameof(JsonFile), source: typeof(JsonFile), e: e);
                message.SendException();
                return default;
            }

            if (resource == null) { return default; }

            var jsonReader = new StreamReader(resource);
            var jsonText = await jsonReader.ReadToEndAsync();
            var jsonOptions = new JsonSerializerOptions();
            await Converters.ToAsyncEnumerable().ForEachAsync(convert => jsonOptions.Converters.Add(convert));
            try
            {
                var jsonData = JsonSerializer.Deserialize<T>(json: jsonText, options: jsonOptions);
                return jsonData;
            }
            catch (Exception e)
            {
                var message = new ExceptionInfo(sourceName: nameof(JsonFile), source: typeof(JsonFile), e: e);
                message.SendException();
                return default;
            }
        }
    }
}