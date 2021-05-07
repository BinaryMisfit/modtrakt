namespace Senselessly.Foolish.ModTrakt.Wpf.Interfaces.Messaging.Settings
{
    using Interfaces.Settings;

    internal interface IOptionsAppSettings
    {
        IAppSettings Settings { get; }

        bool Handled { get; set; }
    }
}