namespace Senselessly.Foolish.Bethesda.Archives.Enums
{
    using System;

    [Flags]
    public enum LooseTypes
    {
        None = 0,
        Aaf = 1,
        F4Se = 2,
        Fo4Edit = 4,
        Mcm = 8,
        Materials = 16,
        Music = 32,
        Meshes = 64,
        Interface = 128,
        Scripts = 256,
        Sound = 512,
        Textures = 1024,
        Tools = 2048,
        Video = 4096,
        Vis = 8192
    }
}