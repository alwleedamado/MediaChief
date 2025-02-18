using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoChief.Convertors
{
    internal class EnumToStringConvertor : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return Enum.GetName(Enum.GetUnderlyingType(targetType), value);
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var str = value.ToString();
            return Enum.Parse(Enum.GetUnderlyingType(targetType), str);
        }
    }
}
