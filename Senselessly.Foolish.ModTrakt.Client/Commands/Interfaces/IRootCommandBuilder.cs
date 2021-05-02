namespace Senselessly.Foolish.ModTrakt.Client.Commands.Interfaces
{
    using System.CommandLine;

    public interface IRootCommandBuilder
    {
        public RootCommand BuildCommand();
    }
}