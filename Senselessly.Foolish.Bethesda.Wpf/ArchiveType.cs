namespace Senselessly.Foolish.Bethesda.Wpf
{
    using System;

    [Flags]
    public enum ArchiveType
    {
        None = 0,
        Main = 1,
        Meshes = 2,
        Materials = 4,
        Interface = 8,
        Scripts = 16,
        Sound = 32,
        Textures = 64,
    }
}