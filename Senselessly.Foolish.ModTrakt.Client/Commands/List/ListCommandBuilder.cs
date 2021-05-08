namespace Senselessly.Foolish.ModTrakt.Client.Commands.List
{
    using System.CommandLine;
    using System.CommandLine.Invocation;
    using System.CommandLine.IO;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Bethesda.Archives.Enums;
    using Bethesda.Archives.Interfaces.Services;
    using Interfaces.Commands;

    internal sealed class ListCommandBuilder : ICommandBuilder
    {
        private readonly IPluginLocatorService _locatorService;

        public ListCommandBuilder(IPluginLocatorService locatorService) => _locatorService = locatorService;

        public Command BuildCommand()
        {
            var source = new Argument<DirectoryInfo>("path").ExistingOnly();
            var type = new Option<ModTypes>(aliases: new[] {
                    "-t",
                    "--type"
                },
                description: "Type of items to search for",
                getDefaultValue: () => ModTypes.Both);
            var recurse = new Option<bool>(aliases: new[] {
                    "-r",
                    "--recurse"
                },
                description: "Perform recursive search on path",
                getDefaultValue: () => false);
            var list = new Command(name: "list", description: "Lists all plugins or archives in a directory");
            list.AddAlias("li");
            list.AddArgument(source);
            list.AddOption(type);
            list.AddOption(recurse);
            list.Handler =
                CommandHandler.Create<DirectoryInfo, ModTypes, bool, IConsole, CancellationToken>(ListCommandHandler);
            return list;
        }

        private async Task<int> ListCommandHandler(
            DirectoryInfo path,
            ModTypes types,
            bool recurse,
            IConsole console,
            CancellationToken cancel)
        {
            console.Out.WriteLine($"[list] Starting search in {path.FullName}");
            if (!_locatorService.IsLocatorReady())
            {
                console.Out.WriteLine("[list] Locator not ready");
                return 0;
            }

            var files = _locatorService.Locate(path: path, type: types, recurse: recurse).ToList();
            if (files.Count > 0)
                await foreach (var file in files.OrderBy(f => f).ToAsyncEnumerable().WithCancellation(cancel))
                {
                    if (cancel.IsCancellationRequested) break;

                    console.Out.WriteLine($"[list] {file}");
                }

            console.Out.WriteLine($"[list] Found {files.Count}");
            return files.Count;
        }
    }
}