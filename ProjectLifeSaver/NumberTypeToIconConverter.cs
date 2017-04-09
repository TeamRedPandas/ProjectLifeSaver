using ProjectLifeSaver.Models;
using System;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace ProjectLifeSaver
{
    public sealed class NumberTypeToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            switch ((Number.Types)value)
            {
                case Number.Types.Police:
                case Number.Types.Ambulance:
                case Number.Types.FireDept:
                case Number.Types.Sos:
                    return new Uri("ms-appx:///Assets/" + value.ToString() + ".png");
                default: throw new Exception(@"You're not doing it right ¯\_ツ_/¯");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
