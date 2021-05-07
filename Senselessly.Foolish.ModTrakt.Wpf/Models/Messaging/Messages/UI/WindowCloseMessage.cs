namespace Senselessly.Foolish.ModTrakt.Wpf.Models.Messaging.Messages.UI
{
    using Interfaces.Messaging.UI;
    using Microsoft.Toolkit.Mvvm.Messaging.Messages;

    internal sealed class WindowCloseMessage : ValueChangedMessage<IOptionsWindowClose>
    {
        public WindowCloseMessage(IOptionsWindowClose value) : base(value) { }
    }
}