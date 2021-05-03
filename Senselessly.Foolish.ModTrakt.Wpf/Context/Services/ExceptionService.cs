namespace Senselessly.Foolish.ModTrakt.Wpf.Context.Services
{
    using Interface;
    using Messages;
    using Microsoft.Toolkit.Mvvm.Messaging;

    public class ExceptionService : IExceptionService
    {
        public void Send(IExceptionInfo e)
        {
            WeakReferenceMessenger.Default.Send(new ExceptionRaisedMessage(e));
        }
    }
}