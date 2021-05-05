namespace Senselessly.Foolish.ModTrakt.Wpf.Models.Messaging
{
    using Interfaces.Messaging;
    using Microsoft.Toolkit.Mvvm.Messaging.Messages;

    internal sealed class StatusUpdateMessage : ValueChangedMessage<IOptionsStatusMessage>
    {
        public StatusUpdateMessage(IOptionsStatusMessage value) : base(value) { }
    }
}