using System;
using System.Globalization;
using System.Linq;
using Avalonia.Data.Converters;
using Avalonia.Media;
using vnedrenie2Lab.Models;

namespace vnedrenie2Lab.Converters
{
    // Конвертер для получения имени пользователя по ID
    public class UserIdToNameConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is int userId)
            {
                // Здесь должен быть запрос к базе данных или кэшу
                // Для примера возвращаем заглушку
                return $"Менеджер #{userId}";
            }
            return "Не назначен";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    // Конвертер для подсчета дней с начала проекта
    public class DaysSinceStartConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is DateTime startDate)
            {
                var days = (DateTime.Now - startDate).Days;
                return $"{days} дней";
            }
            return "0 дней";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    // Конвертер для прогресса проекта (условный)
    public class ProjectProgressConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is DateTime startDate)
            {
                // Условный прогресс на основе времени
                var daysPassed = (DateTime.Now - startDate).Days;
                // Предположим, проект длится 90 дней
                var progress = Math.Min(100, (daysPassed * 100) / 90);
                return progress;
            }
            return 0;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    // Конвертер для цвета роли пользователя
    public class UserRoleColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is int userId && parameter is int managerId)
            {
                if (userId == managerId)
                    return new SolidColorBrush(Color.Parse("#6C5CE7")); // Фиолетовый для менеджера
                
                return new SolidColorBrush(Color.Parse("#F1F2F6")); // Серый для участников
            }
            return new SolidColorBrush(Color.Parse("#F1F2F6"));
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    // Конвертер для роли пользователя (текст)
    public class UserRoleConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is int userId && parameter is int managerId)
            {
                return userId == managerId ? "Менеджер" : "Участник";
            }
            return "Участник";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    // Конвертер для подсчета активных задач
    public class ActiveTasksCountConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is System.Collections.Generic.List<Task> tasks)
            {
                return tasks.Count(t => t.Status == TaskStatus.Взята || t.Status == TaskStatus.НаПроверке);
            }
            return 0;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    // Конвертер для подсчета завершенных задач
    public class CompletedTasksCountConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is System.Collections.Generic.List<Task> tasks)
            {
                return tasks.Count(t => t.Status == TaskStatus.Закончена);
            }
            return 0;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    // Конвертер для фона задачи по статусу
    public class TaskStatusBackgroundConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is TaskStatus status)
            {
                return status switch
                {
                    TaskStatus.Закончена => new SolidColorBrush(Color.Parse("#F0FFF4")),
                    TaskStatus.Взята => new SolidColorBrush(Color.Parse("#F0F7FF")),
                    TaskStatus.НаПроверке => new SolidColorBrush(Color.Parse("#FFF9E6")),
                    TaskStatus.Горит => new SolidColorBrush(Color.Parse("#F5F5F5")),
                    _ => new SolidColorBrush(Colors.White)
                };
            }
            return new SolidColorBrush(Colors.White);
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    // Конвертер для проверки пустой коллекции
    public class EmptyCollectionToVisibilityConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is int count)
            {
                return count == 0;
            }
            return true;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}