namespace Senselessly.Foolish.ModTrakt.Wpf.Models.Exceptions.Settings
{
    using System;
    using System.Runtime.Serialization;
    using Properties;

    [Serializable]
    public sealed class ConfigurationSectionMissingException : Exception
    {
        private ConfigurationSectionMissingException() { }

        public ConfigurationSectionMissingException(string section) : base(
            string.Format(Resources.Exception_Config_Section_Missing, section)) =>
            Section = section;

        public ConfigurationSectionMissingException(string section, string message) : base(message) =>
            Section = section;

        public ConfigurationSectionMissingException(string section, string message, Exception inner) : base(
            message,
            inner) =>
            Section = section;

        private ConfigurationSectionMissingException(SerializationInfo info, StreamingContext context) : base(
            info,
            context) { }

        public string Section { get; }
    }
}