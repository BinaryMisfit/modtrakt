namespace Senselessly.Foolish.ModTrakt.Client.Commands.List
{
    using System.CommandLine;
    using System.CommandLine.Invocation;
    using System.CommandLine.IO;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Bethesda.Archives.Interfaces;
    using Bethesda.Archives.LibraryData;
    using Interfaces;

    public class ListCommandBuilder : ICommandBuilder
    {
        private readonly IPluginLocator _locator;

        public ListCommandBuilder(IPluginLocator locator) => _locator = locator;

        public Command BuildCommand()
        {
            var source = new Argument<DirectoryInfo>("path").ExistingOnly();
            var type = new Option<ModType>(aliases: new[] {
                    "-t",
                    "--type"
                },
                description: "Type of items to search for",
                getDefaultValue: () => ModType.Both);
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
                CommandHandler.Create<DirectoryInfo, ModType, bool, IConsole, CancellationToken>(ListCommandHandler);
            return list;
        }

        private async Task<int> ListCommandHandler(
            DirectoryInfo path,
            ModType type,
            bool recurse,
            IConsole console,
            CancellationToken cancel)
        {
            console.Out.WriteLine($"[list] Starting search in {path.FullName}");
            var files = _locator.Locate(path).ToList();
            if (files.Count > 0)
            {
                await foreach (var file in files.OrderBy(f => f).ToAsyncEnumerable().WithCancellation(cancel))
                {
                    if (cancel.IsCancellationRequested) { break; }

                    console.Out.WriteLine($"[list] {file}");
                }
            }

            console.Out.WriteLine($"[list] Found {files.Count}");
            return files.Count;
        }
    }
}