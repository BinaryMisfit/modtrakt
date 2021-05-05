namespace Senselessly.Foolish.ModTrakt.Wpf.Models.Messages
{
    using Interfaces.Messaging;
    using Microsoft.Toolkit.Mvvm.Messaging.Messages;

    internal sealed class ConfirmExitMessage : ValueChangedMessage<IOptionsConfirmExit>
    {
        public ConfirmExitMessage(IOptionsConfirmExit value) : base(value) { }
    }
}