namespace Senselessly.Foolish.ModTrakt.Wpf.Interfaces.Settings
{
    using System.Threading.Tasks;
    using Extensions;

    internal interface IConfigurator : IMessageStatusUpdate, IJsonSource
    {
        Task<bool> Check(string key);
    }
}