namespace Senselessly.Foolish.ModTrakt.Client
{
    using System.CommandLine;
    using System.IO.Abstractions;
    using System.Threading.Tasks;
    using Bethesda.Archives.Interfaces.Services;
    using Bethesda.Archives.Services;
    using Commands;
    using Commands.List;
    using Interfaces.Commands;
    using Microsoft.Extensions.DependencyInjection;

    internal static class Program
    {
        private static ServiceProvider Provider { get; set; }

        public static async Task<int> Main(params string[] args)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            Provider = services.BuildServiceProvider();
            var builder = Provider.GetService<IRootCommandBuilder>();
            if (builder == null) return 0;

            var rootCommand = builder.BuildCommand();
            return await rootCommand.InvokeAsync(args);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();
            services.AddScoped<IFileSystem, FileSystem>();
            services.AddScoped<IPluginLocatorService, PluginLocatorService>();
            services.AddScoped<ICommandBuilder, ListCommandBuilder>();
            services.AddSingleton<IRootCommandBuilder, RootCommandBuilder>();
        }
    }
}