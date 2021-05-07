namespace Senselessly.Foolish.ModTrakt.Wpf.Interfaces.Settings
{
    using System;

    internal interface IAppSettingsGeneral : IComparable<IAppSettingsGeneral>
    {
        string ActiveGame { get; set; }
    }
}