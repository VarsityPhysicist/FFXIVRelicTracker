using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace FFXIVRelicTracker.Helpers.Converters
{
    class BrokenMapCanvasSizeToXConverter:IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[1] is PointF)
            {
                double tempWidth = (double)values[0];
                PointF tempPoint = (PointF)values[1];
                double elementWidth = (double)values[2] / 2;

                return tempWidth * tempPoint.X - 1 - elementWidth;
            }
            else
                return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
