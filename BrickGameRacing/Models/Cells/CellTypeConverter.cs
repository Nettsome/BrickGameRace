using System.Globalization;
using System.Windows.Data;
using System.Windows;
using System.Windows.Media;
using BrickGameRacing.Models.Cells;

namespace BrickGameRacing;

public class CellTypeConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
        new SolidColorBrush(value switch
        {
            CellType.Car => Cell.IsActiveField ? Colors.Red : Colors.DarkRed,
            CellType.Wall => Cell.IsActiveField ? Colors.Gray : Colors.WhiteSmoke,
            _ => Colors.WhiteSmoke
        });

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        DependencyProperty.UnsetValue;

}
