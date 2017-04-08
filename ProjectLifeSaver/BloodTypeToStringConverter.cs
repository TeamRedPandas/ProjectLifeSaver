using ProjectLifeSaver.Models;
using System;
using Windows.UI.Xaml.Data;

namespace ProjectLifeSaver
{
    public sealed class BloodTypeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value as BloodType? == BloodType.Unset)
            {
                throw new Exception(@"You're not doing it right ¯\_ツ_/¯");
            }

            string[] split = value.ToString().Split('_');
            char rhFactor;

            switch (split[1][0])
            {
                case 'P':    rhFactor = '+'; break;
                case 'N':    rhFactor = '-'; break;
                default: throw new Exception(@"You're not doing it right ¯\_ツ_/¯");
            }

            return (split[0][0] == 'O' ? "0" : split[0]) + rhFactor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
