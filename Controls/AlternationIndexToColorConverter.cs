using System.Globalization;

namespace Tabela.Controls;

public class AlternationIndexToColorConverter : IValueConverter
{
    public Color TrueColor { get; set; } = Colors.LightGray;
    public Color FalseColor { get; set; } = Colors.White;

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool boolValue)
            return boolValue ? TrueColor : FalseColor;
        
        return FalseColor;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}