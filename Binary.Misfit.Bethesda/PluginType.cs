namespace Binary.Misfit.Bethesda
{
    using System;

    [Flags]
    public enum PluginType
    {
        None = 0,
        Plugin = 1,
        Light = 2,
        Master = 4
    }
}