using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tebbet.Converter
{
    public class BytesConverter : IValueConverter
    {
        public static readonly BytesConverter Instance = new BytesConverter();

        public object? Convert(object? value, Type targetType, object? parameter,
                                                            CultureInfo culture)
        {
            if (value is byte[] bytes)
            {
                Bitmap bitmap;
                using (var memorystream = new MemoryStream(bytes))
                {
                    bitmap = new Bitmap(memorystream);
                    return bitmap;
                }
            }

            return new BindingNotification(new InvalidCastException(), BindingErrorType.Error);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}