using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;
using vnedrenie2Lab.Models;

namespace vnedrenie2Lab.Converters
{
    public class StatusToColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is TaskStatus status)
            {
                return status switch
                {
                    TaskStatus.Горит => new SolidColorBrush(Color.Parse("#8C030A")),
                    TaskStatus.Взята => new SolidColorBrush(Color.Parse("056FFA")),
                    TaskStatus.Закончена => new SolidColorBrush(Color.Parse("#EAE6E6")),
                    TaskStatus.ТребуетВзятия => new SolidColorBrush(Color.Parse("#FFCF1A")),
                    TaskStatus.НаПроверке => new SolidColorBrush(Color.Parse("#1AFFF6")),
                    _ => new SolidColorBrush(Color.Parse("#636E72"))
                };
            }
            return new SolidColorBrush(Color.Parse("#636E72"));
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    public class StatusToStringConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is TaskStatus status)
            {
                return status switch
                {
                    TaskStatus.Горит => "Горит",
                    TaskStatus.ТребуетВзятия => "Требует Взятия",
                    TaskStatus.Взята => "Взята",
                    TaskStatus.Закончена => "Закончена",
                    TaskStatus.НаПроверке => "На Проверке",
                    
                    _ => "Неизвестно"
                };
            }
            return "Неизвестно";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    public class DeadlineProgressConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is DateTime deadline)
            {
                var now = DateTime.Now;
                var total = (deadline - now).TotalMilliseconds;
                
                if (total <= 0) return 100;
                if (total > 7 * 24 * 60 * 60 * 1000) return 0; // больше недели
                
                // Прогресс от 0 до 100, где 100% - просрочено
                var progress = 100 - (total / (7 * 24 * 60 * 60 * 1000) * 100);
                return Math.Max(0, Math.Min(100, progress));
            }
            return 0;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    public class DeadlineProgressColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is DateTime deadline)
            {
                var daysLeft = (deadline - DateTime.Now).Days;
                
                return daysLeft switch
                {
                    < 0 => new SolidColorBrush(Color.Parse("#BECACA")), // Красный
                    0 => new SolidColorBrush(Color.Parse("#FF6B6B")),   // Светло-красный
                    1 => new SolidColorBrush(Color.Parse("#FDCB6E")),   // Желтый
                    2 => new SolidColorBrush(Color.Parse("#6C5CE7")),   // Фиолетовый
                    _ => new SolidColorBrush(Color.Parse("#00B894"))    // Зеленый
                };
            }
            return new SolidColorBrush(Color.Parse("#00B894"));
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    public class TimeLeftConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is DateTime deadline)
            {
                var timeLeft = deadline - DateTime.Now;
                
                if (timeLeft.TotalDays < 0)
                    return $"Завершена {Math.Abs(timeLeft.Days)} дн. {Math.Abs(timeLeft.Hours)} ч. назад";
                
                if (timeLeft.TotalDays >= 1)
                    return $"Осталось {timeLeft.Days} дн. {timeLeft.Hours} ч.";
                
                if (timeLeft.TotalHours >= 1)
                    return $"Осталось {timeLeft.Hours} ч. {timeLeft.Minutes} мин.";
                
                if (timeLeft.TotalMinutes >= 1)
                    return $"Осталось {timeLeft.Minutes} мин.";
                
                return "Менее минуты";
            }
            return string.Empty;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    public class DeadlineColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is DateTime deadline)
            {
                var daysLeft = (deadline - DateTime.Now).Days;
                
                return daysLeft switch
                {
                    < 0 => new SolidColorBrush(Color.Parse("#BECACA")),
                    0 => new SolidColorBrush(Color.Parse("#FF6B6B")),
                    1 => new SolidColorBrush(Color.Parse("#FDCB6E")),
                    _ => new SolidColorBrush(Color.Parse("#7F8C8D"))
                };
            }
            return new SolidColorBrush(Color.Parse("#7F8C8D"));
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    public class DifficultyToStarsConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is int difficulty)
            {
                return new string('⭐', difficulty);
            }
            return string.Empty;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    public class NullToVisibilityConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value == null;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}