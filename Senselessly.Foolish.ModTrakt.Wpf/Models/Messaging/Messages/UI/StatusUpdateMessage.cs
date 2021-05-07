namespace Senselessly.Foolish.ModTrakt.Wpf.Models.Messaging.Messages.UI
{
    using Interfaces.Messaging.UI;
    using Microsoft.Toolkit.Mvvm.Messaging.Messages;

    internal sealed class StatusUpdateMessage : ValueChangedMessage<IOptionsStatusMessage>
    {
        public StatusUpdateMessage(IOptionsStatusMessage value) : base(value) { }
    }
}