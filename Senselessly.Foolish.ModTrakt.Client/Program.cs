namespace Senselessly.Foolish.ModTrakt.Client
{
    using System.IO.Abstractions;
    using System.Threading.Tasks;
    using Bethesda.Archives.Interfaces.Services;
    using Bethesda.Archives.Services;
    using Commands;
    using Commands.List;
    using Conceptual;
    using Interfaces.Commands;
    using Microsoft.Extensions.DependencyInjection;

    internal class Program : ConsoleProgram
    {
        public static async Task<int> Main(params string[] args)
        {
            var program = new Program();
            return await program.Run(args);
        }

        internal override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IFileSystem, FileSystem>();
            services.AddScoped<IPluginLocatorService, PluginLocatorService>();
            services.AddScoped<ICommandBuilder, ListCommandBuilder>();
            services.AddSingleton<IRootCommandBuilder, RootCommandBuilder>();
        }
    }
}