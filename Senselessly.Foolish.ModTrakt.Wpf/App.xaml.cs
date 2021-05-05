namespace Senselessly.Foolish.ModTrakt.Wpf
{
    using System.IO.Abstractions;
    using System.Windows;
    using DotNetWindowsRegistry;
    using Interface.Models;
    using Interface.Navigation;
    using Interface.Services;
    using Interface.Settings;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Toolkit.Mvvm.DependencyInjection;
    using Microsoft.Toolkit.Mvvm.Messaging;
    using Models.App;
    using Models.Navigation;
    using Models.Settings;
    using Services.App;
    using Services.Messaging;
    using UI.Dialog.Exit;
    using UI.Dialog.Settings;
    using UI.GameList;
    using UI.Main;
    using UI.Module.ModList;
    using UI.Module.PluginList;
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
            services.AddScoped<IRegistry, WindowsRegistry>();
            services.AddScoped<ISettings, Settings>();
            services.AddScoped<IGameDictionary, GameDictionary>();
            services.AddScoped<IGameLocatorService, GameLocatorService>();
            services.AddScoped<IGameSettings, GameSettings>();
            services.AddSingleton<IAppSettings, AppSettings>();
            services.AddSingleton<IExceptionService, ExceptionService>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IRegistryScannerService, RegistryScannerService>();
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
            Current.DispatcherUnhandledException += (_, args) => {
                WeakReferenceMessenger.Default.Send(new ExceptionInfo(args.Exception));
                args.Handled = true;
            };
            var splashWindow = Ioc.Default.GetService<SplashWindow>();
            splashWindow?.Show();
        }
    }
}