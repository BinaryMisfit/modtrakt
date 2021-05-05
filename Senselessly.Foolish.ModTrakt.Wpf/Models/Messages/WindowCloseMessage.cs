namespace Senselessly.Foolish.ModTrakt.Wpf.Models.Messages
{
    using Interfaces.Messaging;
    using Microsoft.Toolkit.Mvvm.Messaging.Messages;

    internal sealed class WindowCloseMessage : ValueChangedMessage<IOptionsWindowClose>
    {
        public WindowCloseMessage(IOptionsWindowClose value) : base(value) { }
    }
}