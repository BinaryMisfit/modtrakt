namespace Senselessly.Foolish.Bethesda.Wpf.AppData.Interface
{
    using System;

    public interface IAppSettings
    {
        ISettings Settings { get; }

        IGameSettings Game { get; }

        static IAppSettings Load() =>
            throw new NotImplementedException();
    }
}