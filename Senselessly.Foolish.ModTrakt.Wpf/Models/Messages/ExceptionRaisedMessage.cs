namespace Senselessly.Foolish.ModTrakt.Wpf.Models.Messages
{
    using Interface.Models;
    using Microsoft.Toolkit.Mvvm.Messaging.Messages;

    public class ExceptionRaisedMessage : ValueChangedMessage<IExceptionInfo>
    {
        public ExceptionRaisedMessage(IExceptionInfo value) : base(value) { }
    }
}