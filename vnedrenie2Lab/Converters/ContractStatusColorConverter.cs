using System;
using System.Globalization;
using System.Linq;
using Avalonia.Data.Converters;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using System.IO;
using vnedrenie2Lab.Models;

namespace vnedrenie2Lab.Converters
{
    // Статусы контрактов
    public enum ContractStatus
    {
        Draft = 0,      // Черновик
        Active = 1,     // Активен
        Pending = 2,    // На подписании
        Completed = 3,  // Завершен
        Terminated = 4  // Расторгнут
    }

    public class ContractStatusColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is ContractStatus status)
            {
                return status switch
                {
                    ContractStatus.Draft => new SolidColorBrush(Color.Parse("#95A5A6")),      // Серый
                    ContractStatus.Active => new SolidColorBrush(Color.Parse("#00B894")),     // Зеленый
                    ContractStatus.Pending => new SolidColorBrush(Color.Parse("#FDCB6E")),    // Желтый
                    ContractStatus.Completed => new SolidColorBrush(Color.Parse("#6C5CE7")),  // Фиолетовый
                    ContractStatus.Terminated => new SolidColorBrush(Color.Parse("#FF4444")), // Красный
                    _ => new SolidColorBrush(Color.Parse("#95A5A6"))
                };
            }
            return new SolidColorBrush(Color.Parse("#95A5A6"));
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    public class ContractStatusBackgroundConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is ContractStatus status)
            {
                return status switch
                {
                    ContractStatus.Draft => new SolidColorBrush(Color.Parse("#F5F5F5")),
                    ContractStatus.Active => new SolidColorBrush(Color.Parse("#F0FFF4")),
                    ContractStatus.Pending => new SolidColorBrush(Color.Parse("#FFF9E6")),
                    ContractStatus.Completed => new SolidColorBrush(Color.Parse("#F0F7FF")),
                    ContractStatus.Terminated => new SolidColorBrush(Color.Parse("#FFF0F0")),
                    _ => new SolidColorBrush(Colors.White)
                };
            }
            return new SolidColorBrush(Colors.White);
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    public class ContractStatusToStringConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is ContractStatus status)
            {
                return status switch
                {
                    ContractStatus.Draft => "Черновик",
                    ContractStatus.Active => "Активен",
                    ContractStatus.Pending => "На подписании",
                    ContractStatus.Completed => "Завершен",
                    ContractStatus.Terminated => "Расторгнут",
                    _ => "Неизвестно"
                };
            }
            return "Неизвестно";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    public class ContractStatusIconConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is ContractStatus status)
            {
                return status switch
                {
                    ContractStatus.Draft => "📝",
                    ContractStatus.Active => "✓",
                    ContractStatus.Pending => "⏳",
                    ContractStatus.Completed => "✅",
                    ContractStatus.Terminated => "✗",
                    _ => "📄"
                };
            }
            return "📄";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    public class RecentlyUpdatedConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is DateTime updatedAt)
            {
                // Показывать индикатор, если обновлено за последние 7 дней
                return (DateTime.Now - updatedAt).TotalDays <= 7;
            }
            return false;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    public class ScanExistsConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value is byte[] bytes && bytes.Length > 0;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    public class ScanNotExistsConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return !(value is byte[] bytes && bytes.Length > 0);
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    public class ScanExistsColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is byte[] bytes && bytes.Length > 0)
            {
                return new SolidColorBrush(Color.Parse("#6C5CE7")); // Фиолетовый
            }
            return new SolidColorBrush(Color.Parse("#95A5A6")); // Серый
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    public class ByteArrayToImageConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is byte[] bytes && bytes.Length > 0)
            {
                try
                {
                    using var ms = new MemoryStream(bytes);
                    return new Bitmap(ms);
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

}