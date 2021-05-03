namespace Senselessly.Foolish.ModTrakt.Wpf.Context.Messages
{
    using Interface;
    using Microsoft.Toolkit.Mvvm.Messaging.Messages;

    public class ConfirmExitMessage : ValueChangedMessage<IOptionsConfirmExit>
    {
        public ConfirmExitMessage(IOptionsConfirmExit value) : base(value) { }
    }
}