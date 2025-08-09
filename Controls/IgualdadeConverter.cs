using System.Globalization;

namespace Tabela.Controls;

public class IgualdadeConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is int valor && int.TryParse(parameter?.ToString(), out int parametro))
            return valor == parametro;
        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}