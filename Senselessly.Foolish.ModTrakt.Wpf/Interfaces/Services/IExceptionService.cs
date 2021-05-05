namespace Senselessly.Foolish.ModTrakt.Wpf.Interface.Services
{
    using Models;

    public interface IExceptionService
    {
        void Send(IExceptionInfo e);
    }
}