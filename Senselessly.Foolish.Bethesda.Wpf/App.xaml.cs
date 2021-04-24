namespace Senselessly.Foolish.Bethesda.Wpf
{
    using System.IO.Abstractions;
    using System.Windows;
    using AppData.Default;
    using AppData.Interface;
    using AppData.Models;
    using Microsoft.Extensions.DependencyInjection;
    using Services.Game;
    using Services.Helpers;
    using Services.Interface;
    using UI.Dialog.Settings;
    using UI.Main;
    using UI.Splash;

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
            services.AddScoped<IFileSystem, FileSystem>();
            services.AddScoped<ISettings, Settings>();
            services.AddScoped<IGameDictionary, GameDictionary>();
            services.AddScoped<IGameLocator, GameLocator>();
            services.AddScoped<IGameSettings, GameSettings>();
            services.AddSingleton<IAppSettings, AppSettings>();
            services.AddSingleton<IRegistryScanner, RegistryScanner>();
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<MainWindow>();
            services.AddSingleton<SettingsViewModel>();
            services.AddSingleton<SettingsDialog>();
            services.AddSingleton<SplashWindowViewModel>();
            services.AddSingleton<SplashWindow>();
        }

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var splashWindow = Shared.Provider.GetService<SplashWindow>();
            splashWindow?.Show();
        }
    }
}