namespace Senselessly.Foolish.Bethesda.Archives.Interfaces.Extensions
{
    using System.Collections.Generic;
    using Enums;

    public interface IPlugin
    {
        ArchiveTypes Archives { get; set; }

        string ExtraFolders { get; }

        string ExtraFiles { get; }

        string FolderName { get; }

        string Folders { get; set; }

        string Files { get; set; }

        int ModId { get; }

        string ModName { get; }

        LooseTypes Loose { get; set; }

        IDictionary<PluginTypes, int> TypeDict { get; set; }

        string Types { get; }
    }
}