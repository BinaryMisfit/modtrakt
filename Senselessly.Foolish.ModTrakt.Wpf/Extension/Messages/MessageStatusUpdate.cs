namespace Senselessly.Foolish.ModTrakt.Wpf.Extension.Messages
{
    using Interfaces.Messaging;
    using Microsoft.Toolkit.Mvvm.Messaging;
    using Models.Messaging;

    internal static class MessageStatusUpdate
    {
        public static void SendUpdate(this IMessageStatusUpdate sender, string message)
        {
            if (sender == null) { return; }

            WeakReferenceMessenger.Default.Send(new StatusUpdateMessage(new StatusUpdateOptions(message)));
        }
    }
}