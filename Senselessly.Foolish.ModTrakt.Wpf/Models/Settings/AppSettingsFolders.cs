namespace Senselessly.Foolish.ModTrakt.Wpf.Models.Settings
{
    using System;
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

        public int CompareTo(IAppSettingsFolders other)
        {
            if (other == null) return 1;

            var compare = string.Compare(strA: Data, strB: other.Data, comparisonType: StringComparison.Ordinal);
            compare += string.Compare(strA: ExternalModules,
                strB: other.ExternalModules,
                comparisonType: StringComparison.Ordinal);
            compare += string.Compare(strA: ExternalPlugins,
                strB: other.ExternalPlugins,
                comparisonType: StringComparison.Ordinal);
            compare += string.Compare(strA: Games, strB: other.Games, comparisonType: StringComparison.Ordinal);
            compare += string.Compare(strA: Modules, strB: other.Modules, comparisonType: StringComparison.Ordinal);
            compare += string.Compare(strA: Plugins, strB: other.Plugins, comparisonType: StringComparison.Ordinal);
            compare += string.Compare(strA: Product, strB: other.Product, comparisonType: StringComparison.Ordinal);
            if (compare != 0) compare = compare > 0 ? 1 : -1;

            return compare;
        }

        public IEnumerable<string> ToStringArray() =>
            new[] {
                Data,
                ExternalModules,
                ExternalPlugins,
                Games,
                Modules,
                Plugins,
                Product
            }.Distinct();
    }
}