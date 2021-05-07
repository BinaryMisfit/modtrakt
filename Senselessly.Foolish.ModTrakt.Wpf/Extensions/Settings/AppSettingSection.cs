namespace Senselessly.Foolish.ModTrakt.Wpf.Extensions.Settings
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Settings;

    internal static class AppSettingSection
    {
        public static async Task<IAppSettingsSection> LoadKeys(
            this IAppSettingsSection section,
            IEnumerable<IConfigurationFileSectionKey> keys)
        {
            if (section == null) return null;

            if (keys == null) return section;

            await foreach (var key in keys.OrderBy(k => k.Key).ToAsyncEnumerable())
            {
                var property = section.GetType().GetProperty(key.Key);
                if (property != null && key.Value != null) property.SetValue(obj: section, value: key.Value);
            }

            return section;
        }
    }
}