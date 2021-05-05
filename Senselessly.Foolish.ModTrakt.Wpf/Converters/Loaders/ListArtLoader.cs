namespace Senselessly.Foolish.ModTrakt.Wpf.Converters.Loaders
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Windows.Data;
    using System.Windows.Media.Imaging;

    internal sealed class ListArtLoader : IValueConverter
    {
        public static readonly ListArtLoader Instance = new ListArtLoader();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) { return null; }

            var resourceSpace = Assembly.GetExecutingAssembly()
                                        .GetTypes()
                                        .Where(t => t.Namespace != null &&
                                                    !t.Namespace.Equals("XamlGeneratedNamespace"))
                                        .Select(t => t.Namespace)
                                        .Distinct()
                                        .First();
            var resourcePath = $"{resourceSpace}.Resources.Images.ListArt.{value}.jpg";
            var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath);
            if (resource == null) { return null; }

            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.StreamSource = resource;
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();
            bitmap.Freeze();
            return bitmap;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }
}