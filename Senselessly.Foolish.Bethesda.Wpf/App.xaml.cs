namespace Senselessly.Foolish.Bethesda.Wpf
{
    using System.Windows;
    using AppData.Default;
    using AppData.Interface;
    using AppData.Models;
    using AppData.Modules;
    using Microsoft.Extensions.DependencyInjection;
    using UI.Dialog.Settings;
    using UI.Main;

    public partial class App
    {
        public App()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            Shared.Provider = services.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            var settings = LoadSettings();
            services.AddSingleton(settings);
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<MainWindow>();
            services.AddSingleton<SettingsViewModel>();
            services.AddSingleton<SettingsDialog>();
        }

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = Shared.Provider.GetService<MainWindow>();
            mainWindow?.Show();
        }

        private static ISettings LoadSettings()
        {
            return ConfigFile.LoadIni<Settings>(Config.SettingsPath);
        }
    }
}