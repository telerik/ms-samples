using System;
using System.Globalization;

namespace TestApp;

[ContentProperty(nameof(Items))]
public class KeyValueConverter : IValueConverter
{
    public KeyValueConverter()
    {
        this.Items = new ResourceDictionary();
    }

	public ResourceDictionary Items { get; }

    public object Convert(object value, Type type, object parameter, CultureInfo culture)
    {
        if (value is string key)
        {
            return this.Items[key];
        }

        return null;
    }

    public object ConvertBack(object value, Type type, object parameter, CultureInfo culture)
    {
        return this.Items.Where(pair => Equals(pair.Value, value))
                         .Select(pair => pair.Key)
                         .FirstOrDefault();
    }
}

