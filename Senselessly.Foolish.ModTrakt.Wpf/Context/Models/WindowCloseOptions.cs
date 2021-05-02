namespace Senselessly.Foolish.ModTrakt.Wpf.Context.Models
{
    using System;
    using Interface;

    public class WindowCloseOptions : IWindowCloseOptions
    {
        public WindowCloseOptions(Type source, bool close, bool shutdown)
        {
            Source = source;
            Close = close;
            Shutdown = shutdown;
        }

        public bool Close { get; }

        public bool Shutdown { get; }

        public Type Source { get; }
    }
}