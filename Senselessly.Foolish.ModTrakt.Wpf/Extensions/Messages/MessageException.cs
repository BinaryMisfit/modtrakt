namespace Senselessly.Foolish.ModTrakt.Wpf.Extensions.Messages
{
    using Interfaces.App;
    using Microsoft.Toolkit.Mvvm.Messaging;
    using Models.Messaging;

    internal static class MessageException
    {
        public static void SendException(this IExceptionInfo info)
        {
            if (info == null) { return; }

            WeakReferenceMessenger.Default.Send(new ExceptionRaisedMessage(info));
        }
    }
}