namespace Senselessly.Foolish.ModTrakt.Wpf.Models.Settings
{
    using System;
    using Interfaces.Settings;

    internal sealed class AppSettingsGeneral : IAppSettingsGeneral
    {
        public int CompareTo(IAppSettingsGeneral other)
        {
            if (other == null) return 1;

            var compare = string.Compare(strA: ActiveGame,
                strB: other.ActiveGame,
                comparisonType: StringComparison.Ordinal);
            if (compare != 0) compare = compare > 0 ? 1 : -1;

            return compare;
        }

        public string ActiveGame { get; set; }
    }
}