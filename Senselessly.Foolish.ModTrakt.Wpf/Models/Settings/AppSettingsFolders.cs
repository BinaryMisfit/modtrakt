namespace Senselessly.Foolish.ModTrakt.Wpf.Models.Settings
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Settings;

    internal sealed class AppSettingsFolders : IAppSettingsFolders
    {
        public string Data { get; set; }

        public string ExternalModules { get; set; }

        public string ExternalPlugins { get; set; }

        public string Games { get; set; }

        public string Modules { get; set; }

        public string Plugins { get; set; }

        public string Product { get; set; }

        public string User { get; set; }

        public IEnumerable<string> ToStringArray()
        {
            var paths = new List<string>();
            var source = GetType().GetProperties().ToList();
            source.ForEach(prop => {
                var value = prop.GetValue(this)?.ToString();
                if (value != null && !paths.Contains(value)) paths.Add(value);
            });

            return paths.OrderBy(p => p).Distinct();
        }
    }
}