namespace Senselessly.Foolish.Bethesda.Archives.Enums
{
    using System;

    [Flags]
    public enum ArchiveTypes
    {
        None = 0,
        Main = 1,
        Meshes = 2,
        Materials = 4,
        Interface = 8,
        Scripts = 16,
        Sound = 32,
        Textures = 64
    }
}