namespace Senselessly.Foolish.ModTrakt.Wpf.Models.Messages
{
    using Interface.Messaging;
    using Microsoft.Toolkit.Mvvm.Messaging.Messages;

    public class WindowCloseMessage : ValueChangedMessage<IOptionsWindowClose>
    {
        public WindowCloseMessage(IOptionsWindowClose value) : base(value) { }
    }
}