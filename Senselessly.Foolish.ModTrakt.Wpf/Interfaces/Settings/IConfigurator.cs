namespace Senselessly.Foolish.ModTrakt.Wpf.Interfaces.Settings
{
    using Messaging;

    internal interface IConfigurator : IMessageStatusUpdate
    {
        void Check(string key);

        void CheckFolders();

        void CheckConfigFile();
    }
}