namespace Senselessly.Foolish.ModTrakt.Wpf.Models.Messaging.Messages.UI
{
    using Interfaces.Messaging.UI;
    using Microsoft.Toolkit.Mvvm.Messaging.Messages;

    internal sealed class ConfirmExitMessage : ValueChangedMessage<IOptionsConfirmExit>
    {
        public ConfirmExitMessage(IOptionsConfirmExit value) : base(value) { }
    }
}