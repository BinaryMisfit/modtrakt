namespace Senselessly.Foolish.ModTrakt.Wpf.Models.Messaging
{
    using System;
    using Interface.Messaging;

    public class ConfirmExitOptions : IOptionsConfirmExit
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