using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;
using vnedrenie2Lab.Models;

namespace vnedrenie2Lab.Converters
{
    public class DateToStringConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is DateTime date)
            {
                return date.ToString("dd MMMM yyyy");
            }
            return string.Empty;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    public class UserIdToBrushConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is int userId && parameter is int currentUserId)
            {
                // Если это текущий пользователь - фиолетовый фон
                if (userId == currentUserId)
                {
                    return new SolidColorBrush(Color.Parse("#6C5CE7"));
                }
                
                // Иначе - светло-серый фон
                return new SolidColorBrush(Color.Parse("#F1F2F6"));
            }
            
            // Для конвертации в цвет текста (параметр "Text")
            if (parameter is string param && param == "Text")
            {
                if (value is int userIdText && parameter is int currentUserIdText)
                {
                    return userIdText == currentUserIdText 
                        ? new SolidColorBrush(Colors.White)
                        : new SolidColorBrush(Color.Parse("#2D3436"));
                }
            }
            
            return new SolidColorBrush(Color.Parse("#F1F2F6"));
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    public class TaskStatusColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is TaskStatus status)
            {
                return status switch
                {
                    TaskStatus.ТребуетВзятия => new SolidColorBrush(Color.Parse("#FDCB6E")),
                    TaskStatus.Взята => new SolidColorBrush(Color.Parse("#6C5CE7")),
                    TaskStatus.Закончена => new SolidColorBrush(Color.Parse("#00B894")),
                    TaskStatus.НаПроверке => new SolidColorBrush(Color.Parse("#636E72")),
                    _ => new SolidColorBrush(Color.Parse("#636E72"))
                };
            }
            return new SolidColorBrush(Color.Parse("#636E72"));
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    public class TaskStatusToStringConverter : IValueConverter
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

    public class BoolToOpacityConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is int count)
            {
                return count == 0;
            }
            return false;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}