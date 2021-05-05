namespace Senselessly.Foolish.ModTrakt.Client.Interfaces.Commands
{
    using System.CommandLine;

    internal interface ICommandBuilder
    {
        Command BuildCommand();
    }
}