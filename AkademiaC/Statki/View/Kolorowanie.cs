using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;
using Statki.Model;
using System.Windows.Media;
using System.Globalization;

namespace Statki.View
{
    [ValueConversion(typeof(RodzajPola), typeof(Brush))]
    public class Kolorowanie : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            RodzajPola type = (RodzajPola)value;

            switch (type)
            {
                case RodzajPola.Nieznane:
                    return new SolidColorBrush(Colors.LightGray);
                case RodzajPola.Woda:
                    return new SolidColorBrush(Colors.LightBlue);
                case RodzajPola.Nietrafiony:
                    return new SolidColorBrush(Colors.Black);
                case RodzajPola.Trafiony:
                    return new SolidColorBrush(Colors.Orange);
                case RodzajPola.Zatopiony:
                    return new SolidColorBrush(Colors.Red);
            }

            throw new Exception("Blad");
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
