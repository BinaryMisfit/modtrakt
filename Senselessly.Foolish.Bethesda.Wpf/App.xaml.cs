namespace Senselessly.Foolish.Bethesda.Wpf
{
    using System.IO.Abstractions;
    using System.Windows;
    using AppData.Interface;
    using AppData.Models;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Toolkit.Mvvm.DependencyInjection;
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
            Ioc.Default.ConfigureServices(services.BuildServiceProvider());
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
            services.AddSingleton<MainWindow>();
            services.AddTransient<MainWindowViewModel>();
            services.AddTransient<SettingsViewModel>();
            services.AddTransient<SplashWindowViewModel>();
            services.AddTransient<SplashWindow>();
        }

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var splashWindow = Ioc.Default.GetService<SplashWindow>();
            splashWindow?.Show();
        }
    }
}