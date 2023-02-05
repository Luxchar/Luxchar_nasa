using System;
using System.Windows.Data;
using System.Drawing;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace WinForm_Luc_Charlopeau.Converters
{
    public class ImageToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Bitmap bitmap = value as Bitmap;
            return bitmap == null ? null : Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), IntPtr.Zero, System.Windows.Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
