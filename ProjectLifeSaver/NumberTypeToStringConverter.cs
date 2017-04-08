using ProjectLifeSaver.Models;
using System;
using Windows.UI.Xaml.Data;

namespace ProjectLifeSaver
{
    public sealed class NumberTypeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            switch ((Number.Types)value)
            {
                case Number.Types.Police:       return "POLICE";
                case Number.Types.Ambulance:    return "AMBULANCE";
                case Number.Types.FireDept:     return "FIRE DEPT";
                case Number.Types.Sos:          return "SOS";
                default: throw new Exception(@"You're not doing it right ¯\_ツ_/¯");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
