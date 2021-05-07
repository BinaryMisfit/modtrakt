namespace Senselessly.Foolish.ModTrakt.Wpf.Models.Messaging.Options.UI
{
    using System;
    using Interfaces.Messaging.UI;

    internal sealed class WindowCloseOptions : IOptionsWindowClose
    {
        public WindowCloseOptions(Type source, bool close, bool shutdown)
        {
            Source = source;
            Close = close;
            Shutdown = shutdown;
        }

        public bool Close { get; }

        public bool Handled { get; set; }

        public bool Shutdown { get; }

        public Type Source { get; }
    }
}