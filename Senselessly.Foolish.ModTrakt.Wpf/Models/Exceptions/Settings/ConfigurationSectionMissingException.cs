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
            string.Format(format: Resources.Exception_Config_Section_Missing, arg0: section)) =>
            Section = section;

        public ConfigurationSectionMissingException(string section, string message) : base(message) =>
            Section = section;

        public ConfigurationSectionMissingException(string section, string message, Exception inner) : base(
            message: message,
            innerException: inner) =>
            Section = section;

        private ConfigurationSectionMissingException(SerializationInfo info, StreamingContext context) : base(
            info: info,
            context: context) { }

        public string Section { get; }
    }
}