using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Drawing;

namespace moodtracker
{
    public class MoodToImageConverter : IValueConverter
    {
        public enum Mood { Rad = 0, Good = 1, Neutral = 2, Bad = 3, Awful = 4 }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int mood = (int)value;
            return GetMoodImage(mood);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        internal static ImageSource GetMoodImage(int mood)
        {
            switch (mood)
            {
                case (int)Mood.Rad:
                    return new BitmapImage(new Uri("/Image/rad.png", UriKind.Relative));
                case (int)Mood.Good:
                    return new BitmapImage(new Uri("/Image/good.png", UriKind.Relative));
                case (int)Mood.Neutral:
                    return new BitmapImage(new Uri("/Image/neutral.png", UriKind.Relative));
                case (int)Mood.Bad:
                    return new BitmapImage(new Uri("/Image/bad.png", UriKind.Relative));
                case (int)Mood.Awful:
                    return new BitmapImage(new Uri("/Image/awful.png", UriKind.Relative));
                default:
                    return null; // Если передано некорректное настроение, то возвращаем null
            }
        }
    }
}
