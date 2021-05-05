namespace Senselessly.Foolish.ModTrakt.Wpf.Models.Messages
{
    using Interfaces.Messaging;
    using Microsoft.Toolkit.Mvvm.Messaging.Messages;

    internal sealed class ShowWindowMessage : ValueChangedMessage<IOptionsShowWindow>
    {
        public ShowWindowMessage(IOptionsShowWindow value) : base(value) { }
    }
}