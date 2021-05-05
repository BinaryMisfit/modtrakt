namespace Senselessly.Foolish.ModTrakt.Wpf.Models.Settings
{
    using Extension.Messages;
    using Interfaces.Settings;
    using Properties;

    internal sealed class Configurator : IConfigurator
    {
        private readonly IAppSettings _settings;

        public Configurator(IAppSettings settings) => _settings = settings;

        public void Check(string key)
        {
            this.SendUpdate(Resources.Check_Start);
            CheckFolders();
            CheckConfigFile();
        }

        public void CheckFolders()
        {
            var productFolder = _settings.ProductFolder;
            var userFolder = _settings.UserFolder;
            this.SendUpdate(Resources.Check_Folders);
        }

        public void CheckConfigFile()
        {
            this.SendUpdate(Resources.Check_Config_File);
        }

        private void LoadConfigJson(string key)
        {
            // TODO Implement Load Config
        }
    }
}