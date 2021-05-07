namespace Senselessly.Foolish.ModTrakt.Wpf.Extensions.Messages
{
    using Interfaces.Settings;
    using Microsoft.Toolkit.Mvvm.Messaging;
    using Models.Messaging.Messages.Settings;
    using Models.Messaging.Options.Settings;

    internal static class AppSettingUpdate
    {
        public static void SendSettings(this IAppSettings sender)
        {
            if (sender == null) return;

            WeakReferenceMessenger.Default.Send(new AppSettingsMessage(new AppSettingsOptions(sender)));
        }
    }
}