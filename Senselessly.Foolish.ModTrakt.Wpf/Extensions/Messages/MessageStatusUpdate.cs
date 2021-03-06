namespace Senselessly.Foolish.ModTrakt.Wpf.Extensions.Messages
{
    using Interfaces.Extensions;
    using Microsoft.Toolkit.Mvvm.Messaging;
    using Models.Messaging.Messages.UI;
    using Models.Messaging.Options.UI;

    internal static class MessageStatusUpdate
    {
        public static void SendStatusUpdate(this IMessageStatusUpdate sender, string message)
        {
            if (sender == null) return;

            WeakReferenceMessenger.Default.Send(new StatusUpdateMessage(new StatusUpdateOptions(message)));
        }
    }
}