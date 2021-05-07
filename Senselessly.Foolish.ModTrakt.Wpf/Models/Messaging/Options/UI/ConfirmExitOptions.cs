namespace Senselessly.Foolish.ModTrakt.Wpf.Models.Messaging.Options.UI
{
    using System;
    using Interfaces.Messaging.UI;

    internal sealed class ConfirmExitOptions : IOptionsConfirmExit
    {
        public ConfirmExitOptions(string host, bool close, bool shutdown, Action cancel = null)
        {
            CancelAction = cancel;
            Close = close;
            Host = host;
            Shutdown = shutdown;
        }

        public Action CancelAction { get; }

        public bool Close { get; }

        public bool Handled { get; set; }

        public string Host { get; }

        public bool Shutdown { get; }
    }
}