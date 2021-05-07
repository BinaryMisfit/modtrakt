namespace Senselessly.Foolish.ModTrakt.Wpf
{
    using System.IO.Abstractions;
    using System.Windows;
    using DotNetWindowsRegistry;
    using Extensions.Storage;
    using Interfaces.App;
    using Interfaces.Navigation;
    using Interfaces.Services;
    using Interfaces.Settings;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Toolkit.Mvvm.DependencyInjection;
    using Microsoft.Toolkit.Mvvm.Messaging;
    using Models.App;
    using Models.Settings;
    using Services.App;
    using Services.Messaging;
    using Services.Navigation;
    using Services.Settings;
    using UI.Controls.ExceptionBox;
    using UI.Dialog.Exit;
    using UI.Dialog.Settings;
    using UI.GameList;
    using UI.Main;
    using UI.Modules.ModList;
    using UI.Modules.PluginList;
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
            var settings = new AppSettings();
            settings.LoadConfigFile(ConfigKeys.FileAppConfig);
            services.AddSingleton<IAppSettings>(settings);
            services.AddScoped<IFileSystem, FileSystem>();
            services.AddScoped<IRegistry, WindowsRegistry>();
            services.AddScoped<IGameDictionary, GameDictionary>();
            services.AddScoped<IGameLocatorService, GameLocatorService>();
            services.AddScoped<IGameSettings, GameSettings>();
            services.AddSingleton<IConfiguratorService, ConfiguratorService>();
            services.AddSingleton<IExceptionService, ExceptionService>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IRegistryScannerService, RegistryScannerService>();
            services.AddTransient<ExceptionViewModel>();
            services.AddTransient<ExitDialog>();
            services.AddTransient<GameListWindow>();
            services.AddTransient<GameListViewModel>();
            services.AddTransient<MainWindow>();
            services.AddTransient<MainViewModel>();
            services.AddTransient<ModListModule>();
            services.AddTransient<ModListViewModel>();
            services.AddTransient<PluginListModule>();
            services.AddTransient<PluginListViewModel>();
            services.AddTransient<SettingsDialog>();
            services.AddTransient<SettingsViewModel>();
            services.AddTransient<SplashViewModel>();
            services.AddTransient<SplashWindow>();
        }

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            Current.DispatcherUnhandledException += (s, args) => {
                WeakReferenceMessenger.Default.Send(new ExceptionInfo(sourceName: nameof(s),
                    source: s.GetType(),
                    e: args.Exception));
                args.Handled = true;
            };
            var splashWindow = Ioc.Default.GetService<SplashWindow>();
            splashWindow?.Show();
        }
    }
}