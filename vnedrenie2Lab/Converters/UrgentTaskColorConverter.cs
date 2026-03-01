using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;
using vnedrenie2Lab.Models;

namespace vnedrenie2Lab.Converters
{
    public class UrgentTaskColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is Task task)
            {
                // Если задача горит (меньше 1 дня до дедлайна)
                if (task.Deadline <= DateTime.Now.AddDays(1)  && task.Status != TaskStatus.Закончена)
                {
                    // Градиент для горящих задач
                    return new SolidColorBrush(Color.Parse("#FFF0F0"));
                }
                
                // Обычные задачи
                return new SolidColorBrush(Colors.Transparent);
            }
            
            return new SolidColorBrush(Colors.Transparent);
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    
    public class IsUrgentConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is Task task  && task.Status != TaskStatus.Закончена)
            {
                // Показываем индикатор огня, если задача горит
                return task.Deadline <= DateTime.Now.AddDays(1);
            }
            
            return false;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    
    public class IsVeryUrgentConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is Task task  && task.Status != TaskStatus.Закончена)
            {
                // Показываем надпись "ГОРИТ", если до дедлайна меньше 12 часов
                return task.Deadline <= DateTime.Now.AddHours(12);
            }
            
            return false;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    
    public class TitleColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is Task task && task.Status != TaskStatus.Закончена)
            {
                // Для очень срочных задач делаем заголовок красным
                if (task.Deadline <= DateTime.Now.AddHours(12))
                {
                    return new SolidColorBrush(Color.Parse("#FF4444"));
                }
                
                // Для обычных горящих задач - темно-серый
                if (task.Deadline <= DateTime.Now.AddDays(1))
                {
                    return new SolidColorBrush(Color.Parse("#2D3436"));
                }
            }
            
            return new SolidColorBrush(Color.Parse("#2D3436"));
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}