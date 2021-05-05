namespace Senselessly.Foolish.ModTrakt.Wpf.Interface.Messaging
{
    using System;

    public interface IOptionsConfirmExit
    {
        Action CancelAction { get; }

        bool Close { get; }

        bool Handled { get; set; }

        string Host { get; }

        bool Shutdown { get; }
    }
}