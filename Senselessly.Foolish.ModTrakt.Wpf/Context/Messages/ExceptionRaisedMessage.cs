namespace Senselessly.Foolish.ModTrakt.Wpf.Context.Messages
{
    using Interface;
    using Microsoft.Toolkit.Mvvm.Messaging.Messages;

    public class ExceptionRaisedMessage : ValueChangedMessage<IExceptionInfo>
    {
        public ExceptionRaisedMessage(IExceptionInfo value) : base(value) { }
    }
}