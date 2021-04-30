namespace Senselessly.Foolish.Bethesda.Wpf.Context.Services
{
    using Interface;
    using Microsoft.Toolkit.Mvvm.Messaging;
    using Models;

    public class ExceptionService : IExceptionService
    {
        public void Send(IExceptionInfo e)
        {
            WeakReferenceMessenger.Default.Send(new ExceptionRaisedMessage(e));
        }
    }
}