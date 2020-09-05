using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace FFXIVRelicTracker.Helpers.Converters
{
    class MyBooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value is bool)
            {
                switch ((bool)value)
                {
                    case false:
                        return Visibility.Collapsed;
                    case true:
                        return Visibility.Visible;
                }
            }
            else
            {
                BoolObject boolObject = (BoolObject)value;
                switch (boolObject.Bool)
                {
                    case false:
                        return Visibility.Collapsed;
                    case true:
                        return Visibility.Visible;
                }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((Visibility)value)
            {
                case Visibility.Collapsed:
                    return true;
                case Visibility.Visible:
                    return false;
                default:
                    return true;
            }
        }
    }
}
