namespace Senselessly.Foolish.ModTrakt.Client.Commands
{
    using System.CommandLine;
    using Interfaces;

    public class RootCommandBuilder : IRootCommandBuilder
    {
        private readonly ICommandBuilder _list;

        public RootCommandBuilder(ICommandBuilder list) => _list = list;

        public RootCommand BuildCommand() =>
            new RootCommand(description: "Performs various command line tasks on Bethesda Archives") {
                _list.BuildCommand()
            };
    }
}