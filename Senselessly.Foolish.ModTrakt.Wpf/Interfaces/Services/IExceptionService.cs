namespace Senselessly.Foolish.ModTrakt.Wpf.Interfaces.Services
{
    using App;

    internal interface IExceptionService
    {
        void Send(IExceptionInfo e);
    }
}