namespace Senselessly.Foolish.ModTrakt.Client.Commands
{
    using System.CommandLine;
    using Interfaces.Commands;

    internal sealed class RootCommandBuilder : IRootCommandBuilder
    {
        private readonly ICommandBuilder _list;

         internal RootCommandBuilder(ICommandBuilder list) => _list = list;

         public RootCommand BuildCommand() =>
            new RootCommand(Properties.Resources.Application_Description) {_list.BuildCommand()};
    }
}
