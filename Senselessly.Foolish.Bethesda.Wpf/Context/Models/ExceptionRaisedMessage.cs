namespace Senselessly.Foolish.Bethesda.Wpf.Context.Models
{
    using Interface;
    using Microsoft.Toolkit.Mvvm.Messaging.Messages;

    public class ExceptionRaisedMessage : ValueChangedMessage<IExceptionInfo>
    {
        public ExceptionRaisedMessage(IExceptionInfo value)
            : base(value)
        {
        }
    }
}