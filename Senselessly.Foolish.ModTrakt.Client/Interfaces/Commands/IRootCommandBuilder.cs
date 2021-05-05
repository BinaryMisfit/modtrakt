namespace Senselessly.Foolish.ModTrakt.Client.Interfaces.Commands
{
    using System.CommandLine;

    public interface IRootCommandBuilder
    {
        public RootCommand BuildCommand();
    }
}