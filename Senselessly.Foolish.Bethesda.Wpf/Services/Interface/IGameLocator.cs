namespace Senselessly.Foolish.Bethesda.Wpf.Services.Interface
{
    using System.Collections.Generic;
    using AppData.Interface;

    public interface IGameLocator
    {
        string Current { get; }

        int Count { get; }

        int Index { get; }

        bool Load(string gameDictionaryKey);

        bool Locate();

        IEnumerable<IGameSettings> Result();
    }
}