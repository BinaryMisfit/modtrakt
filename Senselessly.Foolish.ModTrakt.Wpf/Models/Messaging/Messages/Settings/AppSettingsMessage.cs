namespace Senselessly.Foolish.ModTrakt.Wpf.Models.Messaging.Messages.Settings
{
    using Interfaces.Messaging.Settings;
    using Microsoft.Toolkit.Mvvm.Messaging.Messages;

    internal sealed class AppSettingsMessage : ValueChangedMessage<IOptionsAppSettings>
    {
        public AppSettingsMessage(IOptionsAppSettings value) : base(value) { }
    }
}