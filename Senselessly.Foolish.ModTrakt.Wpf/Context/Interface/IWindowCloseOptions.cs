namespace Senselessly.Foolish.ModTrakt.Wpf.Context.Interface
{
    using System;

    public interface IWindowCloseOptions
    {
        bool Close { get; }

        bool Shutdown { get; }

        Type Source { get; }
    }
}