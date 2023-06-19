using System.Globalization;

namespace MauiApp10;

public class BoolToValueConverter : IValueConverter
{
	public object TrueValue { get; set; }
	public object FalseValue { get; set; }

	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (value is bool boolValue && boolValue)
		{
			return this.TrueValue;
		}
		else
		{
			return this.FalseValue;
		}
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return object.Equals(this.TrueValue, value);
	}
}
