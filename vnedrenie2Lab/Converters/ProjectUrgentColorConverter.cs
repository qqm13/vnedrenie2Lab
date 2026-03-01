using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Avalonia.Data.Converters;
using Avalonia.Media;
using vnedrenie2Lab.Models;

namespace vnedrenie2Lab.Converters
{
    // Конвертер для цвета фона проекта (срочность по дате начала или задачам)
    public class ProjectUrgentColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is Project project)
            {
                // Проверяем, есть ли просроченные задачи
                var hasOverdueTasks = project.Tasks?
                    .Any(t => t.Status != TaskStatus.Закончена && t.Deadline < DateTime.Now) ?? false;
                
                if (hasOverdueTasks)
                    return new SolidColorBrush(Color.Parse("#FFF0F0")); // Светло-красный
                
                // Проверяем, есть ли задачи, горящие сегодня
                var hasUrgentTasks = project.Tasks?
                    .Any(t => t.Status != TaskStatus.Закончена && t.Deadline <= DateTime.Now.AddDays(1)) ?? false;
                
                if (hasUrgentTasks)
                    return new SolidColorBrush(Color.Parse("#FFF9E6")); // Светло-желтый
                
                // Проект только начался (менее недели)
                if ((DateTime.Now - project.StartedDate).TotalDays < 7)
                    return new SolidColorBrush(Color.Parse("#F0F7FF")); // Светло-голубой
            }
            
            return new SolidColorBrush(Colors.Transparent);
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    // Конвертер для цвета иконки в зависимости от роли пользователя
    public class ManagerRoleColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is int managerId && parameter is int currentUserId)
            {
                // Если текущий пользователь - менеджер проекта
                if (managerId == currentUserId)
                    return new SolidColorBrush(Color.Parse("#6C5CE7")); // Фиолетовый
                
                return new SolidColorBrush(Color.Parse("#636E72")); // Серый
            }
            
            return new SolidColorBrush(Color.Parse("#636E72"));
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    // Конвертер для иконки проекта
    public class ProjectIconConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is Project project)
            {
                // Выбираем иконку в зависимости от статуса проекта
                var hasOverdueTasks = project.Tasks?
                    .Any(t => t.Status != TaskStatus.Закончена && t.Deadline < DateTime.Now) ?? false;
                
                if (hasOverdueTasks)
                    return "⚠️"; // Проблемный проект
                
                var activeTasks = project.Tasks?
                    .Count(t => t.Status == TaskStatus.Взята) ?? 0;
                
                return activeTasks > 5 ? "🔥" : "📁"; // Много активных задач или обычный проект
            }
            
            return "📁";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    // Конвертер для цвета заголовка проекта
    public class ProjectTitleColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is Project project)
            {
                var hasCriticalIssues = project.Tasks?
                    .Any(t => t.Status != TaskStatus.Закончена && t.Deadline < DateTime.Now) ?? false;
                
                if (hasCriticalIssues)
                    return new SolidColorBrush(Color.Parse("#FF4444")); // Красный
                
                return new SolidColorBrush(Color.Parse("#2D3436")); // Темно-серый
            }
            
            return new SolidColorBrush(Color.Parse("#2D3436"));
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    // Конвертер для проверки, является ли текущий пользователь менеджером
    public class IsCurrentUserManagerConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is int managerId && parameter is int currentUserId)
            {
                return managerId == currentUserId;
            }
            
            return false;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    // Конвертер для проверки, является ли текущий пользователь участником
    public class IsCurrentUserParticipantConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is List<User> users && parameter is int currentUserId)
            {
                return users?.Any(u => u.Id == currentUserId) ?? false;
            }
            
            return false;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    // Конвертер для проверки наличия задач пользователя в проекте
    public class HasUserTasksConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is List<Task> tasks && parameter is int currentUserId)
            {
                return tasks?.Any(t => t.UserId == currentUserId && t.Status != TaskStatus.Закончена) ?? false;
            }
            
            return false;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    // Конвертер для подсчета задач пользователя в проекте
    public class UserTasksCountConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is List<Task> tasks && parameter is int currentUserId)
            {
                return tasks?.Count(t => t.UserId == currentUserId && t.Status != TaskStatus.Закончена) ?? 0;
            }
            
            return 0;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}