namespace Senselessly.Foolish.ModTrakt.Wpf.Models.Messages
{
    using Interface.Messaging;
    using Microsoft.Toolkit.Mvvm.Messaging.Messages;

    public class ConfirmExitMessage : ValueChangedMessage<IOptionsConfirmExit>
    {
        public ConfirmExitMessage(IOptionsConfirmExit value) : base(value) { }
    }
}