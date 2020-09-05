using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace FFXIVRelicTracker.Helpers.Converters
{
    class StrikeThroughConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                if (value == null)
                { return null; }
                bool temp = (bool)value;

                if (temp)
                {
                    return TextDecorations.Strikethrough;
                }
                else { return null; }
            }
            else
            {
                BoolObject boolObject = (BoolObject)value;
                switch (boolObject.Bool)
                {
                    case true:
                        return TextDecorations.Strikethrough;
                    case false:
                        return null;
                }
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
