namespace Senselessly.Foolish.Bethesda.Wpf
{
    using System.Windows;
    using AppData.Default;
    using AppData.Models;
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
            var appSettings = AppSettings.Load();
            services.AddSingleton(appSettings);
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
    }
}